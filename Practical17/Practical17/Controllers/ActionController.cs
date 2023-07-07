using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practical17.Interface;
using Practical17.Models;
using System.Security.Claims;
using NuGet.Protocol.Core.Types;
using Practical17.Enum;
using Practical17.ViewModel;

namespace Practical17.Controllers
{
    public class ActionController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ActionController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Users model)
        {
            _userRepository.Add(model);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (model == null)
            {
                return NotFound();
            }
            UserLoginStatus status = await _userRepository.LoginUserAsync(model);

            switch (status)
            {
                case UserLoginStatus.UserNull:
                    return NotFound();
                    break;
                case UserLoginStatus.UserNotFound:
                    ModelState.AddModelError("", "No such Account found!");
                    return View(model);
                    break;

                case UserLoginStatus.InvalidPassword:
                    ModelState.AddModelError("", "Invalid User Id and Password");
                    return View(model);
                    break;

                case UserLoginStatus.LoginSuccess:
                    await LoginUserAsync(model);

                    return RedirectToAction("Index", "Student");
                    break;
            }
            return RedirectToAction("Index", "Student");

        }
        private async Task LoginUserAsync(LoginUser model)
        {
            Users user = await _userRepository.GetUserByEmailAsync(model.Email);
            List<string> roles = _userRepository.GetUserRoles(user.Id);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, model.Email));
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple);
            HttpContext.Session.SetString("Email", user.Email);

        }
        public async Task<IActionResult> Logout()
        {
            var cookieKeys = Request.Cookies.Keys;
            foreach (var cookieKey in cookieKeys)
            {
                Response.Cookies.Delete(cookieKey);
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
