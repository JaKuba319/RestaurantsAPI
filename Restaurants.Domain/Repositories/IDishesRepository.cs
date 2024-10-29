using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<int> CreateDish(Dish entity);
    Task Delete(Dish entity);
}
