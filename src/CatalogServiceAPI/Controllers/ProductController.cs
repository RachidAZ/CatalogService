using System.Web.Http;
using Application.Mappers;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("GetListProducts")]
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "ReadPolicy")]
    public ActionResult<IEnumerable<ProductDto>> GetListProducts()
    {
        Application.Common.Result<IList<Product>> products = productService.GetAllProducts();
        if (products.IsSuccess)
        {
            return Ok(productService.GetAllProducts().Value);
        }
        else
        {
            return BadRequest(products.ErrorMessage);
        }
    }

    [HttpPost("AddProduct")]
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = "WritePolicy")]
    public ActionResult AddProducts(ProductDto productDto)
    {
        //todo mapping should be inside service
        Application.Common.Result<Product> res = productService.AddProduct(ProductMapper.ToEntity(productDto));
        if (res.IsSuccess)
        {
            return Ok();
        }
        else
        {
            return BadRequest(res.ErrorMessage);
        }

    }

    [HttpGet("GetListProductsPagination")]
    public ActionResult<IEnumerable<ProductDto>> GetListProducts([FromUri] int page = 1, [FromUri] int nbrRecords = 10)
    {
        Application.Common.Result<IList<Product>> products = productService.GetAllProducts(page, nbrRecords);
        if (products.IsSuccess)
        {
            return Ok(products.Value);
        }
        else
        {
            return BadRequest(products.ErrorMessage);
        }
    }

    [HttpGet("GetProduct/{id}")]
    public ActionResult<ProductDto> GetProduct(int id)
    {

        Product product = productService.GetProduct(id).Value;
        if (product is null)
        {
            return BadRequest();
        }
        else
        {
            return Ok(ProductMapper.ToDto(product));
        }
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
