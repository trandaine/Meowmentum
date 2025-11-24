using EnglishApp.ApplicationCore.Constants;
using EnglishApp.ApplicationCore.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EnglishApp.MVC.Models
{
    [Bind("Id,Name,Description,Level,Thumbnail,Price")]
    public class CourseViewModel
    {
        public int Id { get; set; }
        [MaxLength(FluentApiConstants.ML_Name)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(FluentApiConstants.ML_Description)]
        public string? Description { get; set; }
        public LevelEnum Level { get; set; }
        [MaxLength(FluentApiConstants.ML_FileNameLength)]
        public string? Thumbnail { get; set; }
        [Precision(18, 2)]
        public double Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int Position { get; set; }
    }
}
