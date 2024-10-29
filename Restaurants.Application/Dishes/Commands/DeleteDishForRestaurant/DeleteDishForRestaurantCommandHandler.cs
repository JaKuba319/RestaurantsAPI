using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;

public class DeleteDishForRestaurantCommandHandler(ILogger<DeleteDishForRestaurantCommandHandler> logger,
    IDishesRepository dishesRepository,
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishForRestaurantCommand>
{
    public async Task Handle(DeleteDishForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing dish with id: {DishId} for restaurant with id: {RestaurantId}", request.DishId, request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dish = restaurant.Dishes.FirstOrDefault(dish => dish.Id == request.DishId);

        if (dish is null)
        {
            throw new NotFoundException(nameof(Dish),request.DishId.ToString());
        }

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        await dishesRepository.Delete(dish);
    }
}
