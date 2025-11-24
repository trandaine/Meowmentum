using EnglishApp.ApplicationCore.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class Lesson
    {
        public Lesson()
        {
            CustomerProgresses = new Collection<CustomerProgress>();
            Excercises = new Collection<Excercise>();
            Vocabularies = new Collection<Vocabulary>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public ContentTypeEnum ContentType { get; set; }
        [Description("Store file name of video or audio")]
        public string? Video { get; set; }
        [Description("Store file name of video or audio")]
        public string? Audio { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int Position { get; set; }

        #region Navigational Property
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<CustomerProgress> CustomerProgresses { get; set; }
        public virtual ICollection<Excercise> Excercises { get; set; }
        public virtual ICollection<Vocabulary> Vocabularies { get; set; }

        #endregion
    }
}
