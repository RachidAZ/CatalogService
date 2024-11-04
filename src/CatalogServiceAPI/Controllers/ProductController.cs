using Application.Mappers;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CatalogServiceAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{


    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet( "GetListProducts")]
    public ActionResult<IEnumerable<ProductDto>> GetListProducts()
    {
        var products = productService.GetAllProducts();
        if (products.IsSuccess)
            return Ok(productService.GetAllProducts().Value);
        else
            return  BadRequest(products.ErrorMessage);
    }

    [HttpGet("GetListProductsPagination")]
    public ActionResult<IEnumerable<ProductDto>> GetListProducts([FromUri] int page = 1, [FromUri] int nbrRecords = 10)
    {
        var products = productService.GetAllProducts(page, nbrRecords);
        if (products.IsSuccess)
            return Ok(products.Value);
        else
            return BadRequest(products.ErrorMessage);
    }

    [HttpGet("GetProduct/{id}")]
    public ActionResult<ProductDto> GetProduct(int id)
    {

        var product= productService.GetProduct(id).Value;
        if(product is null)
            return BadRequest();
        else
        return Ok(ProductMapper.ToDto(product));


    }

    [HttpPost("AddProduct")]
    public ActionResult AddProducts(ProductDto productDto)
    {

        productService.AddProduct(ProductMapper.ToEntity(productDto));
        return Ok();

    }

    [HttpPost("DeleteProduct")]
    public ActionResult DeleteProduct(int productId)
    {
        productService.DeleteProduct(productId);
        return Ok();

    }

    [HttpPut("UpdateProduct")]
    public ActionResult UpdateProduct(ProductUpdateDto product)
    {
        productService.UpdateProduct(ProductMapper.ToEntity(product));
        return Ok();

    }
}