using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class RepositoryProduct : IRepository<Product, int>
{

    private readonly ApplicationDbContext _dbcontext;

    public RepositoryProduct(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public void Add(Product entity)
    {
        _dbcontext.Products.Add(entity);
        _dbcontext.SaveChanges();

    }

    public void Delete(int entity)
    {
        var product = _dbcontext.Products.FirstOrDefault(x=>x.Id == entity);
        if (product != null)
        {
            _dbcontext.Products.Remove(product);
            _dbcontext.SaveChanges();

        }


    }

    public IEnumerable<Product> GetAll()
    {
        return _dbcontext.Products.ToList();

    }

    public Product GetByKey(int id)
    {
        return _dbcontext.Products.FirstOrDefault(x => x.Id == id);
    }

    public void Update(Product entity)
    {
        _dbcontext.Products.Update(entity);
        _dbcontext.SaveChanges();

    }
}
