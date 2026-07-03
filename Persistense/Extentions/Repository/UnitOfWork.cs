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

    public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
    {
        var type = typeof(T).Name;

        if (!_repo.ContainsKey(type))
        {
            var repository = new GenericRepository<T>(_context);

            _repo.Add(type, repository);
        }
        return (IGenericRepository<T>)_repo[type];
    }

    public Task<int> Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
