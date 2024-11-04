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

    [HttpGet( "GetListCategories")]
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

        var res =categoryService.AddCategory(category);
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

    [HttpPut("UpdateCategory")]
    public ActionResult UpdateCategory([System.Web.Http.FromUri] int categoryId, CategoryDto category)
    {

        var res = categoryService.UpdateCategory(categoryId, category);
        if (res.IsSuccess)
            return Ok();
        else
            return BadRequest(res);

    }
}