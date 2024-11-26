using Application.Common;
using Application.Services.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation;

public class ProductsServices : IProductService
{


    private readonly IRepositoryProduct _productsRepository;

    public ProductsServices(IRepositoryProduct repository)
    {
        _productsRepository = repository;
    }

    public Result<Product> AddProduct(Product product)
    {

        try
        {

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
            var products= _productsRepository.GetAll(page, nbrRecords).ToList();
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

            _productsRepository.Update(product);
            return Result<Product>.Success(product);
        }

        catch (Exception ex)
        {

            return Result<Product>.Failure(ex.Message);
        }
    }
}
