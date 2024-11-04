using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces;


public interface IRepository<T, TKey> where T : class
{
    T GetByKey(TKey id);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(int page, int nbrRerords);
    void Add(T entity);
    void Update(T entity);
    void Delete(TKey entity);

}

