using Application.Interfaces.Repositorys;
using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Commands.USERS;

public class RegisterUserCommand : IRequest<Result<string>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public long MobileNo { get; set; }
    public string Role { get; set; }

    internal class RegisterUserCommandhandler : IRequestHandler<RegisterUserCommand,Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand command,CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<>
        }
    }
}
