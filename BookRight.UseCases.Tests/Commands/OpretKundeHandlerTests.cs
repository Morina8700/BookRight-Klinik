using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands.Kunde;
using Moq;

namespace BookRight.UseCases.Tests.Commands;

public class OpretKundeHandlerTests
{
    [Fact]
    public async Task HandleAsync_TilfojerKundeOgReturnererId()
    {
        // Arrange
        var kundeRepo = new Mock<IKundeRepository>();
        kundeRepo
            .Setup(r => r.TilføjAsync(It.IsAny<Kunde>()))
            .Returns(Task.CompletedTask);

        var handler = new OpretKundeHandler(kundeRepo.Object);

        var command = new OpretKundeCommand(
            Fornavn: "Anna",
            Efternavn: "Hansen",
            Email: "anna@test.dk",
            Telefon: "12345678",
            Fødselsdato: new DateOnly(1990, 5, 15),
            Adresse: "Testvej 1",
            Helbredsnotater: "",
            ForetrukkenBehandlerID: Guid.NewGuid());

        // Act
        var kundeId = await handler.HandleAsync(command);

        // Assert
        Assert.NotEqual(Guid.Empty, kundeId);

        kundeRepo.Verify(
            r => r.TilføjAsync(It.Is<Kunde>(k =>
                k.Fornavn == "Anna" && k.Email.Value == "anna@test.dk")),
            Times.Once);
    }
}
