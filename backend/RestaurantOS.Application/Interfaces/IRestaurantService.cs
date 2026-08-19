using RestaurantOS.Application.DTOs.Restaurants;

namespace RestaurantOS.Application.Interfaces;

public interface IRestaurantService
{
    Task<RestaurantResponse?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<RestaurantResponse?> GetActiveAsync(
        CancellationToken cancellationToken = default);

    Task<RestaurantResponse> CreateAsync(
        CreateRestaurantRequest request,
        CancellationToken cancellationToken = default);

    Task<RestaurantResponse?> UpdateAsync(
        long id,
        UpdateRestaurantRequest request,
        CancellationToken cancellationToken = default);
}