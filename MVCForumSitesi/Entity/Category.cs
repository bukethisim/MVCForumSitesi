using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }  
        public virtual List<Question> Questions { get; set; }
        //public virtual List<Teacher> Teachers { get; set; }
    }
}
