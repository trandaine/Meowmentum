using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.Property(currency => currency.Name)
                .HasMaxLength(200);
            builder.Property(currency => currency.Code)
                .HasMaxLength(3);
            builder.Property(currency => currency.Symbol)
                .HasMaxLength(10);
        }
    }
}
