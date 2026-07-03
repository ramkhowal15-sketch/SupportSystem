using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Commands.USERS;

public class ResetUserPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public int OtpCode { get; set; }
    public string NewPassword { get; set; }
    //public bool isvalidateOtp { get; set; }

    internal class ResetUserPasswordCommandhandler : IRequestHandler<ResetUserPasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResetUserPasswordCommandhandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ResetUserPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().Entiteis.FirstOrDefaultAsync(x => x.Email == command.Email, cancellationToken);
            if (user == null)
            {
                return Result<string>.BadRequest("Email or UserName Are not match");
            }

            // await the async AnyAsync to get a bool
            var isvalidateotp = await _unitOfWork.Repository<Otp>()
                .Entiteis.AnyAsync(x => x.UserId == user.Id && x.OTPCode == command.OtpCode, cancellationToken);

            if (!isvalidateotp)
            {
                return Result<string>.BadRequest("Invalid OTP");
            }

            user.Password = command.NewPassword;

            // await Save (likely returns int) and check its value
            var result = await _unitOfWork.Save(cancellationToken);
            if (result <= 0)
            {
                return Result<string>.BadRequest("Failed to update password");
            }

            return Result<string>.Success("Password changed successfully");
        }
    }
}
