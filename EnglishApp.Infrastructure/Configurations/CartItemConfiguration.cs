using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(cartItem => cartItem.Name)
                .HasMaxLength(200);
            builder.Property(cartItem => cartItem.Image)
                .HasMaxLength(100);
            builder.Property(cartItem => cartItem.Price)
                .HasPrecision(18, 2);
        }
    }
}
