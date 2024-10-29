using FluentValidation;
using Restaurants.Application.Dishes.Commands.CreateDish;

namespace Restaurants.Application.Dishes.Commands;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(dish => dish.KiloCalories).GreaterThanOrEqualTo(0).WithMessage("KiloCalories must be a positive numer");

        RuleFor(dish => dish.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be a positive numer");
    }
}
