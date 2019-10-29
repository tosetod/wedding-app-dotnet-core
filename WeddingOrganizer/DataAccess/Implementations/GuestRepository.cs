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
    public class GuestRepository : IRepository<Guest>
    {
        private readonly ApplicationDbContext _context;

        public GuestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Guest> GetAll()
        {
            return _context.Guests.ToList();
        }

        public async Task<Guest> GetById(long id)
        {
            return await _context.Guests.FindAsync(id);
        }

        public async Task Add(Guest entity)
        {
            await _context.Guests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guest entity)
        {
            _context.Guests.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guest entity)
        {
            _context.Guests.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
