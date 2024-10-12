using Application.Interfaces;
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

    public void Delete(int entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Category> GetAll()
    {
        throw new NotImplementedException();
    }

    public Category GetByKey(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Category entity)
    {
        throw new NotImplementedException();
    }
}
