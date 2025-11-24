using System.ComponentModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class ExerciseQuestionOption
    {
        public ExerciseQuestionOption()
        {

        }
        public int Id { get; set; }
        [Description("Nội dung câu trả lời cho câu hỏi")]
        public string Name { get; set; } = string.Empty;
        [Description("Câu hỏi đó có phải là đúng hay sai")]
        public bool IsCorrect { get; set; }
        [Description("Có thể chứa tên file nghe trong dạng này")]
        public string? Audio { get; set; }
        public int Position { get; set; }

        #region Navigational Property
        public int ExcerciseQuestionId { get; set; }
        public virtual ExcerciseQuestion ExcerciseQuestion { get; set; }
        #endregion
    }
}
