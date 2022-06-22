using System.Reflection;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Data
{
    public class StoreDBContext : DbContext
    {
        ////add default constructor with options for add default connection strings
        public StoreDBContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Product> Products { get; set; }

        ////25. Extending the products entity and creating related entities
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        

        ////27. Configuring the migrations  ////EntityConfiguration is used here when build model
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            ////27. Configuring the migrations
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            //////62. Working around the decimal problem in Sqlite
            if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach(var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    
                    var dateTimeProperties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            };
        }
    }

}

