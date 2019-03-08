using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UnitOfWork
    {
        public LearnContext db;

        public UnitOfWork()
        {
            object oylesine = "";
            if (db == null)
            {
                lock (oylesine)
                {
                    if (db == null)
                    {
                        db = new LearnContext();
                    }
                }
            }

            Categories = new BaseRepository<Category>(db);
            Questions = new BaseRepository<Question>(db);
            Answers = new BaseRepository<Answer>(db);
        }

        public static LearnContext Create()
        {
            return new LearnContext();
        }

        public bool Complete()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BaseRepository<Category> Categories;
        public BaseRepository<Question> Questions;
        public BaseRepository<Answer> Answers;
    }
}
