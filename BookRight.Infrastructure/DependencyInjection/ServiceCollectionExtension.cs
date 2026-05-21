using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Persistence;
using BookRight.Infrastructure.Repositories;
using BookRight.UseCases.Queries.Kalender;
using BookRight.UseCases.Queries.Kundehistorik;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookRight.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database / EF Core
            services.AddDbContext<BookRightDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IKundeRepository, KundeRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBehandlerRepository, BehandlerRepository>();
            services.AddScoped<IKlinikRepository, KlinikRepository>();
            services.AddScoped<IBehandlingstypeRepository, BehandlingstypeRepository>();
            services.AddScoped<IKampagneRepository, KampagneRepository>();
            services.AddScoped<IBookingStatusRepository, BookingRepository>();
            services.AddScoped<IBookingQueryRepository, BookingRepository>();
            services.AddScoped<IKundehistorikQueryRepository, BookingRepository>();

            return services;
        }
    }
}