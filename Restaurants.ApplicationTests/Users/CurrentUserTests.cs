using FluentAssertions;
using Restaurants.Domain.Constants;
using System.Runtime.InteropServices;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    // TestMethod_Scenario_ExpectedResult
    [Theory()]
    [InlineData(UserRole.Admin)]
    [InlineData(UserRole.User)]
    public void IsInRoleTest_WithMatchingRole_ShouldReturnTrue(string role)
    {
        // arrange
        var currentUser = new CurrentUser("1", "email@email.com", [UserRole.Admin, UserRole.User], null, null);

        // act

        var isInRole = currentUser.IsInRole(role);

        // assert
        
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRoleTest_WithNoMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "email@email.com", [UserRole.Admin, UserRole.User], null, null);

        // act

        var isInRole = currentUser.IsInRole(UserRole.Owner);

        // assert

        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRoleTest_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "email@email.com", [UserRole.Admin, UserRole.User], null, null);

        // act

        var isInRole = currentUser.IsInRole(UserRole.Admin.ToLower());

        // assert

        isInRole.Should().BeFalse();
    }
}