using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositorys;

public interface IGenericRepository<T,Tkey> where T : class
{
    IQueryable<T> Entiteis { get; }

    Task<List<T>> GetAll();
    Task<T?> GetByIdAsync(int id);
    Task<T> PostAsync(T entity);
    Task<T> PutAsync(Tkey id, T entity);
    Task<T> DeleteAsync(Tkey tkey);
}
