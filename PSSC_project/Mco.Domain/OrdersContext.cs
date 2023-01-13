using Mco.Domain.Dbo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mco.Domain
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {

        }
        
        public DbSet<ItemDbo> Items { get; set; }
        public DbSet<OrderDbo> Orders { get; set; }
        public DbSet<ItemsInOrderDbo> ItemsInOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("1");
            modelBuilder.Entity<ItemDbo>().ToTable("Items").HasKey(s => s.ItemId);
            modelBuilder.Entity<OrderDbo>().ToTable("Orders").HasKey(s => s.CartId);
            modelBuilder.Entity<ItemsInOrderDbo>().ToTable("ItemsInCart").HasKey(s => s.CartId);
            Console.WriteLine("2");
        }
    }
}
