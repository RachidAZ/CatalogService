﻿using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IProductService
{

    Result<Product> GetProduct(int id);
    Result<IList<Product>> GetAllProducts();
    Result<Product> AddProduct(Product product);
    Result<Product> UpdateProduct(Product product);
    Result<Product> DeleteProduct(int productId);



}