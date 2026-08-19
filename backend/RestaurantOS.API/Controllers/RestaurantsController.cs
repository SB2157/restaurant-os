using Microsoft.AspNetCore.Mvc;
using RestaurantOS.Application.DTOs.Restaurants;
using RestaurantOS.Application.Interfaces;

namespace RestaurantOS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet("active")]
    public async Task<ActionResult<RestaurantResponse>> GetActive(
        CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantService.GetActiveAsync(
            cancellationToken);

        if (restaurant is null)
        {
            return NotFound(new
            {
                success = false,
                message = "No active restaurant has been configured."
            });
        }

        return Ok(new
        {
            success = true,
            data = restaurant
        });
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<RestaurantResponse>> GetById(
        long id,
        CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantService.GetByIdAsync(
            id,
            cancellationToken);

        if (restaurant is null)
        {
            return NotFound(new
            {
                success = false,
                message = "Restaurant not found."
            });
        }

        return Ok(new
        {
            success = true,
            data = restaurant
        });
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantResponse>> Create(
        [FromBody] CreateRestaurantRequest request,
        CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantService.CreateAsync(
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = restaurant.Id },
            new
            {
                success = true,
                message = "Restaurant created successfully.",
                data = restaurant
            });
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<RestaurantResponse>> Update(
        long id,
        [FromBody] UpdateRestaurantRequest request,
        CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantService.UpdateAsync(
            id,
            request,
            cancellationToken);

        if (restaurant is null)
        {
            return NotFound(new
            {
                success = false,
                message = "Restaurant not found."
            });
        }

        return Ok(new
        {
            success = true,
            message = "Restaurant updated successfully.",
            data = restaurant
        });
    }
}