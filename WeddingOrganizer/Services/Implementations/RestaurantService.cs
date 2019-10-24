using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DataAccess.Contracts;
using DataModels.Entities;
using Models;
using Services.Contracts;
using Services.Exceptions;

namespace Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Restaurant> _restaurantRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public RestaurantService(
            IRepository<Restaurant> restaurantRepository,
            IRepository<User> userRepository, 
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<RestaurantModel> GetAllRestaurants(long userId)
        {
            return _mapper.Map<IEnumerable<RestaurantModel>>(_restaurantRepository.GetAll());
        }

        public RestaurantModel GetRestaurant(long id)
        {
            var restaurant = _restaurantRepository.GetById(id);
            return _mapper.Map<RestaurantModel>(restaurant ?? throw new ResourceNotFoundException<Restaurant>(id));
        }

        public RestaurantModel GetUserRestaurant(long id, long userId)
        {
            var user = _userRepository.GetById(userId).Result;
            var restaurant = _restaurantRepository
                .GetAll()
                .SingleOrDefault(r => r.Id == id && user.RestaurantId == id);

            return _mapper.Map<RestaurantModel>(restaurant ?? throw new ResourceNotFoundException<Restaurant>(id));
        }

        public void AddRestaurant(RestaurantModel restaurant)
        {
            var newRestaurant = _mapper.Map<Restaurant>(restaurant);
            _restaurantRepository.Add(newRestaurant);
        }

        public void UpdateRestaurant(RestaurantModel restaurant)
        {
            _restaurantRepository.Update(
                _mapper.Map<Restaurant>(restaurant));
        }

        public void DeleteRestaurant(long id)
        {
            var restaurant = GetRestaurant(id);
            _restaurantRepository.Delete(
                _mapper.Map<Restaurant>(restaurant?? throw new ResourceNotFoundException<Restaurant>(id)));
        }
    }
}
