using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataModels;
using DataModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class BudgetItemRepository : IRepository<BudgetItem>
    {
        private readonly ApplicationDbContext _context;

        public BudgetItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BudgetItem> GetAll()
        {
            return _context.BudgetItems.ToList();
        }

        public async Task<BudgetItem> GetById(long id)
        {
            return await _context.BudgetItems.FindAsync(id);
        }

        public async Task Add(BudgetItem entity)
        {
            await _context.BudgetItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(BudgetItem entity)
        {
            _context.BudgetItems.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(BudgetItem entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
