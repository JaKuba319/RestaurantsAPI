namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string type, string identifier) : Exception($"Resource: [{type}] with id: {identifier} was not found!")
{
    
}
