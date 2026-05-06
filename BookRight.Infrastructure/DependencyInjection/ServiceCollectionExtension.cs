using BookRight.Domain.Interfaces;
using BookRight.Domain.Strategies.Rabatberegner;
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

            // Repositories
            services.AddScoped<IKundeRepository, KundeRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBehandlerRepository, BehandlerRepository>();
            services.AddScoped<IKlinikRepository, KlinikRepository>();
            services.AddScoped<IBehandlingstypeRepository, BehandlingstypeRepository>();
            services.AddScoped<IKampagneRepository, KampagneRepository>();


            // Handlers
            services.AddScoped<OpretKundeHandler>();
            services.AddScoped<OpretBookingHandler>();

            // Facades
            services.AddScoped<IKundeFacade, KundeFacade>();
            services.AddScoped<IBookingFacade, BookingFacade>();

            //RabatBeregner
            services.AddScoped<IRabatBeregner, LoyalitetsRabatBeregner>();
            services.AddScoped<IRabatBeregner, FoedselsdagsRabatBeregner>();
            services.AddScoped<IRabatBeregner, KampagneRabatBeregner>();

            services.AddScoped<BedsteRabatBeregner>();


            return services;
        }
    }
}