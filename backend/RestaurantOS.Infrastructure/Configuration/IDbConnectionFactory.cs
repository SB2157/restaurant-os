using System.Data;

namespace RestaurantOS.Application.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}