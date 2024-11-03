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
    public ActionResult AddCategorys(Category category)
    {
        categoryService.AddCategory(category);
        return Ok();

    }

    [HttpPost("DeleteCategory")]
    public ActionResult DeleteCategory(int categoryId)
    {
        categoryService.DeleteCategory(categoryId);
        return Ok();

    }

    [HttpPost("UpdateCategory")]
    public ActionResult UpdateCategory(Category category)
    {
        categoryService.UpdateCategory(category);
        return Ok();

    }
}