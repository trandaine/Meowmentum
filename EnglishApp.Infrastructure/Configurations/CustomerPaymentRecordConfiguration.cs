using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class CustomerPaymentRecordConfiguration : IEntityTypeConfiguration<CustomerPaymentRecord>
    {
        public void Configure(EntityTypeBuilder<CustomerPaymentRecord> builder)
        {
            builder.Property(x => x.Amount)
               .HasColumnType("decimal(10,2)");
            builder.Property(x => x.TransactionId)
               .HasMaxLength(450);
            builder.Property(x => x.UserId)
               .HasMaxLength(450);
            builder
                .HasIndex(x => x.UserId);
        }
    }
}
