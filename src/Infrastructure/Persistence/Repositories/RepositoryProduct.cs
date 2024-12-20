﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;


public class RepositoryProduct : IRepositoryProduct
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

    public void DeleteByCategoryId(int categoryId)
    {

        _dbcontext.Products.Where(p => p.Category.Id == categoryId).ExecuteDelete();
        _dbcontext.SaveChanges();


    }

    public void Delete(int entity)
    {
        var product = _dbcontext.Products.Find(entity);   //.FirstOrDefault(x=>x.Id == entity);
        if (product != null)
        {
            _dbcontext.Products.Remove(product);
            _dbcontext.SaveChanges();



        }
        else
        {
            throw new Exception("Product not found");
        }


    }

    public IEnumerable<Product> GetAll()
    {
        return _dbcontext.Products.ToList();

    }

    public IEnumerable<Product> GetAll(int page, int nbrRerords)
    {
        return _dbcontext.Products.Skip((page - 1) * nbrRerords).Take(nbrRerords);
    }

    public Product GetByKey(int id)
    {
        return _dbcontext.Products.Find(id);
    }

    public void Update(Product entity)
    {
        _dbcontext.Products.Update(entity);
        _dbcontext.SaveChanges();

    }
}