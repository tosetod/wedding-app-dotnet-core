using DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataModels
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}
