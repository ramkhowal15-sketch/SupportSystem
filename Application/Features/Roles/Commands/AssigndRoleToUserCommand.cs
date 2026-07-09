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

public class AssigndRoleToUserCommand : IRequest<Result<string>>
{
    public int UserId { get; set; }
    public Guid RoleId { get; set; }

    internal class AssigndRoleToUserCommandhandler : IRequestHandler<AssigndRoleToUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssigndRoleToUserCommandhandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(AssigndRoleToUserCommand command, CancellationToken cancellationToken)
        {
            var userRepository = _unitOfWork.Repository<User, int>();
            var roleRepository = _unitOfWork.Repository<Role, Guid>();

            var user = await userRepository.Entiteis.FirstOrDefaultAsync(x => x.Id == command.UserId,cancellationToken);

            if (user == null)
            {
                return Result<string>.BadRequest("User Not Found");
            }

            var role = await roleRepository.Entiteis.FirstOrDefaultAsync(x => x.Id == command.RoleId,cancellationToken);

            if (role == null)
            {
                return Result<string>.BadRequest("Role Not Found");
            }

            if (user.RoleId == command.RoleId)
            {
                return Result<string>.BadRequest("Role assigned Already");
            }

            user.RoleId = command.RoleId;


            _mapper.Map(role, user);
            await userRepository.PutAsync(user.Id,user);
            await _unitOfWork.Save(cancellationToken);

           
            return Result<string>.Success("Role assigned");
        }
    }
}
