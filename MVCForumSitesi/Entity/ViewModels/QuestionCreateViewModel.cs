using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class QuestionCreateViewModel
    {
        public string QuestionUrl { get; set; }
        [Display(Name = "Soru İçeriği")]
        public string QuestionContent { get; set; }
        [Display(Name = "Kategoriler")]
        public virtual Category Category { get; set; }
    }
}
