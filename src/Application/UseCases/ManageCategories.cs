using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases;

public class ManageCategories : ICategoryService
{

    private readonly IRepository<Category,int> _repository;

    public ManageCategories(IRepository<Category,int> categoryService)
    {
          this._repository = categoryService;
    }
    public Result<Category> AddCategory(Category category)
    {
        try
        {

            _repository.Add(category);
            return Result<Category>.Success(category);

        }
        catch (Exception ex)
        {

            return Result<Category>.Failure(ex.Message);
        }


    }

    public Result<Category> DeleteCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Result<IList<Category>> GetAllCategories()
    {
        throw new NotImplementedException();
    }

    public Result<Category> GetCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Result<Category> UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }
}
