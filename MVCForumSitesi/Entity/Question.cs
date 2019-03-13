using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public string QuestionUrl { get; set; }
        public string ThumbnailURL { get; set; }
        public string QuestionContent { get; set; }
        public string PersonId { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
