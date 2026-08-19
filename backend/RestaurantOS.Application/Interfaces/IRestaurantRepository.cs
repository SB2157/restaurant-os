using RestaurantOS.Domain.Entities;

namespace RestaurantOS.Application.Interfaces;

public interface IRestaurantRepository
{
    Task<Restaurant?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<Restaurant?> GetActiveAsync(
        CancellationToken cancellationToken = default);

    Task<long> CreateAsync(
        Restaurant restaurant,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Restaurant restaurant,
        CancellationToken cancellationToken = default);
}