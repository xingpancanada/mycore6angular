using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.EntityConfiguration
{
    public class CustomerBasketConfiguration : IEntityTypeConfiguration<CustomerBasket>
    {
        public void Configure(EntityTypeBuilder<CustomerBasket> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.Property(b  => b.ShippingPrice).HasColumnType("decimal(18, 2)");

            builder.HasMany(b => b.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}