using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;
using Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Querries.GetDishesForRestaurant;
using Restaurants.Application.Dishes.Querries.GetDishForRestaurant;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("/api/restaurant/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

            int dishId = await mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdForRestaurant), new {restaurantId, dishId}, null);
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.AtLeast21)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

            return Ok(dishes);
        }

        [HttpGet]
        [Route("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dishes = await mediator.Send(new GetDishForRestaurantQuery(restaurantId, dishId));

            return Ok(dishes);
        }

        [HttpDelete]
        [Route("{dishId}")]
        public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await mediator.Send(new DeleteDishForRestaurantCommand(restaurantId, dishId));
           
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }
    }


}
