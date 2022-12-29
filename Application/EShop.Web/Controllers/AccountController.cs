using EShop.Web.Identity;
using EShop.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
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
        public ActionResult SignUp(Registration registration)
        {
            if (ModelState.IsValid)
            {
                //_shopSignInManager.CreateUserIdentity(new )
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