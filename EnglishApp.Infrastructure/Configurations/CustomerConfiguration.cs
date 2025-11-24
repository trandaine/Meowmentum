using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(customer => customer.Name)
                .HasMaxLength(200);
            builder.Property(customer => customer.Email)
                .HasMaxLength(100);
            builder
                .HasIndex(customer => customer.UserId);
            builder.Property(customer => customer.UserId)
                .HasMaxLength(450);
            builder.Property(customer => customer.Avartar)
                .HasMaxLength(255);
        }
    }
}
