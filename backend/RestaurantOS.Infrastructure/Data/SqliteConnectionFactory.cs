using System.Data;
using Microsoft.Data.Sqlite;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.Infrastructure.Data;

public sealed class SqliteConnectionFactory : IDbConnectionFactory
{
    private readonly IDatabasePathProvider _databasePathProvider;

    public SqliteConnectionFactory(
        IDatabasePathProvider databasePathProvider)
    {
        _databasePathProvider = databasePathProvider;
    }

    public IDbConnection CreateConnection()
    {
        var databasePath = _databasePathProvider.GetDatabasePath();

        var connectionString =
            $"Data Source={databasePath}";

        return new SqliteConnection(connectionString);
    }
}