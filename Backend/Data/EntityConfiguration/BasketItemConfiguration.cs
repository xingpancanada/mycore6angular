using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.EntityConfiguration
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            
            builder.HasOne(b=>b.Product).WithMany().HasForeignKey(p=>p.ProductId);
        }
    }
}