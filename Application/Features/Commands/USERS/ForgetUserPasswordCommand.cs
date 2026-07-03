using Application.Dtos.MailDto;
using Application.Interfaces.Repositorys;
using Application.Interfaces.Services.MailServices;
using Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Commands.USERS;

public class ForgetUserPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }


    internal class ForgetUserPasswordhandler : IRequestHandler<ForgetUserPasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;

        public ForgetUserPasswordhandler(IUnitOfWork unitOfWork, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        public async Task<Result<string>> Handle(ForgetUserPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().Entiteis.FirstOrDefaultAsync(x => x.Email == command.Email);

            if (user == null)
            {
                return Result<string>.BadRequest("User Not Found");
            }

            var random = new Random();
            int otpCode = random.Next(1000, 9999);

            var otp = new Otp
            {
                OTPCode = otpCode,
                Time = DateTime.Now,
                isUsed = false     
            };

            await _unitOfWork.Repository<Otp>().PostAsync(otp);
            await _unitOfWork.Save(cancellationToken);


            //string Subject = "Forgot-Password OTP Verification";
            //string Body = ("Hi this Side Otps service Handler,We Send you Otp for Forgot-Password" + otp);

            var maildto = new MailDTO
            {
                To = command.Email,
                Subject = "Forgot-Password OTP Verification",
                Body = ("<h1>Hi <br> this Side Otps service Handler,<br> We Send you Otp for Forgot-Password </h1>" + otp.OTPCode)
            };

            await _mailService.Sendmail(maildto);
            return Result<string>.Success("OTP Sent Successfully");


        }
    }
}

