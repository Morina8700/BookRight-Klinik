using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Facade.Interfaces;
using BookRight.Facade.Services;
using BookRight.Infrastructure.DependencyInjection;
using BookRight.UI.Components;
using BookRight.UseCases.Commands.Booking.OpretBooking;
using BookRight.UseCases.Commands.Booking.Status.Handlers;
using BookRight.UseCases.Commands.Kunde;
using BookRight.UseCases.Queries.Kalender;
using BookRight.UseCases.Queries.Kundehistorik;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure (DbContext + repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// Use cases — handlers
builder.Services.AddScoped<OpretKundeHandler>();
builder.Services.AddScoped<OpretBookingHandler>();
builder.Services.AddScoped<HentKundehistorikHandler>();
builder.Services.AddScoped<AflysBookingHandler>();
builder.Services.AddScoped<AfslutBookingHandler>();
builder.Services.AddScoped<NoShowBookingHandler>();
builder.Services.AddScoped<AnkommetBookingHandler>();
builder.Services.AddScoped<HentBookingHandler>();

// Domain — Rabatberegner
builder.Services.AddScoped<IRabatBeregner, LoyalitetsRabatBeregner>();
builder.Services.AddScoped<IRabatBeregner, FoedselsdagsRabatBeregner>();
builder.Services.AddScoped<IRabatBeregner, KampagneRabatBeregner>();
builder.Services.AddScoped<RabatBeregnerService>();

// Facade
builder.Services.AddScoped<IKundeFacade, KundeFacade>();
builder.Services.AddScoped<IBookingFacade, BookingFacade>();

// Blazor 
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();