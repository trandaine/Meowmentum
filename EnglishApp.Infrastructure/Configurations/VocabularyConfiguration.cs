using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class VocabularyConfiguration : IEntityTypeConfiguration<Vocabulary>
    {
        public void Configure(EntityTypeBuilder<Vocabulary> builder)
        {
            builder.Property(vocabulary => vocabulary.Word)
                .HasMaxLength(100);
            builder.Property(vocabulary => vocabulary.Meaning)
                .HasMaxLength(255);
            builder.Property(vocabulary => vocabulary.ExampleSentence)
                .HasMaxLength(500);
            builder.Property(vocabulary => vocabulary.Transcription)
                .HasMaxLength(100);
            builder.Property(vocabulary => vocabulary.Audio)
                .HasMaxLength(100);
        }
    }
}
