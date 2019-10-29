using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contracts;

namespace WeddingOrganizerApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantModel>> GetAllRestaurants()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantModel>> GetUserRestaurant(int id)
        {
            var userId = GetAuthorizedUserId();

            var restaurant = await _restaurantService.GetUserRestaurant(id, userId);

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RestaurantModel model)
        {
            await _restaurantService.AddRestaurant(model);
            return Created($"http://localhost:44361/Restaurants", model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RestaurantModel model)
        {
            model.Id = id;
            await _restaurantService.UpdateRestaurant(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _restaurantService.DeleteRestaurant(id);
            return NoContent();
        }

        private long GetAuthorizedUserId()
        {
            if (!int.TryParse(User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userId))
            {
                throw new Exception("Name identifier claim does not exist.");
            }
            return userId;
        }
    }
}
