using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Querries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository,
    ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PagedResults<RestaurantDto>>
{
    public async Task<PagedResults<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");

        var (restaurants, totalCount) = await restaurantsRepository
            .GetAllMatchingAsync(request.SearchPhrase, request.PageSize, request.PageNumber, request.SortBy, request.SortDirection);


        var dtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);


        var result = new PagedResults<RestaurantDto>(dtos, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}
