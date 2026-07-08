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

public class UpdateRoleCommad : IRequest<Result<string>>
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public bool IsActive { get; set; }
    public string RoleDescription {  get; set; }
    public CreateRoleCommand CreateRole { get; set; }

    public UpdateRoleCommad(Guid Id,CreateRoleCommand createRole)
    {
       RoleId = Id;
       CreateRole = createRole;
    }

    internal class UpdateRoleCommadhandler : IRequestHandler<UpdateRoleCommad,Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoleCommadhandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(UpdateRoleCommad command,CancellationToken cancellationToken)
        {
            var roleRepository = _unitOfWork.Repository<Role, Guid>();

            var role = await _unitOfWork.Repository<Role, Guid>().Entiteis.FirstOrDefaultAsync(x => x.Id == command.RoleId);

            if(role == null)
            {
                return Result<string>.BadRequest("Role Not Found");
            }

            var existingrole = await _unitOfWork.Repository<Role,Guid>().Entiteis.FirstOrDefaultAsync(x=>x.RoleName == command.RoleName && x.RoleDescription==command.RoleDescription && x.IsActive==command.IsActive);

            if(existingrole != null)
            {
                return Result<string>.BadRequest("Role Already Exist");
            }

            //role.RoleName = commad.RoleName;
            //role.RoleDescription = commad.RoleDescription;
            //role.IsActive= commad.IsActive;

            _mapper.Map(command.CreateRole, role);
            await _unitOfWork.Repository<Role, Guid>().PutAsync(command.RoleId, role);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("Role Updated Successfully");

            
        }
    }
}
