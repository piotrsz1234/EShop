using EShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Login = EShop.Web.Models.Login;

namespace EShop.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Registration registration)
        {
            if (ModelState.IsValid)
            {
                //TO DO
                return RedirectToAction("Message");
            }
            return View();
        }

        public ActionResult Message()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Login login)
        {
            if (ModelState.IsValid)
            {
                //TO DO
                return RedirectToAction("Message");
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            if (ModelState.IsValid)
            {
                //TO DO
                //return RedirectToAction("Message");
                ViewBag.Msg = "Wysłaliśmy wiadomość na e-maila: " + (string)forgotPassword.Email;
            }
            else
            {
                ViewBag.Msg = "Dane nieprawidłowe";
            }
            return View();
        }
    }
}