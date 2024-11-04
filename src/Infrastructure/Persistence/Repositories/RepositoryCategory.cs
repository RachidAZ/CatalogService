using Application.Services.Interfaces;
using Azure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class RepositoryCategory : IRepository<Category, int>
{
    private readonly ApplicationDbContext _dbcontext;
    public RepositoryCategory(ApplicationDbContext dbContext)
    {
         this._dbcontext = dbContext;

    }
    public void Add(Category entity)
    {
        _dbcontext.Add(entity);
        _dbcontext.SaveChanges();

    }

    public void Delete(int id)
    {
        var category = _dbcontext.Categories.Find(id);

        if (category is null)
            throw new Exception("category unfound");

        _dbcontext.Remove(category);

        _dbcontext.SaveChanges();

    }

    public IEnumerable<Category> GetAll()
    {
        return _dbcontext.Categories;
    }

    public IEnumerable<Category> GetAll(int page, int nbrRerords)
    {
        return _dbcontext.Categories.Skip((page - 1) * nbrRerords).Take(nbrRerords);  
    }

    public Category GetByKey(int id)
    {
        return _dbcontext.Categories.Find(id); 
    }

    public void Update(Category entity)
    {
        _dbcontext.Update(entity);
        _dbcontext.SaveChanges();
    }
}
