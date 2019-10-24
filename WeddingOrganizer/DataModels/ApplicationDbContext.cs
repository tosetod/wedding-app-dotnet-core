using System;
using System.Linq;
using System.Threading;
using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DataModels.Mapping;

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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var addedEntries = ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Added).AsQueryable();

            foreach (var entry in addedEntries)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }

            var modifiedEntries= ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Modified).AsQueryable();

            foreach (var entry in modifiedEntries)
            {
                entry.Entity.DateModified = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new GuestMap());

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
