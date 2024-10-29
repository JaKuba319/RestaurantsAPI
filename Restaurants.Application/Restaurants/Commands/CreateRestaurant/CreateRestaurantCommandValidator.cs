using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name).Length(3, 100).WithMessage("Name should be at least 3 characters long.");

        RuleFor(dto => dto.Description).NotEmpty().
            WithMessage("Description is requierd.");

        RuleFor(dto => dto.Category).NotEmpty().
            WithMessage("Category is requierd.");

        RuleFor(dto => dto.ContactEmail).EmailAddress()
            .WithMessage("Email addres is not valid.");

        RuleFor(dto => dto.ContactNumber).Matches(@"^\d{9}$")
            .WithMessage("Phone number should be in XXXXXXXXX  format");

        RuleFor(dto => dto.PostalCode).Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Postal code should be in XX-XXX format.");
    }
}
