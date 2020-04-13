using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CW_SITAIRIS.Models;
using CW_SITAIRIS.Models.AppDBContext;
using CW_SITAIRIS.Models.RegisterModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CW_SITAIRIS.Controllers.LoginController
{

    public class AccountController : Controller
    {
        public static int UserId { get; private set; }

        /*
         *-1 -- Not authentficated
         * 0 -- client
         * 1 -- admin
         * 2 -- manager
         */
        public static int status { get; private set; } = -1;

        private AppDBContext db;
        public AccountController(AppDBContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.users.FirstOrDefaultAsync(u => u.email == model.Email && u.password == model.Password);
                if (user != null)
                {
                    UserId = user.idUser;
                    status = (int) user.role;

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.users.FirstOrDefaultAsync(u => u.email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.users.Add(new User { email = model.Email, password = model.Password, name = model.UserFIO});
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            status = -1;
            return RedirectToAction("Login", "Account");
        }

        public static bool authentificated()
        {
            if (UserId != 0)
            {
                return true;
            }

            return false;
        }
    }
}