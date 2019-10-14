using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Entities
{
    public class BudgetItem : BaseEntity
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public int Budget { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
