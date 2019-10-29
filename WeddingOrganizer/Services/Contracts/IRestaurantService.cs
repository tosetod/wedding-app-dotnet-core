using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Contracts
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantModel> GetAllRestaurants();
        Task<RestaurantModel> GetRestaurant(long id);
        Task<RestaurantModel> GetUserRestaurant(long id, long userId);
        Task AddRestaurant(RestaurantModel restaurant);
        Task UpdateRestaurant(RestaurantModel restaurant);
        Task DeleteRestaurant(long id);
    }
}
