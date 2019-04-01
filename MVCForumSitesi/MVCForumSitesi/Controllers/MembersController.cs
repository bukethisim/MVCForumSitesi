using BLL;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCForumSitesi.Controllers
{
    public class MembersController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();
        // GET: Members
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult Login(LoginViewModel info)
        {
            //  1.  Signin managera ulaş
            ApplicationSignInManager signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

            //  2.  Giriş yapmayı dene(result döner)
            SignInStatus result = signInManager.PasswordSignIn(info.Email, info.Password, true, false);

            //  3.  Sonucu döndür
            switch (result)
            {
                case SignInStatus.Success:
                    return Json(new { success = true });
                case SignInStatus.Failure:
                    return Json(new { success = false });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Person person, string Pass, HttpPostedFileBase img, bool hasLesson)
        {
            UserStore<Person> store = new UserStore<Person>(UnitOfWork.Create());
            UserManager<Person> manager = new UserManager<Person>(store);
            var result = manager.Create(person, Pass);
            if (hasLesson == true)
                person.hasLesson = true;
            else
                person.hasLesson = false;
            if (result.Succeeded)
            {
                if (img != null)
                {
                    string path = Server.MapPath("/Uploads/Members/");
                    img.SaveAs(path + person.Id + ".jpg");

                    person.HasPhoto = true;
                    manager.Update(person);

                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Errors = result.Errors;
            }
            return View();
        }

        public ActionResult Edit()
        {
            var id = User.Identity.GetUserId();
            ViewBag.Id = id;
            ViewBag.Questions = _uw.Questions.Search(x => x.PersonId == id);
            ViewBag.Photo = "/Uploads/Members/" + id + ".jpg";
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string email,string phone,HttpPostedFileBase image)
        {
            var id = User.Identity.GetUserId();
            ViewBag.Id = id;
            ViewBag.Questions = _uw.Questions.Search(x => x.PersonId == id);
            ViewBag.Photo = "/Uploads/Members/" + id + ".jpg";

            UserStore <Person> store = new UserStore<Person>(UnitOfWork.Create());
            UserManager<Person> manager = new UserManager<Person>(store);
            Person person = manager.FindById(id);  /*_uw.db.Users.Find(uid); aynı db kullanmamız gerekiyor.ondan managerdan aldık.*/
            person.Email = email;
            person.PhoneNumber = phone;
            if (image != null)
            {
                string path = Server.MapPath("/Uploads/Members/");
                string old = path + person.Id + ".jpg";

                if (System.IO.File.Exists(old))
                    System.IO.File.Delete(old);

                string _new = path + person.Id + ".jpg";
                image.SaveAs(_new);

                person.HasPhoto = true;
            }

            manager.Update(person);

            if (person.HasPhoto)
                ViewBag.Photo = "/Uploads/Members/" + id + ".jpg";

            return View();
        }

        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}