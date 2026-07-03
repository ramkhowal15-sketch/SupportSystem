using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Commands.USERS;

public class VerifyOtpCommand : IRequest<Result<string>>
{
    public int OtpCode { get; set; }

    internal class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyOtpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(
            VerifyOtpCommand command,
            CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Repository<Otp>().Entiteis.Where(x => x.OTPCode == command.OtpCode).OrderByDescending(x => x.Time)
                .FirstOrDefaultAsync(cancellationToken);

            if (otp == null)
            {
                return Result<string>.BadRequest("Invalid OTP");
            }

            if (otp.isUsed)
            {
                return Result<string>.BadRequest("OTP already used");
            }


            if (otp.Time.AddMinutes(5) < DateTime.Now)
            {
                return Result<string>.BadRequest("OTP has expired");
            }

            var todayOtpCount = await _unitOfWork.Repository<Otp>()
                .Entiteis
                .Where(x =>
                    x.Id == otp.Id &&
                    x.Time.Date == DateTime.Now.Date)
                .CountAsync(cancellationToken);

            if (todayOtpCount > 10)
            {
                return Result<string>.BadRequest("You have reached your daily limit");
            }

            otp.isUsed = true;

            await _unitOfWork.Save(cancellationToken);

            return Result<string>.Success("OTP verified successfully");
        }
    }
}
