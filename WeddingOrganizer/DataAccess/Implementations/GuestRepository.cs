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

        public IQueryable<Guest> GetAll()
        {
            return _context.Guests.AsQueryable();
        }

        public async Task<Guest> GetById(long id)
        {
            return await _context.Guests.SingleOrDefaultAsync(guest => guest.Id == id);
        }

        public async void Add(Guest entity)
        {
            await _context.Guests.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async void Update(Guest entity)
        {
            _context.Guests.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async void Delete(Guest entity)
        {
            _context.Guests.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
