using Domain.Models;
using Moq;

namespace Domain.Tests.UserTests;

public class UserGetNamesTests
{
    [Fact]
    public void WhenGettingNames_ThenReturnsNames()
    {
        //arrange
        var name = "John";
        var user = new User(1, name, "Doe", "john@email.com", It.IsAny<PeriodDateTime>());

        //act
        var userName = user.GetNames();

        //assert
        Assert.Equal(name, userName);
    }
}