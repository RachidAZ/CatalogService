using Domain.Entities;
using System;

public class CategoryAddUpdateDto
{

 
    public string Name { get; set; }
    public string Image { get; set; }
    public int? CategoryParentId { get; set; }


}
