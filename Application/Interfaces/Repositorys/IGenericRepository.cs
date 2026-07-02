using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositorys;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Entiteis { get; }
    Task<T> PostAsync(T entity);
    Task<T> PutAsync(int id, T entity);
    Task<T> DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAll();
}
