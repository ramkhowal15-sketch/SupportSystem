using Application.Features.Users.Commands;
using Application.Interfaces.Repositorys;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

      

       

        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Responsehelper.GenerateResponse(response);
        }

        [HttpPost("Forget-password")]

        public async Task<IActionResult> Forgetpassword(ForgetUserPasswordCommand forget)
        {
            var response = await _mediator.Send(forget);
            return Responsehelper.GenerateResponse(response);
        }

        [HttpPost("Verify-Otp")]

        public async Task<IActionResult> Verifyotp(VerifyOtpCommand command)
        {
            var response = await _mediator.Send(command);
            return Responsehelper.GenerateResponse(response);
        }

        [HttpPost("Reset-password")]

        public async Task<IActionResult> ResetPassword(ResetUserPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return Responsehelper.GenerateResponse(response);
        }

        [HttpGet]

        public async Task<IActionResult> Getall()
        {
            var users = await _unitOfWork.Repository<User,int>().GetAll();
            var result = Shared.Result<List<User>>.Success(users, "Success");
            return Responsehelper.GenerateResponse(result);
        }


    }
}
