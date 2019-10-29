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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var userId = GetAuthorizedUserId();

            if (id != userId)
            {
                throw  new UnauthorizedAccessException("You are not authorized");
            }
            var guest = await _userService.GetUser(id);

            return Ok(guest);
        }

        // POST: api/Users/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            await _userService.Register(model);
            return Ok("Successfully registered");
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public IActionResult Authenticate([FromBody] SignInModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);
            if (user == null)
            {
                return NotFound("Incorrect email or password");
            }
            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel model)
        {
            model.Id = id;
            await _userService.UpdateUser(model);
            return NoContent();
        }

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

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
