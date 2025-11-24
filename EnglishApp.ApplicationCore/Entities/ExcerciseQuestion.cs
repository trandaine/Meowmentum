using System.Collections.ObjectModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class ExcerciseQuestion
    {
        public ExcerciseQuestion()
        {
            ExerciseQuestionOptions = new Collection<ExerciseQuestionOption>();
        }
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public int Postion { get; set; }


        #region Navigational Property
        public int ExcerciseId { get; set; }
        public virtual Excercise Excercise { get; set; }

        public virtual ICollection<ExerciseQuestionOption> ExerciseQuestionOptions { get; set; }
        //public int ExerciseQuestionOptionId { get; set; }
        //public virtual ExerciseQuestionOption ExerciseQuestionOption { get; set; }
        #endregion

    }
}
