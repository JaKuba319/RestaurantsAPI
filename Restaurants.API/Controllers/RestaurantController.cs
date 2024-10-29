using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Querries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Querries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("/api/restaurants")]
    [Authorize]
    public class RestaurantController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Policy = PolicyNames.MinimumRestaurantsQuantity)]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaurants = await mediator.Send(query);
            return Ok(restaurants);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await mediator.Send(new GetRestaurantByIdQuery(id));

            return Ok(entity);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Owner)]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
        {

            int id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute]int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));

            return NoContent();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            
            return NoContent();
        }
    }
}
