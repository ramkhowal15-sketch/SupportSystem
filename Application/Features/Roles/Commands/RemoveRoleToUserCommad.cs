using Application.Interfaces.Repositorys;
using AutoMapper;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Roles.Commands;

public class RemoveRoleFromUserCommand : IRequest<Result<string>>
{
    public int UserId { get; set; }

    internal class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RemoveRoleFromUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(RemoveRoleFromUserCommand command, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.Repository<User, int>();

            var user = await userRepository.Entiteis.FirstOrDefaultAsync(x => x.Id == command.UserId, cancellationToken);

            if (user == null)
            {
                return Result<string>.BadRequest("User Not Found");
            }

            if (user.RoleId == null)
            {
                return Result<string>.BadRequest("User has no role assigned");
            }

            user.RoleId = Guid.Empty;

            _mapper.Map(user, cancellationToken);

            await userRepository.PutAsync(user.Id, user);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Role removed");
        }
    }
}