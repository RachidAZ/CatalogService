using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductMapper
{

    public static Product ToEntity(ProductUpdateDto productUpdateDto)
    {

        return new Product()
        {

            Id = productUpdateDto.Id,
            Name = productUpdateDto.Name,
            Amount = productUpdateDto.Amount,
            Description = productUpdateDto.Description,
            Price = new Domain.ValueObjects.Money(productUpdateDto.Price, productUpdateDto.Currency),
            Image = productUpdateDto.Image,
            Category = new Category() { Id = productUpdateDto.CategoryId }

        };

    }
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto()
        {

            Name = product.Name,
            Description = product.Description,
            Amount = product.Amount,
            CategoryId = product.Category?.Id,
            Price = product.Price?.Price ?? 0,
            Image = product.Image,
            Currency = product.Price?.Currency ?? 0,

        };
    }

    public static Product ToEntity(ProductDto productDto)
    {
        var prod = new Product
        {
            Description = productDto.Description,
            Amount = productDto.Amount,
            Image = productDto.Image,
            Name = productDto.Name,
            Price = new Domain.ValueObjects.Money(productDto.Price, productDto.Currency),


        };

        if (productDto.CategoryId is not null)
            prod.Category = new Category() { Id = (int)productDto.CategoryId };

        return prod;

    }


}