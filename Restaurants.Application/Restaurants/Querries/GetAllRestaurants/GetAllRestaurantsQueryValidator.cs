using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Querries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private string[] allowedSortByColumnNames = [nameof(RestaurantDto.Name),
            nameof(RestaurantDto.Category), 
            nameof(RestaurantDto.Description)];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize).GreaterThanOrEqualTo(1);
            // RuleFor(r => r.PageSize).Must(v => allowedPageSizesArray.Contains(v)).WithMessage("My message");

            RuleFor(r => r.SortBy).Must(v => allowedSortByColumnNames.Contains(v))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(',', allowedSortByColumnNames)}]");
        }
    }
}
