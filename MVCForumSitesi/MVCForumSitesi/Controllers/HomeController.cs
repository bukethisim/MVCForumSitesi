using BLL;
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
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Questions()
        {
            var list = _uw.Categories.GetAll();
           
            return View(list);
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}