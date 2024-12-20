using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Mappers;

public static class CategoryMapper
{

    public static Category ToEntity(CategoryDto categoryDto, Category parentCategory)
    {


        return new Category()
        {

            Name = categoryDto.Name,
            Image = categoryDto.Image,
            CategoryParent = parentCategory
        };

    }




}