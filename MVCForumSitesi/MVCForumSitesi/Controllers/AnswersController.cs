using BLL;
using Entity;
using Microsoft.AspNet.Identity;
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
            Question q = _uw.Questions.GetOne(id);
            ViewBag.Image = q.ThumbnailURL;
            var list = _uw.Answers.Search(x => x.QuestionId == id);
            return View(list);
        }

        [HttpGet]
        public ActionResult CreateAnswer(int questionid)
        {
            var question = _uw.Questions.GetOne(questionid);
            TempData["questionıd"] = question.Id;
            TempData["catıd"] = question.CategoryId;
            ViewBag.QuestionPic = question.ThumbnailURL;
            return View();
        }

        [HttpPost]
        public ActionResult CreateAnswer(string answer)
        {
            Answer ans = new Answer();
            ans.AnswerContent = answer;
            ans.PersonId = User.Identity.GetUserId();
            ans.QuestionId = (int)TempData["questionıd"];

            _uw.Answers.Add(ans);
            _uw.Complete();

            var question = _uw.Questions.GetOne(ans.QuestionId);
            ViewBag.QuestionPic = question.ThumbnailURL;
            return RedirectToAction("GetAnswers", new { id = ans.QuestionId });
        }
    }
}