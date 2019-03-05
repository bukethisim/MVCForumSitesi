using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Person
    {
        public int Like { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }

    public class Teacher : Person
    {
        public bool hasLesson { get; set; }
    }

    public class Student : Person
    {
        public virtual List<Question> Questions { get; set; }
    }
}
