using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles.Queries;

public class GetRoleByIdQuery : IRequest<Result<Role>>
{
    public Guid Id { get; set; }

    public GetRoleByIdQuery(Guid id)
    {
        Id = id;
    }

    internal class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<Role>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork
                .Repository<Role, Guid>()
                .Entiteis
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (role == null)
            {
                return Result<Role>.BadRequest("Role not found.");
            }

            return Result<Role>.Success(role, "Role fetched successfully.");
        }
    }
}