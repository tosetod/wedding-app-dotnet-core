using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BudgetItemModel
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public int Budget { get; set; }
        public int OverUnder => Budget - Amount;
    }
}
