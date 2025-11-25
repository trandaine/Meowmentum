using EnglishApp.ApplicationCore.Constants;
using EnglishApp.ApplicationCore.Enums;
using MathNet.Numerics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace EnglishApp.BusinessLogic.DTOs
{
    [Bind("Id,Name,Description,Level,ImageFile,Thumbnail,Price")]
    public class CourseDTO
    {
        public int Id { get; set; }
        [MaxLength(FluentApiConstants.ML_Name)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(FluentApiConstants.ML_Description)]
        public string? Description { get; set; }
        public LevelEnum Level { get; set; }
        [MaxLength(FluentApiConstants.ML_FileNameLength)]
        public string? Thumbnail { get; set; }
        public string FilePath =>
                !string.IsNullOrEmpty(Thumbnail)
                    ? "/media/course_images/" + Thumbnail
                    : "/media/default/image.png";
        public IFormFile? ImageFile { get; set; }
        [Precision(18, 9)]
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int Position { get; set; }

        //public Course NewCourse { get; set; }
        //public List<Course> ExistingCourses { get; set; }
    }
}


