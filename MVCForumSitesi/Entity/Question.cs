using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class Question
    {
        public int Id { get; set; }
        public string QuestionUrl { get; set; }
        public string QuestionContent { get; set; }
        public virtual Person Person { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public virtual Category Category { get; set; }
    }
}
