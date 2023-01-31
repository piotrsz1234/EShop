﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EShop.Web.Identity;
using EShop.Web.Models;
using System.Web.Mvc;
using EShop.Core.Entities;
using EShop.Core.Extensions;
using EShop.Dtos.User.Dtos;
using EShop.Web.Helpers;
using Microsoft.AspNet.Identity.Owin;
using Login = EShop.Web.Models.Login;
using EShop.Core.Domain;
using EShop.Core.Infrastructure.Repositories;
using System.Collections.Generic;

namespace EShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopSignInManager _shopSignInManager;
        private readonly IUserService _userService;

        public AccountController(ShopSignInManager shopSignInManager, IUserService userService)
        {
            _shopSignInManager = shopSignInManager;
            _userService = userService;
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
            if (ModelState.IsValid) {
                var user = new User() {
                    Email = registration.Email,
                    UserName = registration.Username,
                    PasswordHash = registration.Password.CreateHash(),
                    PhoneNumber = registration.PhoneNumber,
                    InsertDateUtc = DateTime.UtcNow,
                    ModificationDateUtc = DateTime.UtcNow,
                };

                await _shopSignInManager.SignUp(user);

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
        public async Task<ActionResult> SignIn(Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _shopSignInManager.UserManager.FindAsync(login.Username, login.Password.CreateHash());

                if (user == null) return View();
                
                _shopSignInManager.SignIn(user, false, true);

                SessionHelper.LoggedUser = new LoggedUserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    IsAdmin = user.UserRole.Any(x => x.Role.IsAdmin)
                };
                
                return RedirectToAction("ProductList", "Product");
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

        [HttpGet]
        public async Task<ActionResult> Profile()
        {
            LoggedUserDto user = SessionHelper.LoggedUser;
            User userData = await _userService.GetUserAsync(user.Id);
            return View();
        }

        [Authorize]
        public ActionResult SignOut()
        {
            _shopSignInManager.Logout();

            return RedirectToAction("SignIn");
        }
    }
}