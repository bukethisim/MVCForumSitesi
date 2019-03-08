using DAL;
using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BaseRepository<T> where T : class, IEntity
    {
        private LearnContext _db;

        public BaseRepository(LearnContext db)
        {
            _db = db;
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetOne(int id)
        {
            return _db.Set<T>().Find(id);
        }


    }
}
