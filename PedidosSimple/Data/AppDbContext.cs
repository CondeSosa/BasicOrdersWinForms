using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidosSimple.Data
{
   public class AppDbContext : DbContext
    {
        public AppDbContext()
        { }
        public DbSet<Client> Client { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlServer($"Server=DESKTOP-R542BTM\\LOCALDB;Database=PedidoSDb;Trusted_Connection=True;MultipleActiveResultSets=true;");

        }
    }
}
