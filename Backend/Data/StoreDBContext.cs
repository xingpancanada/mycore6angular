using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class StoreDBContext : DbContext
    {
        ////add default constructor with options for add default connection strings
        public StoreDBContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Product> Products { get; set; }
        
        
    }

}

