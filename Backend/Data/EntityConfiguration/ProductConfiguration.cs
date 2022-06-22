using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.EntityConfiguration
{
    ////27. Configuring the migrations  ////used in StoreDBContext for model build
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Id).IsRequired();
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(100);  //sqlite can use hasmaxlength
            builder.Property(p=>p.Description).IsRequired().HasMaxLength(200);
            // builder.Property(p=>p.Name).IsRequired();
            // builder.Property(p=>p.Description).IsRequired();
            builder.Property(p=>p.Price).HasColumnType("decimal(18,2)");  //sqlite uses NUMERIC???
            builder.Property(p=>p.PictureUrl).IsRequired();
            ////27. we don't need code productBrandId and ProductTypeId here, it will be auto migrate because of HasForeignKey 
            builder.HasOne(b=>b.ProductBrand).WithMany().HasForeignKey(p=>p.ProductBrandId);
            builder.HasOne(b=>b.ProductType).WithMany().HasForeignKey(p=>p.ProductTypeId);
        }
    }
}