using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Contracts
{
    public interface IBudgetItemService
    {
        IEnumerable<BudgetItemModel> GetAllUserItems(long userId);
        Task<BudgetItemModel> GetBudgetItem(long id, long userId);
        Task AddBudgetItem(BudgetItemModel budgetItem);
        Task UpdateBudgetItem(BudgetItemModel budgetItem);
        Task DeleteBudgetItem(long id, long userId);
    }
}
