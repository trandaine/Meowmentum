using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class ExcerciseConfiguration : IEntityTypeConfiguration<Excercise>
    {
        public void Configure(EntityTypeBuilder<Excercise> builder)
        {
            builder.Property(excercise => excercise.Explaination)
                .HasMaxLength(500);

        }
    }
}
