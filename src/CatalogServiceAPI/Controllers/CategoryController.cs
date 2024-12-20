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

    [HttpGet("")]
    public IEnumerable<Category> List()
    {

        return categoryService.GetAllCategories().Value;

    }

    [HttpGet("{id}")]
    public Category Get(int id)
    {

        return categoryService.GetCategory(id).Value;

    }

    [HttpPost("/")]
    public ActionResult Create(CategoryDto category)
    {

        Application.Common.Result<Category> res = categoryService.AddCategory(category);
        if (res.IsSuccess)
        {
            return Ok();
        }
        else
        {
            return BadRequest(res);
        }
    }

    [HttpDelete("/")]
    public ActionResult DeleteCategory(int categoryId)
    {
        categoryService.DeleteCategory(categoryId);
        return Ok();

    }

    [HttpPut("/")]
    public ActionResult UpdateCategory([System.Web.Http.FromUri] int categoryId, CategoryDto category)
    {

        Application.Common.Result<int> res = categoryService.UpdateCategory(categoryId, category);
        if (res.IsSuccess)
        {
            return Ok();
        }
        else
        {
            return BadRequest(res);
        }
    }
}
