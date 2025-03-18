using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ThreaderApp.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string AboutMe { get; set; }
    }
}
