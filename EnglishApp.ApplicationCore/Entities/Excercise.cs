using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class Excercise
    {
        public Excercise()
        {
            ExcerciseQuestions = new Collection<ExcerciseQuestion>();
        }
        public int Id { get; set; }
        //public string? CorrectAnswer { get; set; } // Bài tập không thể nào có đáp án đúng sai, chuyển xuống bảng ExerciseQuestionOption
        [Description("Giải thích rằng bài tập này có gì, sau khi làm thì được gì")]
        public string? Explaination { get; set; }
        public int Position { get; set; }

        #region Navigational Property
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public int QuestionTypeId { get; set; }
        public virtual QuestionType QuestionType { get; set; }
        public virtual ICollection<ExcerciseQuestion> ExcerciseQuestions { get; set; }
        #endregion
    }
}
