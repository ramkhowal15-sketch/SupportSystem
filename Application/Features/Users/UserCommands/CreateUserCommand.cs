using Application.Commons.Mappings;
using Application.Features.Users.UserCommands;
using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.UserCommands;

public class CreateUserCommand : IRequest<Result<int>> , ICreateMapFrom<User>
{
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public string Email { get; set; }
   public string Password { get; set; }
   public long PhoneNumber { get; set; }
   public Guid RoleId { get; set; }
}
internal class CreateUserCommandhandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandhandler(IUnitOfWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }

    

    public async Task<Result<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Repository<Role, Guid>().Entiteis.FirstOrDefaultAsync(x => x.Id == command.RoleId);
        if (role == null)
        {
            return Result<int>.BadRequest("Invailid Role Id");
        }
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password,
            PhoneNumber = command.PhoneNumber,
            RoleId=command.RoleId
        };

        await _unitOfWork.Repository<User,int>().PostAsync(user);
        await _unitOfWork.Save(cancellationToken);

        return Result<int>.Success(user.Id, "User Created successfully");
    }
}


