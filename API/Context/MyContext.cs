using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(); 
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
       

    }
}
