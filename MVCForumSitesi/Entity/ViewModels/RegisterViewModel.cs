using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "Kullanıcı Türü")]
        public bool hasLesson { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
