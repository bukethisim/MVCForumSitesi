using Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LearnContext : IdentityDbContext<Person>
    {
        public LearnContext() : base("LearnContext")
        {

        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Table Configuration

            modelBuilder.Entity<Question>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Answer>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<Category>()
               .HasKey(x => x.Id);

            #endregion


            #region Relations

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Questions)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            //modelBuilder.Entity<Category>()
            //   .HasMany(x => x.Teachers)
            //   .WithRequired(x => x.Category)
            //   .HasForeignKey(x => x.CategoryId);

            //modelBuilder.Entity<Student>()
            //  .HasMany(x => x.Questions)
            //  .WithRequired(x => x.Student)
            //  .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<Question>()
             .HasMany(x => x.Answers)
             .WithRequired(x => x.Question)
             .HasForeignKey(x => x.QuestionId);

            modelBuilder.Entity<Person>()
                .HasMany(x => x.Answers)
                .WithRequired(x => x.Person)
                .HasForeignKey(x => x.PersonId);

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
