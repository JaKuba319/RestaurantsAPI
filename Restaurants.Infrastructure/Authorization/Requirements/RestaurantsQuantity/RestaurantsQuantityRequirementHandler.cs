using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements.RestaurantsQuantity
{
    public class RestaurantsQuantityRequirementHandler(
        ILogger<RestaurantsQuantityRequirementHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IUserContext userContext
        ) : AuthorizationHandler<RestaurantsQuantityRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RestaurantsQuantityRequirement requirement)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation($"User: {user.Email} handles RestaurantsQuantityRequirement");

            var restaurants = await restaurantsRepository.GetAllAsync();

            if ( restaurants == null )
            {
                context.Fail();
                logger.LogWarning("There are no restaurants in database");
                return;
            }

            var userRestaurantsQuantity = restaurants.Count(r => r.OwnerId == user.Id);

            if( userRestaurantsQuantity >= requirement.MinimumQuantity )
            {
                logger.LogInformation("Authorization succeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

        }
    }
}
