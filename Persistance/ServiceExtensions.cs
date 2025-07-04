﻿using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Context;
using Persistance.IdentityModels;
using Persistance.Seeds;
using Persistance.SharedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class ServiceExtensions
    {
        public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
                ));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


            services.AddTransient<IAccountService, AccountService>();

            DefaultRoles.SeedRolesAsync(services.BuildServiceProvider()).Wait();
            DefaultUsers.SeedUsersAsync(services.BuildServiceProvider()).Wait();

        }
    }
}
