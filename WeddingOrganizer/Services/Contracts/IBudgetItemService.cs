using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Services.Contracts
{
    public interface IBudgetItemService
    {
        IEnumerable<BudgetItemModel> GetAllUserItems(long userId);
        BudgetItemModel GetBudgetItem(long id, long userId);
        void AddBudgetItem(BudgetItemModel budgetItem);
        void UpdateBudgetItem(BudgetItemModel budgetItem);
        void DeleteBudgetItem(long id, long userId);
    }
}
