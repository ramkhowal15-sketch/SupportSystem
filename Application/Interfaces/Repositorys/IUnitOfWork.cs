using System;
using System.Collections.Generic;
using System.Text;
using Domain.Commons;

namespace Application.Interfaces.Repositorys;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;

    Task<int> Save(CancellationToken cancellationToken);
}
