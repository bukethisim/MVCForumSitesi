using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCForumSitesi.Controllers
{
    public class AnswersController : Controller
    {
        // GET: Answers
        UnitOfWork _uw = new UnitOfWork();
        public ActionResult GetAnswers(int id)
        {
           var list = _uw.Answers.Search(x => x.QuestionId == id);
            return View(list);
        }

        public ActionResult CreateAnswer(int questionid)
        {
            return View();
        }
    }
}