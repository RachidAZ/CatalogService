using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("GetProduct")]
    public ProductDto GetProduct(int id)
    {

        var product= productService.GetProduct(id).Value;
        return ProductMapper.ToDto(product);

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

    [HttpPost("UpdateProduct")]
    public ActionResult UpdateProduct(ProductUpdateDto product)
    {
        productService.UpdateProduct(ProductMapper.ToEntity(product));
        return Ok();

    }
}