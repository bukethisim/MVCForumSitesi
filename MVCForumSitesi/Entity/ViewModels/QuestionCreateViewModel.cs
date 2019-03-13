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
        [Display(Name = "Soru İçeriği")]
        public string QuestionContent { get; set; }
        [Display(Name = "Kategoriler")]
        public int CategoryId { get; set; }
    }
}
