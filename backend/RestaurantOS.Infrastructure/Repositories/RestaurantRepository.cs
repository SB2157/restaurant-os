using Dapper;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Infrastructure.Repositories;

public sealed class RestaurantRepository : IRestaurantRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public RestaurantRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Restaurant?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                Id,
                Name,
                LegalName,
                Phone,
                Email,
                Address,
                City,
                State,
                PostalCode,
                CurrencyCode,
                TimeZoneId,
                IsActive,
                CreatedAtUtc,
                UpdatedAtUtc
            FROM Restaurants
            WHERE Id = @Id;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Restaurant>(
            new CommandDefinition(
                sql,
                new { Id = id },
                cancellationToken: cancellationToken));
    }

    public async Task<Restaurant?> GetActiveAsync(
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                Id,
                Name,
                LegalName,
                Phone,
                Email,
                Address,
                City,
                State,
                PostalCode,
                CurrencyCode,
                TimeZoneId,
                IsActive,
                CreatedAtUtc,
                UpdatedAtUtc
            FROM Restaurants
            WHERE IsActive = 1
            ORDER BY Id
            LIMIT 1;
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Restaurant>(
            new CommandDefinition(
                sql,
                cancellationToken: cancellationToken));
    }

    public async Task<long> CreateAsync(
        Restaurant restaurant,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO Restaurants
            (
                Name,
                LegalName,
                Phone,
                Email,
                Address,
                City,
                State,
                PostalCode,
                CurrencyCode,
                TimeZoneId,
                IsActive,
                CreatedAtUtc
            )
            VALUES
            (
                @Name,
                @LegalName,
                @Phone,
                @Email,
                @Address,
                @City,
                @State,
                @PostalCode,
                @CurrencyCode,
                @TimeZoneId,
                @IsActive,
                @CreatedAtUtc
            );

            SELECT last_insert_rowid();
            """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.ExecuteScalarAsync<long>(
            new CommandDefinition(
                sql,
                restaurant,
                cancellationToken: cancellationToken));
    }

    public async Task UpdateAsync(
        Restaurant restaurant,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            UPDATE Restaurants
            SET
                Name = @Name,
                LegalName = @LegalName,
                Phone = @Phone,
                Email = @Email,
                Address = @Address,
                City = @City,
                State = @State,
                PostalCode = @PostalCode,
                CurrencyCode = @CurrencyCode,
                TimeZoneId = @TimeZoneId,
                IsActive = @IsActive,
                UpdatedAtUtc = @UpdatedAtUtc
            WHERE Id = @Id;
            """;

        using var connection = _connectionFactory.CreateConnection();

        await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                restaurant,
                cancellationToken: cancellationToken));
    }
}