﻿namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast21 = "AtLeast21";
    public const string MinimumRestaurantsQuantity = "MinimumRestaurantsQuantity";
}
public static class AppClaimTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
    public const string OwnedRestaurants = "OwnedRestaurants";
}
