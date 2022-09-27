using CleanArch.Application.Interfaces;
using CleanArch.Application.Security;
using CleanArch.Application.ViewModels.Account;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArch.MVC.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel data)
        {
            if (!ModelState.IsValid)
                return View(data);
            CheckUser checkUser = _userService.CheckUser(data.Username, data.Email);
            if (checkUser != CheckUser.Ok)
            {
                ViewBag.Check = checkUser;
                return View(data);
            }

            User user = new User()
            {
                Email = data.Email.Trim().ToLower(),
                Password = PasswordHelper.EncodePasswordMd5(data.Password),
                Username = data.Username
            };
            _userService.Register(user);
            return View("SuccessRegister", user);
        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login(string ReturnUrl = "/")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel data, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            if (!_userService.IsExistUser(data.Email, data.Password))
            {
                ModelState.AddModelError("Email", "User Not Found");
                return View(data);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, data.Email),
                new Claim(ClaimTypes.Name, data.Email)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = data.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);
            return Redirect(ReturnUrl);
        }
        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        #endregion
    }
}
