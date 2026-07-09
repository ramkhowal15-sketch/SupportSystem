using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles.Commands;

public class CreateRoleCommand : IRequest<Result<string>>
{
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }

    internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var existingRole = await _unitOfWork
                .Repository<Role, Guid>()
                .Entiteis
                .FirstOrDefaultAsync(x => x.RoleName == command.RoleName, cancellationToken);

            if (existingRole != null)
            {
                return Result<string>.BadRequest("Role already exists.");
            }

            var role = new Role
            {
                RoleName = command.RoleName,
                RoleDescription = command.RoleDescription
            };

            await _unitOfWork.Repository<Role, Guid>().PostAsync(role);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Role created successfully.");
        }
    }
}