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
    public class BudgetItemsController : ControllerBase
    {
        private readonly IBudgetItemService _budgetItemService;

        public BudgetItemsController(IBudgetItemService budgetItemService)
        {
            _budgetItemService = budgetItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BudgetItemModel>> GetAllBudgetItems()
        {
            var userId = GetAuthorizedUserId();
            var items = _budgetItemService.GetAllUserItems(userId);
            return Ok(items);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetItemModel>> GetBudgetItem(int id)
        {
            var userId = GetAuthorizedUserId();

            var item = await _budgetItemService.GetBudgetItem(id, userId);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BudgetItemModel model)
        {
            model.UserId = GetAuthorizedUserId();
            await _budgetItemService.AddBudgetItem(model);
            return Created($"http://localhost:44361/BudgetItems/{model.UserId}", model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BudgetItemModel model)
        {
            model.Id = id;
            await _budgetItemService.UpdateBudgetItem(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetAuthorizedUserId();
            await _budgetItemService.DeleteBudgetItem(id, userId);
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
