using DAL;
using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public List<T> Search(Func<T, bool> query)
        {
            return _db.Set<T>().Where(query).ToList();
        }

        public bool Add(T record)
        {
            try
            {
                _db.Set<T>().Add(record);
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            try
            {
                T t = GetOne(id);
                _db.Set<T>().Remove(t);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(IEntity newRecord)
        {
            try
            {
                T old = GetOne(newRecord.Id);
                _db.Entry(old).CurrentValues.SetValues(newRecord);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
