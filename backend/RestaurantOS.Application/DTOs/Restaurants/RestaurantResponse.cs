namespace RestaurantOS.Application.DTOs.Restaurants;

public sealed class RestaurantResponse
{
    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? LegalName { get; init; }

    public string? Phone { get; init; }

    public string? Email { get; init; }

    public string? Address { get; init; }

    public string? City { get; init; }

    public string? State { get; init; }

    public string? PostalCode { get; init; }

    public string CurrencyCode { get; init; } = "INR";

    public string TimeZoneId { get; init; } = "Asia/Kolkata";

    public bool IsActive { get; init; }

    public DateTime CreatedAtUtc { get; init; }

    public DateTime? UpdatedAtUtc { get; init; }
}