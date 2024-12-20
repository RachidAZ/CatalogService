using System;
using Domain.Entities;

public class CategoryDto
{

    public string Name { get; set; }

    public string Image { get; set; }
    public int? CategoryParentId { get; set; }


}