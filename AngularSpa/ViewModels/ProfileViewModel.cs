using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSpa.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Description = "Id")]
        public long Id { set; get; }
        [Display(Description = "Статус пользователя")]
        public string Status { set; get; }
        [Display(Description = "Логин")]
        public string Login { set; get; }
        [Display(Description = "Полное имя")]
        public string FullName { set; get; }
        [Display(Description = "Электронная почта")]
        public string EmailAddress { set; get; }
        [Display(Description = "Страна")]
        public string Country { set; get; }
        [Display(Description = "Город")]
        public string City { set; get; }
        [Display(Description = "Почтовый индекс")]
        public string Zip { set; get; }
        [Display(Description = "Адрес")]
        public string Address { set; get; }
    }
}
