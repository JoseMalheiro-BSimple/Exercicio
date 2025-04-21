using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class AssociationProjectCollaboratorFindAllByProjectAndCollaboratorAsyncTests
{
    [Fact]
    public async Task WhenPassingExistingProjectAndCollaboratorIdCombo_ThenReturnAssociation()
    {
        // Arrange
        // ------------ Setup test in-memory database ------------
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new AbsanteeContext(options);

        PeriodDate period =
            new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

        var assoc1 = new Mock<IAssociationProjectCollaborator>();
        assoc1.Setup(a => a._projectId).Returns(1);
        assoc1.Setup(a => a._collaboratorId).Returns(1);
        assoc1.Setup(a => a._periodDate).Returns(period);
        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);
        context.Associations.Add(assocDM1);

        var assoc2 = new Mock<IAssociationProjectCollaborator>();
        assoc2.Setup(a => a._projectId).Returns(2);
        assoc2.Setup(a => a._collaboratorId).Returns(2);
        assoc2.Setup(a => a._periodDate).Returns(period);
        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);
        context.Associations.Add(assocDM2);

        var assoc3 = new Mock<IAssociationProjectCollaborator>();
        assoc3.Setup(a => a._projectId).Returns(2);
        assoc3.Setup(a => a._collaboratorId).Returns(3);
        assoc3.Setup(a => a._periodDate).Returns(period);
        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);
        context.Associations.Add(assocDM3);

        await context.SaveChangesAsync();

        // -----------------------------------------

        long collabId = 3;
        long projectId = 3;

        var expected = new List<IAssociationProjectCollaborator>();

        Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>> mapper =
            new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();

        mapper.Setup(m => m.ToDomain(new List<AssociationProjectCollaboratorDataModel> { assocDM3 })).Returns(expected);

        var repo = new AssociationProjectCollaboratorRepositoryEF(context, mapper.Object);

        // Act
        var result = await repo.FindAllByProjectAndCollaboratorAsync(projectId, collabId);

        // Assert
        Assert.True(expected.SequenceEqual(result));
    }
}