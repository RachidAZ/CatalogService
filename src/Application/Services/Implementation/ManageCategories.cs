using Application.Common;
using Application.Mappers;
using Application.Services.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation;

public class ManageCategories : ICategoryService
{

    private readonly IRepository<Category, int> _repository;
    private readonly IRepository<Product, int> _repositoryProducts;

    public ManageCategories(IRepository<Category, int> categoryService)
    {
        _repository = categoryService;
    }
    public Result<Category> AddCategory(Category category)
    {
        try
        {

            _repository.Add(category);
            return Result<Category>.Success(null);

        }
        catch (Exception ex)
        {

            return Result<Category>.Failure(ex.Message);
        }


    }

    public Result<int> DeleteCategory(int categoryId)
    {
        _repository.Delete(categoryId);

        var productsOfCategory=_repositoryProducts.GetAll().Where(p => p.Category.Id == categoryId);
        foreach(var product in productsOfCategory)
        {
            _repositoryProducts.Delete(product.Id);
        }

        return Result<int>.Success(categoryId);


    }

    public Result<IList<Category>> GetAllCategories()
    {
        return Result<IList<Category>>.Success(_repository.GetAll().ToList());
    }

    public Result<IList<Category>> GetAllCategories(int page, int nbrRecords)
    {
       return Result<IList<Category>>.Success(_repository.GetAll(page, nbrRecords).ToList());
    }

    public Result<Category> GetCategory(int id)
    {
        return Result<Category>.Success(_repository.GetByKey(id));
    }

    public Result<int> UpdateCategory(Category category)
    {
        _repository.Update(category);
        return Result<int>.Success(category.Id);
    }
}
