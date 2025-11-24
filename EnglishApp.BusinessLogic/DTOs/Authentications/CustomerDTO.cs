using EnglishApp.ApplicationCore.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using EnglishApp.ApplicationCore.Constants;
using Microsoft.AspNetCore.Http;

namespace EnglishApp.BusinessLogic.DTOs.Authentications
{
    [Bind("Id,Name,Email,DateOfBirth,DateCreated,LastLogin,UserId,Avartar,Gender, Level,FilePath,FileSubmit, Description")]
    public class CustomerDTO
    {
        public int Id { get; set; }

        [MaxLength(FluentApiConstants.ML_Name)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(FluentApiConstants.ML_Email)]
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastLogin { get; set; }

        [MaxLength(FluentApiConstants.ML_UserId)]
        public string? UserId { get; set; }

        [MaxLength(FluentApiConstants.ML_FileNameLength)]
        public string? Avartar { get; set; }
        public string FilePath =>
                !string.IsNullOrEmpty(Avartar)
                    ? "/media/customer_profile_avatar/" + Avartar
                    : "/media/default/male_avatar.png";
        public IFormFile? FileSubmit { get; set; }
        public GenderEnum Gender { get; set; }
        public LevelEnum Level { get; set; }

        [MaxLength(FluentApiConstants.ML_Description)]
        public string? Description { get; set; }
    }
}
