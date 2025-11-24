using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Property(shoppingCart => shoppingCart.UserId)
                .HasMaxLength(450);
            builder.Property(shoppingCart => shoppingCart.TotalAmount)
                .HasPrecision(18, 2);
        }
    }
}
