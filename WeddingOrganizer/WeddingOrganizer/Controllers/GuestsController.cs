using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Contracts;

namespace WeddingOrganizerApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestsController(IGuestService guestService)
        {
            _guestService = guestService;
        }
        // GET: api/Guests
        [HttpGet]
        public ActionResult<IEnumerable<GuestModel>> GetAllGuests()
        {
            var userId = GetAuthorizedUserId();
            var guests = _guestService.GetAllUserGuests(userId);
            return Ok(guests);
        }

        // GET: api/Guests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GuestModel>> GetGuest(int id)
        {
            var userId = GetAuthorizedUserId();

            var guest = await _guestService.GetGuest(id, userId);

            return Ok(guest);
        }

        // POST: api/Guests
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GuestModel model)
        {
            model.UserId = GetAuthorizedUserId();
            await _guestService.AddGuest(model);
            return Created($"http://localhost:44361/Guests/{model.UserId}", model);
        }

        // PUT: api/Guests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GuestModel model)
        {
            model.Id = id;
            await _guestService.UpdateGuest(model);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetAuthorizedUserId();
            await _guestService.DeleteGuest(id, userId);
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
