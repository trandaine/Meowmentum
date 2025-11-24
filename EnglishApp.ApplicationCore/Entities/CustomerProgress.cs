using EnglishApp.ApplicationCore.Enums;

namespace EnglishApp.ApplicationCore.Entities
{
    public class CustomerProgress
    {
        public CustomerProgress()
        {

        }
        public int Id { get; set; }
        public int TotalPoint { get; set; }
        public LevelEnum CurrentLevel { get; set; }
        public int? StreakDays { get; set; }

        #region Navigational Property
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        #endregion

    }
}
