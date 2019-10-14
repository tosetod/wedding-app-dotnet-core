using System;
using System.Linq;

namespace DataModels.Entities
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte Age { get; set; }
        public DateTime WeddingDate { get; set; }
        public string PartnerName { get; set; }
        public byte PartnerAge { get; set; }
        public virtual  IQueryable<BudgetItem> BudgetItems { get; set; }
        public virtual  IQueryable<Guest> Guests{ get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}