using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Infrastructure.Data;

public sealed class LocalDatabasePathProvider : IDatabasePathProvider
{
    public string GetDatabasePath()
    {
        var localAppData = Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData);

        var dataDirectory = Path.Combine(
            localAppData,
            "RestaurantOS",
            "Data");

        Directory.CreateDirectory(dataDirectory);

        return Path.Combine(dataDirectory, "restaurant.db");
    }
}