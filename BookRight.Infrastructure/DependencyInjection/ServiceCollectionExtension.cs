using Microsoft.Extensions.DependencyInjection;
using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Repositories;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BookRight.UseCases.Commands;
using BookRight.Facade.Interfaces;
using BookRight.Facade.Services;
namespace BookRight.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookRightDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IKundeRepository, KundeRepository>();
            services.AddScoped<OpretKundeHandler>();
            services.AddScoped<IKundeFacade, KundeFacade>();
            return services;
        }
    }
}
