using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.RemoveUserRole;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch]
        [Route("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        [Route("userRole")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [Route("userRole")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> RemoveUserRole(RemoveUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
