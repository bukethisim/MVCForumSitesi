using BLL;
using Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCForumSitesi.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        public ActionResult Index()
        {
            UserStore<Person> store = new UserStore<Person>(UnitOfWork.Create());
            UserManager<Person> manager = new UserManager<Person>(store);
            ViewBag.Category = _uw.Categories.GetAll().Count();
            ViewBag.Student = manager.Users.Where(x => x.hasLesson == false).Count();
            ViewBag.Teacher = manager.Users.Where(x => x.hasLesson == true).Count();
            ViewBag.TotalQ = _uw.Questions.GetAll().Count();
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}