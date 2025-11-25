using System.ComponentModel.DataAnnotations;

namespace EnglishApp.BusinessLogic.DTOs
{
    public class CourseCommentDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter a comment.")]
        public string CommentText { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; } = string.Empty;
        public bool IsReported { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        #region Navigational Property
        public int CourseId { get; set; }
        public int CustomerId { get; set; }
        #endregion
    }
}
