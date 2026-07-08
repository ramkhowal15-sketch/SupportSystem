using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles.Commands;

public class DeleteRoleCommand : IRequest<Result<string>>
{
    public Guid RoleId { get; set;  }

    internal class DeleteRoleCommadhandler : IRequestHandler<DeleteRoleCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommadhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Repository<Role,Guid>().Entiteis.FirstOrDefaultAsync(x=>x.Id==command.RoleId,cancellationToken);

            if(role == null)
            {
                return Result<string>.BadRequest("Role Not found");
            }

            await _unitOfWork.Repository<Role,Guid>().DeleteAsync(command.RoleId);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Role Deleted Successfully");
        }
    }
}
