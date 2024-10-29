using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishForRestaurant;

public class DeleteDishForRestaurantCommand(int restaurantId, int dishId) : IRequest
{
    public int RestaurantId { get; set; } = restaurantId;
    public int DishId { get; set; } = dishId;
}
