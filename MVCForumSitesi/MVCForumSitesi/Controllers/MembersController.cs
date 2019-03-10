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
        public ActionResult Register(Person person, string Pass, HttpPostedFileBase img)
        {
            UserStore<Person> store = new UserStore<Person>(UnitOfWork.Create()); 
            UserManager<Person> manager = new UserManager<Person>(store);
            var result = manager.Create(person, Pass);
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

        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}