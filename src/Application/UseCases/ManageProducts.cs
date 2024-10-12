using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases;

public class ManageProducts : IProductService
{


    private readonly IRepository<Product, int> _productsRepository;

    public ManageProducts(IRepository<Product, int> repository)
    {
        this._productsRepository = repository;
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

        var product=_productsRepository.GetByKey(productId);
        if (product is null)
            return Result<bool>.Failure("Product Unfound");

        _productsRepository.Delete(productId);
        return Result<bool>.Success(true);
    }

    public Result<IList<Product>> GetAllProducts()
    {
        try
        {
            
            return Result< IList<Product>>.Success(_productsRepository.GetAll().ToList());

        }
        catch (Exception ex)
        {

            return Result<IList<Product>>.Failure(ex.Message);
        }
    }

    public Result<Product> GetProduct(int id)
    {
        try { 
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
