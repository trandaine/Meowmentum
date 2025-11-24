using System.ComponentModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class Vocabulary
    {
        public Vocabulary()
        {

        }
        public int Id { get; set; }
        public string Word { get; set; } = string.Empty;
        public string Meaning { get; set; } = string.Empty;
        public string? ExampleSentence { get; set; }
        [Description("Phiên âm của từ")]
        public string? Transcription { get; set; }
        public string? Audio { get; set; }

        #region Navigational Property
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        #endregion

    }
}
