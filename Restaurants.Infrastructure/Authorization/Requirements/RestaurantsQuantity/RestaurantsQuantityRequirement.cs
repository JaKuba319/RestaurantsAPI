using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.RestaurantsQuantity
{
    public class RestaurantsQuantityRequirement(int minimumQuantity) : IAuthorizationRequirement
    {
        public int MinimumQuantity { get; set; } = minimumQuantity;
    }
}
