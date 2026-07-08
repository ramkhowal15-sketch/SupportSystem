using System;
using System.Collections.Generic;
using System.Text;
using Domain.Commons;

namespace Application.Interfaces.Repositorys;

public interface IUnitOfWork
{
    IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseAuditableEntity<TKey>;
    Task<int> Save(CancellationToken cancellationToken);
}
