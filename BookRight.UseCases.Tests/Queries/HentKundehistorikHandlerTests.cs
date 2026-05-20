using BookRight.Domain.Interfaces;
using BookRight.Domain.Models;
using BookRight.UseCases.Queries.Kundehistorik;
using Moq;

namespace BookRight.UseCases.Tests.Queries;

public class HentKundehistorikHandlerTests
{
    [Fact]
    public async Task HandleAsync_TomKundeId_KasterArgumentException()
    {
        // Arrange
        var bookingRepo = new Mock<IBookingRepository>();
        var handler = new HentKundehistorikHandler(bookingRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => handler.HandleAsync(Guid.Empty));

        bookingRepo.Verify(
            r => r.HentKundehistorikAsync(It.IsAny<Guid>()),
            Times.Never);
    }

    [Fact]
    public async Task HandleAsync_ReturnererHistorikFraRepository()
    {
        // Arrange
        var kundeId = Guid.NewGuid();
        var forventet = new List<KundehistorikPost>
        {
            new()
            {
                BookingId = Guid.NewGuid(),
                StartTid = DateTime.Today.AddHours(10),
                SlutTid = DateTime.Today.AddHours(11),
                BehandlingstypeNavn = "Massage",
                BehandlerNavn = "Dr. Test",
                KlinikNavn = "BookRight København",
                PrisMedRabat = 500m
            }
        };

        var bookingRepo = new Mock<IBookingRepository>();
        bookingRepo
            .Setup(r => r.HentKundehistorikAsync(kundeId))
            .ReturnsAsync(forventet);

        var handler = new HentKundehistorikHandler(bookingRepo.Object);

        // Act
        var resultat = await handler.HandleAsync(kundeId);

        // Assert
        Assert.Same(forventet, resultat);
        bookingRepo.Verify(r => r.HentKundehistorikAsync(kundeId), Times.Once);
    }
}
