using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class CustomerProgressConfiguration : IEntityTypeConfiguration<CustomerProgress>
    {
        public void Configure(EntityTypeBuilder<CustomerProgress> builder)
        {

        }
    }
}
