using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerContent { get; set; }
        public virtual Person Person { get; set; }
        public virtual Question Question { get; set; }        
    }
}
