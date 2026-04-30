using BookRight.Domain.Interfaces;
using BookRight.Facade.Interfaces;
using BookRight.Facade.Services;
using BookRight.Infrastructure.Persistence;
using BookRight.Infrastructure.Repositories;
using BookRight.UseCases.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BookRight.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookRightDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IKundeRepository, KundeRepository>();
            services.AddScoped<IKundeFacade, KundeFacade>();
            services.AddScoped<OpretKundeHandler>();
            return services;
        }
    }
}
