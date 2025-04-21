using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationProjectCollaboratorTests;

public class AssociationProjectCollaboratorConstructorTests
{
    [Fact]
    public void WhenPassingArguments_ThenClassIsInstatiated()
    {
        // Arrange
        PeriodDate periodDate = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        // Act
        var result = new AssociationProjectCollaborator(It.IsAny<long>(), It.IsAny<long>(), periodDate);

        // Assert
        Assert.NotNull(result);
    }
}