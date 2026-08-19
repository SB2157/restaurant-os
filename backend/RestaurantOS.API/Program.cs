using RestaurantOS.Application.Interfaces;
using RestaurantOS.Infrastructure.Data;
using RestaurantOS.Infrastructure.Repositories;
using RestaurantOS.Application.Services;
using RestaurantOS.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy =
            System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddSingleton<IDatabasePathProvider, LocalDatabasePathProvider>();

builder.Services.AddScoped<IDbConnectionFactory, SqliteConnectionFactory>();
builder.Services.AddScoped<IDatabaseInitializer, SqliteDatabaseInitializer>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();



var app = builder.Build();

app.UseMiddleware<RestaurantExceptionMiddleware>();

app.MapControllers();

await InitializeDatabaseAsync(app);

app.Run();

static async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var initializer = scope.ServiceProvider
        .GetRequiredService<IDatabaseInitializer>();

    await initializer.InitializeAsync();
}