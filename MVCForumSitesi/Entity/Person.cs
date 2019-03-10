using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Person : IdentityUser
    {
        public int Like { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public bool HasPhoto { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Person> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Teacher : Person
    {
        public bool hasLesson { get; set; }
        public int CategoryId { get; set; }
        //public virtual Category Category { get; set; }
    }

    public class Student : Person
    {
        public virtual List<Question> Questions { get; set; }
    }
}
