namespace EnglishApp.ApplicationCore.Entities
{
    public class CourseComment
    {
        public CourseComment()
        {

        }
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? CommentText { get; set; } = string.Empty;
        public bool IsReported { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        #region Navigational Property
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        #endregion
    }
}
