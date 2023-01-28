using System;
using System.Threading.Tasks;
using EShop.Web.Identity;
using EShop.Web.Models;
using System.Web.Mvc;
using EShop.Core.Entities;
using EShop.Core.Extensions;
using Microsoft.AspNet.Identity.Owin;
using Login = EShop.Web.Models.Login;

namespace EShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopSignInManager _shopSignInManager;

        public AccountController(ShopSignInManager shopSignInManager)
        {
            _shopSignInManager = shopSignInManager;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            //TO DO
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(Registration registration)
        {
            if (ModelState.IsValid)
            {
                var temp  = await _shopSignInManager.SignUp (new User()
                {
                    Email = registration.Email,
                    UserName = registration.Username,
                    PasswordHash = registration.Password.CreateHash(),
                    PhoneNumber = registration.PhoneNumber,
                    InsertDateUtc = DateTime.UtcNow,
                    ModificationDateUtc = DateTime.UtcNow,
                });
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