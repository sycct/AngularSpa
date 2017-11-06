using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSpa.ViewModels
{
    public class UserInfo
    {
        [Display(Description = "Record #")]
        public long Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        [Display(Description = "Username", Name ="Username", Prompt="Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Description = "Password", Name = "Password")]
        public string Password { get; set; }

        [StringLength(30, MinimumLength = 6)]
        [Display(Description = "Full Name", Name ="FullName", Prompt="Full Name")]
        public string FullName { get; set; }

    }
}
