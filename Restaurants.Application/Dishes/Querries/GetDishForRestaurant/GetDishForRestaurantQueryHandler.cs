using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Querries.GetDishForRestaurant;

public class GetDishForRestaurantQueryHandler 
    (
    ILogger<GetDishForRestaurantQueryHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantRepository
    ) : IRequestHandler<GetDishForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting single dish with id: {DishId} for restaurant with id: {RestaurantId}",
            request.DishId, request.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var result = restaurant.Dishes.FirstOrDefault(dish => dish.Id == request.DishId);

        if (result == null)
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        var dto = mapper.Map<DishDto>(result);

        return dto;
    }
}
