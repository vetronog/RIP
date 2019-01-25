using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Имя*")]
        public string Name { get; set; }

        [Display(Name = "Фамилия*")]
        public string Surname { get; set; }

        [Display(Name = "Email*")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Некорректный формат Email")]
        public string Email { get; set; }

        [Display(Name = "Пароль*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля*")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}