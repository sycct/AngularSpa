using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSpa.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Description = "Имя пользователя", Name = "Username", Prompt = "Введите имя пользователя")]
        public string Login { set; get; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Description = "Пароль", Name = "Password", Prompt = "Введите пароль для пользователя")]
        public string Password { set; get; }

        [Required, RegularExpression(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})", ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress]
        [Display(Description = "Email Адресс", Name = "EmailAddress", Prompt = "Email адресс")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 6)]
        [Display(Description = "Полное имя", Name = "FullName", Prompt = "Введите полное имя")]
        public string FullName { get; set; }
    }
}
