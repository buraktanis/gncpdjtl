using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Configuration = Entities.Concrete.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.2.10;User ID=sa;Password=Deneme99??;Database=u9315160_crm");
            //Server=192.168.2.10;User ID=sa;Password=Deneme99??;
        }

        //public DbSet<Article> Articles { get; set; }
        public DbSet<Not> Nots { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<BigData> BigDatas { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CustomerConsignment> CustomerConsignments { get; set; }


    }
}
