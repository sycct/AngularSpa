using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSpa.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Description = "Имя пользователя", Name = "Username", Prompt = "Введите имя пользователя")]
        public string Login { set; get; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Description = "Пароль", Name = "Password", Prompt = "Введите пароль для пользователя")]
        public string Password { set; get; }
    }
}
