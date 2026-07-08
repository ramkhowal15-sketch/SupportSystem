using Application.Features.Users.UserCommands;
using Application.Features.Users.UserQuerries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi;

namespace SupportWebapi.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Responsehelper.GenerateResponse(response);
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GelAllUserQurrey());
            return Responsehelper.GenerateResponse(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQurrey(id));
            return Responsehelper.GenerateResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateUserCommand createUser)
        {
            var response = await _mediator.Send(new UpdateUserCommand(id, createUser));
            return Responsehelper.GenerateResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));
            return Responsehelper.GenerateResponse(response);
        }

    }
}



