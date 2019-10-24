using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Services.Contracts
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantModel> GetAllRestaurants(long userId);
        RestaurantModel GetRestaurant(long id);
        RestaurantModel GetUserRestaurant(long id, long userId);
        void AddRestaurant(RestaurantModel restaurant);
        void UpdateRestaurant(RestaurantModel restaurant);
        void DeleteRestaurant(long id);
    }
}
