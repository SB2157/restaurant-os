namespace RestaurantOS.Infrastructure.Configuration;

public sealed class DatabaseOptions
{
    public const string SectionName = "ConnectionStrings";

    public string RestaurantDatabase { get; set; } = string.Empty;
}