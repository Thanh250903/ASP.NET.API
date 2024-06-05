using System.Collections.Generic;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data

{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
