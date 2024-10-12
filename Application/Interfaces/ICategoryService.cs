using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICategoryService
{

    Result<Category> GetCategory(int id);
    Result<IList<Category>> GetAllCategories();
    Result<Category> AddCategory(Category category);
    Result<Category> UpdateCategory(Category category);
    Result<Category> DeleteCategory(int categoryId);



}
