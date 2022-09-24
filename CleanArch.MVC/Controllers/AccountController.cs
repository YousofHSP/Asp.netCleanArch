using CleanArch.Application.Interfaces;
using CleanArch.Application.Security;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.MVC.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel data)
        {
            if(!ModelState.IsValid)
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
    }
}
