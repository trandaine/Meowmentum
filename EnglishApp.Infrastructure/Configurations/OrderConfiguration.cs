using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(order => order.Phone)
                .HasMaxLength(15);
            builder.Property(order => order.Address)
                .HasMaxLength(200);
            builder.Property(order => order.TransactionId)
                .HasMaxLength(450);
            builder.Property(order => order.Note)
                .HasMaxLength(200);
            builder.Property(order => order.Amount)
                .HasPrecision(18, 2);


        }
    }
}
