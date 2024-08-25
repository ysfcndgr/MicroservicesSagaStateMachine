using System;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;

namespace Order.API.Contexts
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}

