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
        public ActionResult GetAnswers(int id, int? page)
        {
            Question q = _uw.Questions.GetOne(id);
            ViewBag.Image = q.ThumbnailURL;
            ViewBag.questionId = id;
            List<Answer> list; /*= _uw.Questions.Search(x => x.CategoryId == catid);*/
            if (page.HasValue)
            {
                int step = (page.Value - 1) * 3; //nullable ise .value alarak işlem yaparız.
                list = _uw.Answers.Search(x => x.QuestionId == id).Skip(step).Take(3).ToList();
            }
            else
            {
                list = _uw.Answers.Search(x => x.QuestionId == id).Take(3).ToList();
            }
            float AnsCount = _uw.Answers.Search(x => x.QuestionId == id).Count();
            double PageCount = Math.Ceiling(AnsCount / 3);
            int current = page.HasValue ? page.Value : 1;

            ViewBag.Start = current > 2 ? current - 2 : 1;
            ViewBag.End = current < PageCount - 2 ? current + 2 : PageCount;
            ViewBag.CurrentPage = current;

            ViewBag.PrevVisible = current > 1;
            ViewBag.NextVisible = current < PageCount;
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