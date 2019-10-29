using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DataAccess.Contracts;
using DataAccess.Implementations;
using DataModels;
using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Helpers
{
    public class DiModule
    {
        /// <summary>
        /// Register repositories to services with dependency injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterModule(
            IServiceCollection services, string connectionString)
        {
            
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<BudgetItem>, BudgetItemRepository>();
            services.AddTransient<IRepository<Guest>, GuestRepository>();
            services.AddTransient<IRepository<Restaurant>, RestaurantRepository>();
            services.AddDbContext<ApplicationDbContext>(x =>
                x.UseSqlServer(connectionString));

            return services;
        }
    }
}
