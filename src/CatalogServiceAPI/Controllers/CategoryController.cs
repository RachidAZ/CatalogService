using Application.Mappers;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace CatalogServiceAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{


    private readonly ICategoryService categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet( "GetListCategorys")]
    public IEnumerable<Category> GetListCategorys()
    {

        return categoryService.GetAllCategories().Value;

    }

    [HttpGet("GetCategory")]
    public Category GetCategorys(int id)
    {

        return categoryService.GetCategory(id).Value;

    }

    [HttpPost("AddCategory")]
    public ActionResult AddCategory(CategoryDto category)
    {

        // or update infra so that we modify the parent category explicitly?

        Category parentCategory= null;
        if (category.CategoryParentId is not null) {
            parentCategory = categoryService.GetCategory((int)category.CategoryParentId).Value;
            }
        var res =categoryService.AddCategory(CategoryMapper.ToEntity(category, parentCategory));
        if (res.IsSuccess)
        return Ok();
        else 
            return BadRequest(res);    

    }

    [HttpPost("DeleteCategory")]
    public ActionResult DeleteCategory(int categoryId)
    {
        categoryService.DeleteCategory(categoryId);
        return Ok();

    }

    [HttpPost("UpdateCategory")]
    public ActionResult UpdateCategory([System.Web.Http.FromUri] int categoryId, CategoryDto category)
    {
        Category parentCategory = null;
        if (category.CategoryParentId is not null)
        {
            parentCategory = categoryService.GetCategory((int)category.CategoryParentId).Value;
        }

        var cat= CategoryMapper.ToEntity(category, parentCategory);
        var res = categoryService.UpdateCategory(cat);
        if (res.IsSuccess)
            return Ok();
        else
            return BadRequest(res);

    }
}