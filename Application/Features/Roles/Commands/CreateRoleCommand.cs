using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles.Commands;

public class CreateRoleCommand : IRequest<Result<string>>
{
    public string Name { get; set; }
    public string Description { get; set; }

    internal class CreateRoleCommandhandler : IRequestHandler<CreateRoleCommand, Result<string>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateRoleCommand command,CancellationToken cancellationToken)
        {
            var exitingrole = await _unitOfWork.Repository<Role,Guid>().Entiteis.FirstOrDefaultAsync(x => x.Id == command.RoleId || x.RoleName == command.RoleName,cancellationToken);

            if(exitingrole != null)
            {
                return Result<string>.BadRequest("Role Already Exist");
            }

            var role = new Role
            {
                Id = command.RoleId,
                IsActive =command.isActive,
                RoleName = command.RoleName,
                RoleDescription = command.RoleDescription,
                Users = command.Users
            };

            await _unitOfWork.Repository<Role, Guid>().PostAsync(role);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Role Created Successfull");

        }
    }


}

