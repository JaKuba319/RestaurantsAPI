using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name).Length(3, 100).WithMessage("Name should be at least 3 characters long.");

        RuleFor(dto => dto.Description).NotEmpty().
            WithMessage("Description is requierd.");
    }
}
