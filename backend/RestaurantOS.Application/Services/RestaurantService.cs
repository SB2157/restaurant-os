using RestaurantOS.Application.DTOs.Restaurants;
using RestaurantOS.Application.Interfaces;
using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Services;

public sealed class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<RestaurantResponse?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(
            id,
            cancellationToken);

        return restaurant is null
            ? null
            : MapToResponse(restaurant);
    }

    public async Task<RestaurantResponse?> GetActiveAsync(
        CancellationToken cancellationToken = default)
    {
        var restaurant = await _restaurantRepository.GetActiveAsync(
            cancellationToken);

        return restaurant is null
            ? null
            : MapToResponse(restaurant);
    }

    public async Task<RestaurantResponse> CreateAsync(
        CreateRestaurantRequest request,
        CancellationToken cancellationToken = default)
    {
        Validate(request);

        var existingRestaurant =
            await _restaurantRepository.GetActiveAsync(
                cancellationToken);

        if (existingRestaurant is not null)
        {
            throw new InvalidOperationException(
                "An active restaurant already exists.");
        }

        var restaurant = new Restaurant
        {
            Name = request.Name.Trim(),
            LegalName = request.LegalName?.Trim(),
            Phone = request.Phone?.Trim(),
            Email = request.Email?.Trim(),
            Address = request.Address?.Trim(),
            City = request.City?.Trim(),
            State = request.State?.Trim(),
            PostalCode = request.PostalCode?.Trim(),
            CurrencyCode = request.CurrencyCode.Trim().ToUpperInvariant(),
            TimeZoneId = request.TimeZoneId.Trim(),
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };

        restaurant.Id = await _restaurantRepository.CreateAsync(
            restaurant,
            cancellationToken);

        return MapToResponse(restaurant);
    }

    public async Task<RestaurantResponse?> UpdateAsync(
        long id,
        UpdateRestaurantRequest request,
        CancellationToken cancellationToken = default)
    {
        Validate(request);

        var restaurant = await _restaurantRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (restaurant is null)
        {
            return null;
        }

        restaurant.Name = request.Name.Trim();
        restaurant.LegalName = request.LegalName?.Trim();
        restaurant.Phone = request.Phone?.Trim();
        restaurant.Email = request.Email?.Trim();
        restaurant.Address = request.Address?.Trim();
        restaurant.City = request.City?.Trim();
        restaurant.State = request.State?.Trim();
        restaurant.PostalCode = request.PostalCode?.Trim();
        restaurant.CurrencyCode =
            request.CurrencyCode.Trim().ToUpperInvariant();
        restaurant.TimeZoneId = request.TimeZoneId.Trim();
        restaurant.IsActive = request.IsActive;
        restaurant.UpdatedAtUtc = DateTime.UtcNow;

        await _restaurantRepository.UpdateAsync(
            restaurant,
            cancellationToken);

        return MapToResponse(restaurant);
    }

    private static void Validate(CreateRestaurantRequest request)
    {
        ValidateCommon(
            request.Name,
            request.CurrencyCode,
            request.TimeZoneId);
    }

    private static void Validate(UpdateRestaurantRequest request)
    {
        ValidateCommon(
            request.Name,
            request.CurrencyCode,
            request.TimeZoneId);
    }

    private static void ValidateCommon(
        string name,
        string currencyCode,
        string timeZoneId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Restaurant name is required.");
        }

        if (name.Trim().Length > 200)
        {
            throw new ArgumentException(
                "Restaurant name cannot exceed 200 characters.");
        }

        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            throw new ArgumentException(
                "Currency code is required.");
        }

        if (currencyCode.Trim().Length != 3)
        {
            throw new ArgumentException(
                "Currency code must contain exactly 3 characters.");
        }

        if (string.IsNullOrWhiteSpace(timeZoneId))
        {
            throw new ArgumentException(
                "Time zone is required.");
        }
    }

    private static RestaurantResponse MapToResponse(
        Restaurant restaurant)
    {
        return new RestaurantResponse
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            LegalName = restaurant.LegalName,
            Phone = restaurant.Phone,
            Email = restaurant.Email,
            Address = restaurant.Address,
            City = restaurant.City,
            State = restaurant.State,
            PostalCode = restaurant.PostalCode,
            CurrencyCode = restaurant.CurrencyCode,
            TimeZoneId = restaurant.TimeZoneId,
            IsActive = restaurant.IsActive,
            CreatedAtUtc = restaurant.CreatedAtUtc,
            UpdatedAtUtc = restaurant.UpdatedAtUtc
        };
    }
}