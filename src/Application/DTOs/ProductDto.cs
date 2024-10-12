using Domain.Entities;
using System;

public class ProductDto
{

    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int? CategoryId { get; set; }
    public long Price { get; set; }
    public int Currency { get; set; }
    public int Amount { get; set; }


}
