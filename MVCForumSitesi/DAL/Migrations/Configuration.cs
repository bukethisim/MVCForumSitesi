namespace DAL.Migrations
{
    using Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.LearnContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DAL.LearnContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if(!context.Users.Any(x=> x.Email == "admin@a.com"))
            {
                Person p = new Person();
                p.Email = "admin@a.com";
                p.UserName = "Admin";

                IdentityRole role = new IdentityRole(); 
                role.Name = "Administrator";
                context.Roles.Add(role);
                context.SaveChanges();

                UserStore<Person> employeeStore = new UserStore<Person>(context);
                UserManager<Person> employeeManager = new UserManager<Person>(employeeStore);

                employeeManager.Create(p, "Aa123456!");
                employeeManager.AddToRole(p.Id, "Administrator");

                context.SaveChanges();
            }
        }
    }
}
