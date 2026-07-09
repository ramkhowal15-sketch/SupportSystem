using Application.Interfaces.Repositorys;
using AutoMapper;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Roles.Queries;

public class GetAllRolesQuery : IRequest<Result<List<Role>>>
{
    internal class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<Role>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork
                .Repository<Role, Guid>()
                .Entiteis
                .ToListAsync(cancellationToken);

            if (roles == null || !roles.Any())
            {
                return Result<List<Role>>.BadRequest("No Roles Found");
            }

            return Result<List<Role>>.Success(roles);
        }
    }
}