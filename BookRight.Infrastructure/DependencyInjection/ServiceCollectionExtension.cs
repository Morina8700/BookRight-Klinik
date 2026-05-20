using BookRight.Domain.Interfaces;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Facade.Interfaces;
using BookRight.Facade.Services;
using BookRight.Infrastructure.Persistence;
using BookRight.Infrastructure.Repositories;
using BookRight.UseCases.Commands.Booking.OpretBooking;
using BookRight.UseCases.Commands.Booking.Status.Handlers;
using BookRight.UseCases.Commands.Kunde;
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



            // Handlers
            services.AddScoped<OpretKundeHandler>();
            services.AddScoped<OpretBookingHandler>();
            services.AddScoped<HentKundehistorikHandler>();
            services.AddScoped<AflysBookingHandler>();
            services.AddScoped<AfslutBookingHandler>();
            services.AddScoped<NoShowBookingHandler>();
            services.AddScoped<AnkommetBookingHandler>();
            services.AddScoped<HentBookingHandler>();


            // Facades
            services.AddScoped<IKundeFacade, KundeFacade>();
            services.AddScoped<IBookingFacade, BookingFacade>();

            //RabatBeregner
            services.AddScoped<IRabatBeregner, LoyalitetsRabatBeregner>();
            services.AddScoped<IRabatBeregner, FoedselsdagsRabatBeregner>();
            services.AddScoped<IRabatBeregner, KampagneRabatBeregner>();

            services.AddScoped<RabatBeregnerService>();


            return services;
        }
    }
}