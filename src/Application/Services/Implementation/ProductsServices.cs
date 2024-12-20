using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.EventHandlers;
using Application.Common.Events;
using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services.Implementation;

public class ProductsServices : IProductService
{


    private readonly IRepositoryProduct _productsRepository;
    private readonly ICategoryService _categoriesService;
    private readonly ProductPropertyUpdatedEventHandler _productPropertyUpdatedEventHandler;

    public ProductsServices(IRepositoryProduct repository, ProductPropertyUpdatedEventHandler productPropertyUpdatedEventHandler, ICategoryService categoryService)
    {
        _productsRepository = repository;
        _productPropertyUpdatedEventHandler = productPropertyUpdatedEventHandler;
        _categoriesService = categoryService;
    }

    public Result<Product> AddProduct(Product product)
    {

        try
        {

            product.Category = _categoriesService.GetCategory(product.Category.Id).Value;

            _productsRepository.Add(product);
            return Result<Product>.Success(product);

        }
        catch (Exception ex)
        {

            return Result<Product>.Failure(ex.Message);
        }

    }

    public Result<bool> DeleteProduct(int productId)
    {

        var product = _productsRepository.GetByKey(productId);
        if (product is null)
            return Result<bool>.Failure("Product Unfound");

        _productsRepository.Delete(productId);
        return Result<bool>.Success(true);
    }

    public Result<IList<Product>> GetAllProducts()
    {
        try
        {

            return Result<IList<Product>>.Success(_productsRepository.GetAll().ToList());

        }
        catch (Exception ex)
        {

            return Result<IList<Product>>.Failure(ex.Message);
        }
    }
    public Result<IList<Product>> GetAllProducts(int page, int nbrRecords)
    {
        try
        {
            //var products= _productsRepository.GetAll().Skip((page -1) * nbrRecords).Take(nbrRecords).ToList();
            var products = _productsRepository.GetAll(page, nbrRecords).ToList();
            return Result<IList<Product>>.Success(products);

        }
        catch (Exception ex)
        {

            return Result<IList<Product>>.Failure(ex.Message);
        }
    }

    public Result<Product> GetProduct(int id)
    {
        try
        {
            var product = _productsRepository.GetByKey(id);
            return Result<Product>.Success(product);
        }
        catch (Exception ex)
        {

            return Result<Product>.Failure(ex.Message);
        }

    }

    public Result<Product> UpdateProduct(Product product)
    {
        try
        {
            // todo: use await async pattern
            _productsRepository.Update(product);

            var updateEvent = new ProductPropertyUpdatedEvent
                (product.Id, product.Name, product.Description, product.Category.Id, product.Price, product.Amount);
            _productPropertyUpdatedEventHandler.Handle(updateEvent).Wait();
            // todo: use IMediatr instead of custom event handler
            return Result<Product>.Success(product);
        }

        catch (Exception ex)
        {

            return Result<Product>.Failure(ex.Message);
        }
    }
}