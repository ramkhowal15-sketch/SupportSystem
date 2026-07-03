using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Commands.USERS;

public class LoginUserCommand : IRequest<Result<string>>
{
public string Email { get; set; }
public string Password { get; set; }

    
}
internal class LogInUserCommandhandler : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public LogInUserCommandhandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().Entiteis.FirstOrDefaultAsync(x => x.Email == command.Email);

        if (user != null)
        {
            if (command.Password == user.Password)
            {
                return Result<string>.Success("Login Succefull");
            }
            return Result<string>.BadRequest("Password Wrong");


        }
        return Result<string>.BadRequest("Login Failed");







    }
}

