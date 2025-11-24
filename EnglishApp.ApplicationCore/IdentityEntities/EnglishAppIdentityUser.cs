using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EnglishApp.ApplicationCore.IdentityEntities
{
    public class EnglishAppIdentityUser : IdentityUser
    {
        [MaxLength(255)]
        public string? FullName { get; set; }
    }
}
