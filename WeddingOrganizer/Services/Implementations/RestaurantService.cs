using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<RestaurantModel> GetAllRestaurants()
        {
            return _mapper.Map<IEnumerable<RestaurantModel>>(_restaurantRepository.GetAll());
        }

        public async Task<RestaurantModel> GetRestaurant(long id)
        {
            var restaurant = await _restaurantRepository.GetById(id);
            return _mapper.Map<RestaurantModel>(restaurant ?? throw new ResourceNotFoundException<Restaurant>(id));
        }

        public async Task<RestaurantModel> GetUserRestaurant(long id, long userId)
        {
            var user = await _userRepository.GetById(userId);
            var restaurant = _restaurantRepository
                .GetAll()
                .SingleOrDefault(r => r.Id == id && user.RestaurantId == id);

            return _mapper.Map<RestaurantModel>(restaurant ?? throw new ResourceNotFoundException<Restaurant>(id));
        }

        public async Task AddRestaurant(RestaurantModel restaurant)
        {
            var newRestaurant = _mapper.Map<Restaurant>(restaurant);
            await _restaurantRepository.Add(newRestaurant);
        }

        public async Task UpdateRestaurant(RestaurantModel restaurant)
        {
            await _restaurantRepository.Update(
                _mapper.Map<Restaurant>(restaurant));
        }

        public async Task DeleteRestaurant(long id)
        {
            var restaurant = await _restaurantRepository.GetById(id);
            await _restaurantRepository.Delete(
                _mapper.Map<Restaurant>(restaurant));
        }
    }
}
