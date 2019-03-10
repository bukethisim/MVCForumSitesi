using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCForumSitesi.Controllers
{
    public class QuestionsController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();

        // GET: Questions
        public ActionResult Index()
        {
            var list = _uw.Categories.GetAll();
            return View(list);
        }

        public ActionResult GetQuestions(int catid)
        {
            var cat = _uw.Categories.GetOne(catid);
            var list = _uw.Questions.Search(x => x.CategoryId == catid);
            return View(list);
        }
    }
}