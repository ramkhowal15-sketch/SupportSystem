using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Commands.Users;

public class RegisterUserCommand : IRequest<Result<string>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public long MobileNo { get; set; }
    //public string Role { get; set; }

    internal class RegisterUserCommandhandler : IRequestHandler<RegisterUserCommand,Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand command,CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().Entiteis.FirstOrDefaultAsync(x => x.Email == command.Email || x.PhoneNumber == command.MobileNo);

            if (user != null)
            {
                return Result<string>.BadRequest("email or phone number already ex");
            }

            //var role = await _unitOfWork.Repository<Role>().Entiteis.FirstOrDefaultAsync(x => x.RoleName == command.Role);

            var User = new User
            {
               
                FirstName = command.UserName,
                LastName = command.Password,
                Email = command.Email,
                Password = command.Password,
                PhoneNumber = command.MobileNo
            };

            //var Role = new Role
            //{
            //    RoleName = command.Role
            //};

            await _unitOfWork.Repository<User>().PostAsync(User);
            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("User Created Successfully");
        
    }
    }
}
