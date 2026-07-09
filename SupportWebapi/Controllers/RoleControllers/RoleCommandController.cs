using Application.Features.Roles.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi;

namespace SupportWebapi.Controllers.RoleControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return Responsehelper.GenerateResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleCommad commad)
        {
            var response = await _mediator.Send(commad);
            return Responsehelper.GenerateResponse(response);
        }

        [HttpDelete("{id:guid}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteRoleCommand(id));
            return Responsehelper.GenerateResponse(response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(AssigndRoleToUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Responsehelper.GenerateResponse(response);
        }

        [HttpPost("RemoveRole")]
        public async Task<IActionResult> RemoveRole(RemoveRoleFromUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Responsehelper.GenerateResponse(response);
        }
    }
}
