using BLL;
using Entity;
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

        [HttpGet]
        public ActionResult CreateQuestion()
        {
            List<SelectListItem> categories= _uw.Categories.GetAll()
              .Select(x => new SelectListItem()
              {
                  Text = x.CategoryName,
                  Value = x.Id.ToString()
              }).ToList();

            categories.Insert(0, new SelectListItem()
            {
                Text = "Seçiniz",
                Value = ""
            });
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult CreateQuestion(Question question,HttpPostedFileBase image)
        {
           
            return View();
        }
    }
}