using System.Collections.ObjectModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class QuestionType
    {
        public QuestionType()
        {
            Excercises = new Collection<Excercise>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int Position { get; set; }

        #region Navigational Property
        public virtual ICollection<Excercise> Excercises { get; set; }
        #endregion

    }
}
