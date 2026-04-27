using BookRight.UseCases.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BookRight.UseCases.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<OpretKundeHandler>();
            return services;
        }
    }
}
