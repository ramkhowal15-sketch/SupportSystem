using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles.Queries;

public class GetRoleByNameQuery : IRequest<Result<Role>>
{
    public string RoleName { get; set; }

    public GetRoleByNameQuery(string rolename)
    {
        RoleName = rolename;
    }

    internal class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, Result<Role>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoleByNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Role>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork
                .Repository<Role, Guid>()
                .Entiteis
                .FirstOrDefaultAsync(x => x.RoleName == request.RoleName, cancellationToken);

            if (role == null)
            {
                return Result<Role>.BadRequest("Role not found.");
            }

            return Result<Role>.Success(role, "Role fetched successfully.");
        }
    }
}