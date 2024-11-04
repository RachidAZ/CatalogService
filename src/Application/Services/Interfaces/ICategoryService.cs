using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;

public interface ICategoryService
{

    Result<Category> GetCategory(int id);
    Result<IList<Category>> GetAllCategories();
    Result<IList<Category>> GetAllCategories(int page, int nbrRecords);
    Result<Category> AddCategory(CategoryDto category);
    Result<int> UpdateCategory(int categoryId, CategoryDto category);
    Result<int> DeleteCategory(int categoryId);



}
