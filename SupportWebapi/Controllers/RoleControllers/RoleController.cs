using Application.Features.Roles.Queries;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi;

namespace SupportWebapi.Controllers.RoleControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _mediator.Send(new GetAllRolesQuery());
            return Responsehelper.GenerateResponse(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult>GetById(Guid id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery(id));
            return Responsehelper.GenerateResponse(response);
        }

        [HttpGet("ByName/{Name}")]
        public async Task<IActionResult>GetByName(string name)
        {
            var response = await _mediator.Send(new GetRoleByNameQuery(name));
            return Responsehelper.GenerateResponse(response);
        }

        [HttpGet("Users/{roleId:guid}")]
        public async Task<IActionResult>GetUserByRole(Guid roleId)
        {
            var respone = await _mediator.Send(new GetUsersByRoleQuery(roleId));
            return Responsehelper.GenerateResponse(respone);
        }


    }
}
