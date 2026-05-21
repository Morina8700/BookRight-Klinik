using BookRight.Facade.DependencyInjection;
using BookRight.Infrastructure.DependencyInjection;
using BookRight.UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure (DbContext + repositories)
builder.Services.AddInfrastructure(builder.Configuration);
// BookRight (Use cases + facades)
builder.Services.AddBookRight();

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