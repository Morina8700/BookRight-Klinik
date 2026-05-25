using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.UseCases.Commands;
using BookRight.UseCases.Commands.Booking.OpretBooking;
using BookRight.UseCases.Commands.Booking.Status.Handlers;
using BookRight.UseCases.Commands.Kunde;
using BookRight.UseCases.Queries.Behandler;
using BookRight.UseCases.Queries.Behandler.Behandlingstype;
using BookRight.UseCases.Queries.Kalender;
using BookRight.UseCases.Queries.Klinik;
using BookRight.UseCases.Queries.Kunde;
using BookRight.UseCases.Queries.Kundehistorik;
using Microsoft.Extensions.DependencyInjection;

namespace BookRight.UseCases.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Use cases — handlers
        services.AddScoped<OpretKundeHandler>();
        services.AddScoped<OpretBehandlerHandler>();
        services.AddScoped<OpretBookingHandler>();
        services.AddScoped<AflysBookingHandler>();
        services.AddScoped<AfslutBookingHandler>();
        services.AddScoped<NoShowBookingHandler>();
        services.AddScoped<AnkommetBookingHandler>();

        // Queries
        services.AddScoped<HentKundehistorikHandler>();
        services.AddScoped<HentBookingHandler>();
        services.AddScoped<HentAlleKunderHandler>();
        services.AddScoped<HentAlleKlinikkerHandler>();
        services.AddScoped<HentAlleBehandlereHandler>();
        services.AddScoped<HentAlleBehandlingstyperHandler>();

        // Domain — Rabatberegner
        services.AddScoped<IRabatBeregner, LoyalitetsRabatBeregner>();
        services.AddScoped<IRabatBeregner, FoedselsdagsRabatBeregner>();
        services.AddScoped<IRabatBeregner, KampagneRabatBeregner>();
        services.AddScoped<RabatBeregnerService>();

        return services;
    }
}
