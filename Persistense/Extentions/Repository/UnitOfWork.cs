using Application.Interfaces.Repositorys;
using Domain.Commons;
using Persistense.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistense.Extentions.Repository;

public class UnitOfWork : IUnitOfWork
{
    private Dictionary<string, object> _repo = new();
    private readonly ApplicationDataContext _context;

    public UnitOfWork(ApplicationDataContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseAuditableEntity<TKey>
    {
        var key = $"{typeof(TEntity).FullName}_{typeof(TKey).FullName}";

        if (!_repo.ContainsKey(key))
        {
            var repository = new GenericRepository<TEntity, TKey>(_context);
            _repo.Add(key, repository);
        }

        return (IGenericRepository<TEntity, TKey>)_repo[key];
    }

    public Task<int> Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
