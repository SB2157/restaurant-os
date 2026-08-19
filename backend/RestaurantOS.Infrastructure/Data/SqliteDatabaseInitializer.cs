using Microsoft.Data.Sqlite;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Infrastructure.Data;

public sealed class SqliteDatabaseInitializer : IDatabaseInitializer
{
    private readonly IDatabasePathProvider _databasePathProvider;

    public SqliteDatabaseInitializer(
        IDatabasePathProvider databasePathProvider)
    {
        _databasePathProvider = databasePathProvider;
    }

    public async Task InitializeAsync(
        CancellationToken cancellationToken = default)
    {
        var databasePath = _databasePathProvider.GetDatabasePath();

        var connectionString =
            $"Data Source={databasePath}";

        await using var connection =
            new SqliteConnection(connectionString);

        await connection.OpenAsync(cancellationToken);

        const string sql = """
            CREATE TABLE IF NOT EXISTS Restaurants
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                LegalName TEXT NULL,
                Phone TEXT NULL,
                Email TEXT NULL,
                Address TEXT NULL,
                City TEXT NULL,
                State TEXT NULL,
                PostalCode TEXT NULL,
                CurrencyCode TEXT NOT NULL DEFAULT 'INR',
                TimeZoneId TEXT NOT NULL DEFAULT 'Asia/Kolkata',
                IsActive INTEGER NOT NULL DEFAULT 1,
                CreatedAtUtc TEXT NOT NULL,
                UpdatedAtUtc TEXT NULL
            );
            """;

        await using var command = connection.CreateCommand();

        command.CommandText = sql;

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}