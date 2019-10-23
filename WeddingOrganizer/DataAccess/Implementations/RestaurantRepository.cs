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
    public class RestaurantRepository : IRepository<Restaurant>
    {
        private readonly ApplicationDbContext _context;

        public RestaurantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Restaurant> GetAll()
        {
            return _context.Restaurants.AsQueryable();
        }

        public async Task<Restaurant> GetById(long id)
        {
            return await _context.Restaurants.SingleOrDefaultAsync(restaurant => restaurant.Id == id);
        }

        public async void Add(Restaurant entity)
        {
            await _context.Restaurants.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async void Update(Restaurant entity)
        {
            _context.Restaurants.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async void Delete(Restaurant entity)
        {
            _context.Restaurants.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
