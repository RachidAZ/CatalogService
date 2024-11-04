using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;

public interface IProductService
{

    Result<Product> GetProduct(int id);
    Result<IList<Product>> GetAllProducts();
    Result<IList<Product>> GetAllProducts(int page, int nbrRecords);
    Result<Product> AddProduct(Product product);
    Result<Product> UpdateProduct(Product product);
    Result<bool> DeleteProduct(int productId);



}
