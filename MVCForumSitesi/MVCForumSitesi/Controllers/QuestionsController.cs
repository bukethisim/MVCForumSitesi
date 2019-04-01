using BLL;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public ActionResult GetQuestions(int catid, int? page)
        {
            ViewBag.Person = User.Identity.GetUserId();
            var cat = _uw.Categories.GetOne(catid);
            ViewBag.catId = catid;
            List<Question> list; /*= _uw.Questions.Search(x => x.CategoryId == catid);*/
            if (page.HasValue)
            {
                int step = (page.Value - 1) * 3; //nullable ise .value alarak işlem yaparız.
                list = _uw.Questions.Search(x => x.CategoryId == catid).Skip(step).Take(3).ToList();
            }
            else
            {
                list = _uw.Questions.Search(x=> x.CategoryId == catid).Take(3).ToList();
            }
            float QuestionCount = _uw.Questions.GetAll().Count();
            double PageCount = Math.Ceiling(QuestionCount / 3);
            int current = page.HasValue ? page.Value : 1;

            ViewBag.Start = current > 2 ? current - 2 : 1;
            ViewBag.End = current < PageCount - 2 ? current + 2 : PageCount;
            ViewBag.CurrentPage = current;

            ViewBag.PrevVisible = current > 1;
            ViewBag.NextVisible = current < PageCount;
            return View(list);
        }

        [HttpGet]
        public ActionResult CreateQuestion()
        {
            List<SelectListItem> categories = _uw.Categories.GetAll()
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
        public ActionResult CreateQuestion(QuestionCreateViewModel question, HttpPostedFileBase imagefile)
        {
            List<SelectListItem> categories = _uw.Categories.GetAll()
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

            Question q = new Question();
            q.CategoryId = question.CategoryId;
            q.QuestionContent = question.QuestionContent;
            q.PersonId = User.Identity.GetUserId();

            if (imagefile != null)
            {
                string path = Server.MapPath("/Uploads/Questions/");
                string thumbpath = path + "thumb/";
                string largepath = path + "large/";
                string unique = DateTime.Now.Millisecond.ToString();
                string guid = Guid.NewGuid().ToString();

                imagefile.SaveAs(largepath + guid + unique + ".jpg");

                Image i = Image.FromFile(largepath +
                   guid + unique + ".jpg");

                Size s = new Size(400, 400);

                Image small = ImageHelper.ResizeImage(i, s);

                small.Save(thumbpath + guid + unique + ".jpg");

                i.Dispose();

                //img src içinde göstereceğimiz için relative path kaydediyoruz.
                q.QuestionUrl = "/Uploads/Questions/large/" + guid + unique + ".jpg";
                q.ThumbnailURL = "/Uploads/Questions/thumb/" + guid + unique + ".jpg";

            }
            else
                q.QuestionUrl = @"C:\Users\craft\Desktop\WA Benim yaptıklarım\MVCForumSitesi\MVCForumSitesi\MVCForumSitesi\Content\Image\iconfinder_0602-question-mark_206748 (1).png";

            _uw.Questions.Add(q);
            _uw.Complete();
            return View();
        }

        public JsonResult DeleteQuestion(int id)
        {
            try
            {
                var list = _uw.Answers.Search(x => x.QuestionId == id);
                if (list.Count != 0)
                {
                    foreach (var item in list)
                    {
                        _uw.Answers.Delete(item.Id);
                    }
                    _uw.Complete();
                }
                _uw.Questions.Delete(id);
                _uw.Complete();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }

        }
    }
}