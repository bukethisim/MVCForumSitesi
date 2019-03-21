using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCForumSitesi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        // GET: Administrator
        public ActionResult Categories()
        {
            return View(_uw.Categories.GetAll());
        }

        [HttpGet]
        public ActionResult CreateCategories()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategories(Category newCategory, HttpPostedFileBase image)
        {
            Category c = new Category();
            c.CategoryName = newCategory.CategoryName;

            if (image != null)
            {
                string path = Server.MapPath("/Uploads/Categories/");
                string thumbpath = path + "CategoriesImageUrl/";
                string largepath = path + "image/";

                image.SaveAs(largepath + image.FileName);

                Image i = Image.FromFile(largepath + image.FileName);

                Size s = new Size(400, 400);

                Image small = ImageHelper.ResizeImage(i, s);

                small.Save(thumbpath + image.FileName);

                i.Dispose();

                c.CategoryImageUrl = "/Uploads/Categories/CategoriesImageUrl/" + image.FileName;

            }
            else
                c.CategoryImageUrl = @"\Content\Theme\images\project-4.jpg";

            _uw.Categories.Add(c);
            _uw.Complete();
            return RedirectToAction("Categories");
        }

        public ActionResult Delete(int id)
        {
            Category selected = _uw.Categories.GetOne(id);
            var selectedQuestions = _uw.Questions.Search(x => x.CategoryId == id);
            var path = Server.MapPath("/");
            foreach (var question in selectedQuestions)
            {
                var selectedAnswers = _uw.Answers.Search(x => x.QuestionId == question.Id);

                foreach (var answer in selectedAnswers)
                {
                    _uw.Answers.Delete(answer.Id);
                }

                var l = path + question.QuestionUrl;
                var t = path + question.ThumbnailURL;

                System.IO.File.Delete(l);
                System.IO.File.Delete(t);
                _uw.Questions.Delete(question.Id);
            }

            var lc = path + selected.CategoryImageUrl;
            System.IO.File.Delete(lc);

            _uw.Categories.Delete(id);
            _uw.Complete();
            return RedirectToAction("Categories");
        }
    }
}