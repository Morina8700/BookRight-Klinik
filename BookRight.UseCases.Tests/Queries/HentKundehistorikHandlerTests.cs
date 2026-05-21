using BookRight.Domain.Enums;
using BookRight.UseCases.Queries.Kundehistorik;
using Moq;

namespace BookRight.UseCases.Tests.Queries;

public class HentKundehistorikHandlerTests
{
    [Fact]
    public async Task HandleAsync_TomKundeId_KasterArgumentException()
    {
        // Arrange
        var queryRepo = new Mock<IKundehistorikQueryRepository>();
        var handler = new HentKundehistorikHandler(queryRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => handler.HandleAsync(Guid.Empty));

        queryRepo.Verify(
            r => r.HentForKundeAsync(It.IsAny<Guid>()),
            Times.Never);
    }

    [Fact]
    public async Task HandleAsync_ReturnererHistorikFraRepository()
    {
        // Arrange
        var kundeId = Guid.NewGuid();
        var forventet = new List<KundehistorikPost>
        {
            new(
                Guid.NewGuid(),
                DateTime.Today.AddHours(10),
                DateTime.Today.AddHours(11),
                "Massage",
                "Dr. Test",
                "BookRight København",
                BookingStatus.Afsluttet,
                500m,
                null)
        };

        var queryRepo = new Mock<IKundehistorikQueryRepository>();
        queryRepo
            .Setup(r => r.HentForKundeAsync(kundeId))
            .ReturnsAsync(forventet);

        var handler = new HentKundehistorikHandler(queryRepo.Object);

        // Act
        var resultat = await handler.HandleAsync(kundeId);

        // Assert
        Assert.Equal(forventet, resultat.ToList());
        queryRepo.Verify(r => r.HentForKundeAsync(kundeId), Times.Once);
    }
}