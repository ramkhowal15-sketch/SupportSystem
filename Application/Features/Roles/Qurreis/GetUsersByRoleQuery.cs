using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Users.Queries;

public class GetUsersByRoleQuery : IRequest<Result<List<User>>>
{
    public Guid RoleId { get; set; }

    public GetUsersByRoleQuery(Guid roleId)
    {
        RoleId = roleId;
    }

    internal class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, Result<List<User>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersByRoleQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<User>>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork
                .Repository<User, int>()
                .Entiteis
                .Where(x => x.RoleId == request.RoleId)
                .ToListAsync(cancellationToken);

            if (!users.Any())
            {
                return Result<List<User>>.BadRequest("No users found for this role.");
            }

            return Result<List<User>>.Success(users, "Users fetched successfully.");
        }
    }
}