using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Answer : IEntity
    {
        public int Id { get; set; }
        public string AnswerContent { get; set; }
        public string PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }        
    }
}
