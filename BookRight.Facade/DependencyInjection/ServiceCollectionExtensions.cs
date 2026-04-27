using BookRight.Facade.Interfaces;
using BookRight.Facade.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookRight.Facade.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFacade(this IServiceCollection services)
        {
            services.AddScoped<IKundeFacade, KundeFacade>();
            return services;
        }
    }
}
