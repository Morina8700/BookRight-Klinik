using System.Reflection;
using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Interfaces;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.UseCases.Commands;
using Moq;

namespace BookRight.UseCases.Tests.Commands;

public class OpretBookingHandlerTests
{
    private static readonly DateTime StartTid = new(2026, 6, 1, 10, 0, 0);
    private static readonly DateTime SlutTid = new(2026, 6, 1, 11, 0, 0);

    [Fact]
    public async Task HandleAsync_KlinikIkkeFundet_KasterInvalidOperationException()
    {
        // Arrange
        var klinikRepo = new Mock<IKlinikRepository>();
        klinikRepo.Setup(r => r.HentEfterIdAsync(It.IsAny<Guid>())).ReturnsAsync((Klinik?)null);

        var handler = OpretHandler(klinikRepo: klinikRepo.Object);
        var command = OpretCommand();

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(command));
        Assert.Equal("Klinik ikke fundet", ex.Message);
    }

    [Fact]
    public async Task HandleAsync_BehandlerIkkeFundet_KasterInvalidOperationException()
    {
        // Arrange
        var klinik = new Klinik("Test Klinik", "Adresse 1", 2);
        var klinikRepo = new Mock<IKlinikRepository>();
        klinikRepo.Setup(r => r.HentEfterIdAsync(klinik.KlinikId)).ReturnsAsync(klinik);

        var behandlerRepo = new Mock<IBehandlerRepository>();
        behandlerRepo.Setup(r => r.HentEfterIdAsync(It.IsAny<Guid>())).ReturnsAsync((Behandler?)null);

        var handler = OpretHandler(klinikRepo: klinikRepo.Object, behandlerRepo: behandlerRepo.Object);
        var command = OpretCommand(klinikId: klinik.KlinikId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(command));
        Assert.Equal("Behandler ikke fundet", ex.Message);
    }

    [Fact]
    public async Task HandleAsync_BehandlingstypeIkkeFundet_KasterInvalidOperationException()
    {
        // Arrange
        var (klinik, behandler, _, _) = OpretGyldigeEntiteter();
        var repos = OpretReposMedEntiteter(klinik, behandler, behandlingstype: null, kunde: null);
        repos.BehandlingstypeRepo
            .Setup(r => r.HentEfterIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Behandlingstype?)null);

        var handler = OpretHandler(
            klinikRepo: repos.KlinikRepo.Object,
            behandlerRepo: repos.BehandlerRepo.Object,
            behandlingstypeRepo: repos.BehandlingstypeRepo.Object);

        var command = OpretCommand(
            klinikId: klinik.KlinikId,
            behandlerId: behandler.BehandlerId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(command));
        Assert.Equal("Behandlingstype ikke fundet", ex.Message);
    }

    [Fact]
    public async Task HandleAsync_KundeIkkeFundet_KasterInvalidOperationException()
    {
        // Arrange
        var (klinik, behandler, behandlingstype, _) = OpretGyldigeEntiteter();
        var repos = OpretReposMedEntiteter(klinik, behandler, behandlingstype, kunde: null);
        repos.KundeRepo
            .Setup(r => r.HentPåIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Kunde?)null);

        var handler = OpretHandler(
            klinikRepo: repos.KlinikRepo.Object,
            behandlerRepo: repos.BehandlerRepo.Object,
            behandlingstypeRepo: repos.BehandlingstypeRepo.Object,
            kundeRepo: repos.KundeRepo.Object);

        var command = OpretCommand(
            klinikId: klinik.KlinikId,
            behandlerId: behandler.BehandlerId,
            behandlingstypeId: behandlingstype.BehandlingstypeId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(command));
        Assert.Equal("Kunde ikke fundet", ex.Message);
    }

    [Fact]
    public async Task HandleAsync_BehandlerArbejderIkkePaaKlinik_KasterInvalidOperationException()
    {
        // Arrange
        var klinik = new Klinik("Test Klinik", "Adresse 1", 2);
        var behandler = new Behandler(
            "Lars", "Læge", "lars@test.dk", "11111111", "AUT-1", AutorisationsType.Fysioterapeut);
        var behandlingstype = new Behandlingstype(
            "Fysioterapi", 60, 500m, AutorisationsType.Fysioterapeut, BehandlingsType.Fysioterapi);
        var kunde = new Kunde(
            "Anna", "Hansen", "anna@test.dk", "22222222", new DateOnly(1990, 5, 15), "Testvej 1", "");

        var repos = OpretReposMedEntiteter(klinik, behandler, behandlingstype, kunde);
        var handler = OpretHandler(
            klinikRepo: repos.KlinikRepo.Object,
            behandlerRepo: repos.BehandlerRepo.Object,
            behandlingstypeRepo: repos.BehandlingstypeRepo.Object,
            kundeRepo: repos.KundeRepo.Object);

        var command = OpretCommand(
            klinikId: klinik.KlinikId,
            behandlerId: behandler.BehandlerId,
            behandlingstypeId: behandlingstype.BehandlingstypeId,
            kundeId: kunde.KundeId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(command));
        Assert.Equal("Behandler arbejder ikke på den valgte klinik", ex.Message);
    }

    [Fact]
    public async Task HandleAsync_BehandlerIkkeAutoriseret_KasterInvalidOperationException()
    {
        // Arrange — behandler på klinik, men uden behandlingstype på CV
        var klinik = new Klinik("Test Klinik", "Adresse 1", 2);
        var behandler = new Behandler(
            "Lars", "Læge", "lars@test.dk", "11111111", "AUT-1", AutorisationsType.Fysioterapeut);
        var behandlingstype = new Behandlingstype(
            "Fysioterapi", 60, 500m, AutorisationsType.Fysioterapeut, BehandlingsType.Fysioterapi);
        var kunde = new Kunde(
            "Anna", "Hansen", "anna@test.dk", "22222222", new DateOnly(1990, 5, 15), "Testvej 1", "");
        TilfoejKlinikTilBehandler(behandler, klinik);

        var repos = OpretReposMedEntiteter(klinik, behandler, behandlingstype, kunde);
        var handler = OpretHandler(
            klinikRepo: repos.KlinikRepo.Object,
            behandlerRepo: repos.BehandlerRepo.Object,
            behandlingstypeRepo: repos.BehandlingstypeRepo.Object,
            kundeRepo: repos.KundeRepo.Object);

        var command = OpretCommand(
            klinikId: klinik.KlinikId,
            behandlerId: behandler.BehandlerId,
            behandlingstypeId: behandlingstype.BehandlingstypeId,
            kundeId: kunde.KundeId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(command));
        Assert.Equal("Behandler er ikke autoriseret til behandlingstypen", ex.Message);
    }

    [Fact]
    public async Task HandleAsync_BehandlerIkkeLedig_KasterInvalidOperationException()
    {
        // Arrange
        var (klinik, behandler, behandlingstype, kunde) = OpretGyldigeEntiteter();
        var repos = OpretReposMedEntiteter(klinik, behandler, behandlingstype, kunde);
        repos.BookingRepo
            .Setup(r => r.ErBehandlerLedigAsync(behandler.BehandlerId, StartTid, SlutTid))
            .ReturnsAsync(false);

        var handler = OpretHandler(
            bookingRepo: repos.BookingRepo.Object,
            klinikRepo: repos.KlinikRepo.Object,
            behandlerRepo: repos.BehandlerRepo.Object,
            behandlingstypeRepo: repos.BehandlingstypeRepo.Object,
            kundeRepo: repos.KundeRepo.Object,
            kampagneRepo: repos.KampagneRepo.Object);

        var command = OpretCommand(
            klinikId: klinik.KlinikId,
            behandlerId: behandler.BehandlerId,
            behandlingstypeId: behandlingstype.BehandlingstypeId,
            kundeId: kunde.KundeId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(command));
        Assert.Equal("Behandler er allerede booket i dette tidsrum", ex.Message);
    }

    [Fact]
    public async Task HandleAsync_IngenLedigeRum_KasterInvalidOperationException()
    {
        // Arrange
        var (klinik, behandler, behandlingstype, kunde) = OpretGyldigeEntiteter();
        var repos = OpretReposMedEntiteter(klinik, behandler, behandlingstype, kunde);
        repos.BookingRepo
            .Setup(r => r.ErBehandlerLedigAsync(behandler.BehandlerId, StartTid, SlutTid))
            .ReturnsAsync(true);
        repos.BookingRepo
            .Setup(r => r.HentAntalOverlappendeBookingerAsync(klinik.KlinikId, StartTid, SlutTid))
            .ReturnsAsync(klinik.AntalRum);

        var handler = OpretHandler(
            bookingRepo: repos.BookingRepo.Object,
            klinikRepo: repos.KlinikRepo.Object,
            behandlerRepo: repos.BehandlerRepo.Object,
            behandlingstypeRepo: repos.BehandlingstypeRepo.Object,
            kundeRepo: repos.KundeRepo.Object,
            kampagneRepo: repos.KampagneRepo.Object);

        var command = OpretCommand(
            klinikId: klinik.KlinikId,
            behandlerId: behandler.BehandlerId,
            behandlingstypeId: behandlingstype.BehandlingstypeId,
            kundeId: kunde.KundeId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(command));
        Assert.Equal("Klinikken har ingen ledige rum i det valgte tidsrum", ex.Message);
    }

    [Fact]
    public async Task HandleAsync_GyldigBooking_GemmerOgReturnererResultat()
    {
        // Arrange
        var (klinik, behandler, behandlingstype, kunde) = OpretGyldigeEntiteter();
        var repos = OpretReposMedEntiteter(klinik, behandler, behandlingstype, kunde);
        repos.BookingRepo
            .Setup(r => r.ErBehandlerLedigAsync(behandler.BehandlerId, StartTid, SlutTid))
            .ReturnsAsync(true);
        repos.BookingRepo
            .Setup(r => r.HentAntalOverlappendeBookingerAsync(klinik.KlinikId, StartTid, SlutTid))
            .ReturnsAsync(0);
        repos.KampagneRepo
            .Setup(r => r.HentAktiveKampagnerAsync(It.IsAny<DateOnly>()))
            .ReturnsAsync(Array.Empty<Kampagne>());

        var handler = OpretHandler(
            bookingRepo: repos.BookingRepo.Object,
            klinikRepo: repos.KlinikRepo.Object,
            behandlerRepo: repos.BehandlerRepo.Object,
            behandlingstypeRepo: repos.BehandlingstypeRepo.Object,
            kundeRepo: repos.KundeRepo.Object,
            kampagneRepo: repos.KampagneRepo.Object);

        var command = OpretCommand(
            klinikId: klinik.KlinikId,
            behandlerId: behandler.BehandlerId,
            behandlingstypeId: behandlingstype.BehandlingstypeId,
            kundeId: kunde.KundeId);

        // Act
        var resultat = await handler.HandleAsync(command);

        // Assert
        Assert.True(resultat.Success);
        Assert.Equal(500m, resultat.PrisUdenRabat);
        Assert.Equal(500m, resultat.PrisMedRabat);
        Assert.Equal("Ingen", resultat.AnvendtRabatType);

        repos.BookingRepo.Verify(
            r => r.AddAsync(It.Is<Booking>(b =>
                b.KundeId == kunde.KundeId &&
                b.BehandlerId == behandler.BehandlerId &&
                b.KlinikId == klinik.KlinikId)),
            Times.Once);
    }

    private static OpretBookingHandler OpretHandler(
        IBookingRepository? bookingRepo = null,
        IKlinikRepository? klinikRepo = null,
        IBehandlerRepository? behandlerRepo = null,
        IBehandlingstypeRepository? behandlingstypeRepo = null,
        IKundeRepository? kundeRepo = null,
        IKampagneRepository? kampagneRepo = null)
    {
        return new OpretBookingHandler(
            bookingRepo ?? new Mock<IBookingRepository>().Object,
            klinikRepo ?? new Mock<IKlinikRepository>().Object,
            behandlerRepo ?? new Mock<IBehandlerRepository>().Object,
            behandlingstypeRepo ?? new Mock<IBehandlingstypeRepository>().Object,
            new RabatBeregnerService(Enumerable.Empty<IRabatBeregner>()),
            kundeRepo ?? new Mock<IKundeRepository>().Object,
            kampagneRepo ?? new Mock<IKampagneRepository>().Object);
    }

    private static OpretBookingCommand OpretCommand(
        Guid? kundeId = null,
        Guid? behandlerId = null,
        Guid? klinikId = null,
        Guid? behandlingstypeId = null) =>
        new(
            kundeId ?? Guid.NewGuid(),
            behandlerId ?? Guid.NewGuid(),
            klinikId ?? Guid.NewGuid(),
            behandlingstypeId ?? Guid.NewGuid(),
            StartTid,
            SlutTid);

    private static (Klinik klinik, Behandler behandler, Behandlingstype behandlingstype, Kunde kunde) OpretGyldigeEntiteter()
    {
        var klinik = new Klinik("Test Klinik", "Adresse 1", 2);
        var behandler = new Behandler(
            "Lars", "Læge", "lars@test.dk", "11111111", "AUT-1", AutorisationsType.Fysioterapeut);
        var behandlingstype = new Behandlingstype(
            "Fysioterapi", 60, 500m, AutorisationsType.Fysioterapeut, BehandlingsType.Fysioterapi);
        var kunde = new Kunde(
            "Anna", "Hansen", "anna@test.dk", "22222222", new DateOnly(1990, 5, 15), "Testvej 1", "");

        TilfoejKlinikTilBehandler(behandler, klinik);
        TilfoejBehandlingstypeTilBehandler(behandler, behandlingstype);

        return (klinik, behandler, behandlingstype, kunde);
    }

    private static (
        Mock<IBookingRepository> BookingRepo,
        Mock<IKlinikRepository> KlinikRepo,
        Mock<IBehandlerRepository> BehandlerRepo,
        Mock<IBehandlingstypeRepository> BehandlingstypeRepo,
        Mock<IKundeRepository> KundeRepo,
        Mock<IKampagneRepository> KampagneRepo) OpretReposMedEntiteter(
        Klinik klinik,
        Behandler behandler,
        Behandlingstype? behandlingstype,
        Kunde? kunde)
    {
        var bookingRepo = new Mock<IBookingRepository>();
        var klinikRepo = new Mock<IKlinikRepository>();
        var behandlerRepo = new Mock<IBehandlerRepository>();
        var behandlingstypeRepo = new Mock<IBehandlingstypeRepository>();
        var kundeRepo = new Mock<IKundeRepository>();
        var kampagneRepo = new Mock<IKampagneRepository>();

        klinikRepo.Setup(r => r.HentEfterIdAsync(klinik.KlinikId)).ReturnsAsync(klinik);
        behandlerRepo.Setup(r => r.HentEfterIdAsync(behandler.BehandlerId)).ReturnsAsync(behandler);

        if (behandlingstype is not null)
            behandlingstypeRepo.Setup(r => r.HentEfterIdAsync(behandlingstype.BehandlingstypeId)).ReturnsAsync(behandlingstype);

        if (kunde is not null)
            kundeRepo.Setup(r => r.HentPåIdAsync(kunde.KundeId)).ReturnsAsync(kunde);

        return (bookingRepo, klinikRepo, behandlerRepo, behandlingstypeRepo, kundeRepo, kampagneRepo);
    }

    private static void TilfoejKlinikTilBehandler(Behandler behandler, Klinik klinik)
    {
        var felt = typeof(Behandler).GetField("_klinikker", BindingFlags.Instance | BindingFlags.NonPublic);
        var liste = (List<Klinik>)felt!.GetValue(behandler)!;
        liste.Add(klinik);
    }

    private static void TilfoejBehandlingstypeTilBehandler(Behandler behandler, Behandlingstype behandlingstype)
    {
        var felt = typeof(Behandler).GetField("_behandlingstyper", BindingFlags.Instance | BindingFlags.NonPublic);
        var liste = (List<Behandlingstype>)felt!.GetValue(behandler)!;
        liste.Add(behandlingstype);
    }
}
