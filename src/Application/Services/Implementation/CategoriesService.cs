using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Mappers;
using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services.Implementation;

public class CategoriesService : ICategoryService
{

    private readonly IRepository<Category, int> _repository;
    private readonly IRepositoryProduct _repositoryProducts;

    public CategoriesService(IRepository<Category, int> categoryService)
    {
        _repository = categoryService;
    }
    public Result<Category> AddCategory(CategoryDto category)
    {
        try
        {
            Category parentCategory = null;
            if (category.CategoryParentId.HasValue)
            {
                parentCategory = GetCategory((int)category.CategoryParentId).Value;

                if (parentCategory is null)
                    return Result<Category>.Failure("Parent Category not found");

            }
            var cat = CategoryMapper.ToEntity(category, parentCategory);
            _repository.Add(cat);
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

        _repositoryProducts.DeleteByCategoryId(categoryId);

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

    public Result<int> UpdateCategory(int categoryId, CategoryDto category)
    {

        var catToUpdate = GetCategory(categoryId).Value;

        if (catToUpdate is null)
            return Result<int>.Failure("Category not found");

        catToUpdate.Name = category.Name ?? catToUpdate.Name;
        catToUpdate.Image = category.Image ?? catToUpdate.Image;


        Category parentCategory = null;
        if (category.CategoryParentId.HasValue)
        {
            parentCategory = GetCategory((int)category.CategoryParentId).Value;
            if (parentCategory is null)
                return Result<int>.Failure("Parent Category not found");

            catToUpdate.CategoryParent = parentCategory;
        }

        _repository.Update(catToUpdate);
        return Result<int>.Success(catToUpdate.Id);
    }
}