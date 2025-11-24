using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EnglishApp.ApplicationCore.IdentityEntities
{
    public class EnglishAppIdentityRole : IdentityRole
    {
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
