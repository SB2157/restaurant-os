using RestaurantOS.Application.Interfaces;
using RestaurantOS.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IDatabasePathProvider, LocalDatabasePathProvider>();

builder.Services.AddScoped<IDbConnectionFactory, SqliteConnectionFactory>();
builder.Services.AddScoped<IDatabaseInitializer, SqliteDatabaseInitializer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

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