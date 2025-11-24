using EnglishApp.ApplicationCore.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EnglishApp.BusinessLogic.DTOs
{
    [Bind("Id,Name,Content,ContentType,Video,Audio, CourseId")]
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Content { get; set; }
        public ContentTypeEnum ContentType { get; set; }
        [Description("Store file name of video or audio")]
        public string? Video { get; set; }
        [Description("Store file name of video or audio")]
        public string? Audio { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int Position { get; set; }
        public int CourseId { get; set; }
    }
}
