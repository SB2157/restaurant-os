namespace RestaurantOS.Application.DTOs.Restaurants;

public sealed class UpdateRestaurantRequest
{
    public string Name { get; set; } = string.Empty;

    public string? LegalName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public string CurrencyCode { get; set; } = "INR";

    public string TimeZoneId { get; set; } = "Asia/Kolkata";

    public bool IsActive { get; set; } = true;
}