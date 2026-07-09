using Application.Interfaces.Repositorys;
using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Persistense.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistense.Extentions.Repository;

public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : BaseAuditableEntity<TKey>
{
    private readonly ApplicationDataContext _Context;
    public GenericRepository(ApplicationDataContext Context)
    {
        _Context = Context;
    }

    public IQueryable<T> Entiteis => _Context.Set<T>();

    public async Task<T> DeleteAsync(TKey id)
    {
        var exist = await _Context.Set<T>().FirstOrDefaultAsync(x => x.Id!.Equals(id));

        if (exist == null)
        {
            throw new Exception($"{typeof(T).Name} does not exist");
        }

        exist.IsDelete = true;
        exist.UpdateDate = DateTime.UtcNow;

        _Context.Set<T>().Update(exist);

        return exist;
    }

    public async Task<List<T>> GetAll()
    {
        return await _Context.Set<T>().Where(x => !x.IsDelete).ToListAsync();
    }

    public async Task<T> GetByIdAsync(TKey id)
    {
        var exist = await _Context.Set<T>().FirstOrDefaultAsync(x => x.Id!.Equals(id));
        if (exist == null)
        {
            throw new Exception($"{typeof(T).Name} does not exist");
        }
        return exist;
    }

    public async Task<T> PostAsync(T entity)
    {
        entity.CreateDate = DateTime.Now;
        entity.IsDelete = false;
        entity.IsActive = true;

        await _Context.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task<T> PutAsync(TKey id, T entity)
    {
        var exist = _Context.Set<T>().FirstOrDefaultAsync(x => x.Id!.Equals(id));
        if (exist == null)
        {
            throw new Exception($"{typeof(T).Name} does not exist.");
        }

        entity.UpdateDate = DateTime.Now;
        _Context.Set<T>().Update(entity);
        return exist;
    }
}
