using BookRight.Facade.Interfaces;
using BookRight.Facade.Services;
using BookRight.UseCases.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BookRight.Facade.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookRight(this IServiceCollection services)
    {
        services.AddUseCases();
       
        // Facade
        services.AddScoped<IKundeFacade, KundeFacade>();
        services.AddScoped<IBookingFacade, BookingFacade>();
        services.AddScoped<IBehandlerFacade, BehandlerFacade>();

        return services;
    }
}
