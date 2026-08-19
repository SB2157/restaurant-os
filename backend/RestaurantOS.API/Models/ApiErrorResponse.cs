namespace RestaurantOS.API.Models;

public sealed class ApiErrorResponse
{
    public bool Success { get; init; } = false;

    public string Message { get; init; } = string.Empty;

    public object? Errors { get; init; }
}