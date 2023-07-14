using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practical_19.Models.Models;
using System.Net;

namespace Practical_19.View.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }

        [HttpGet]
        public async Task<IActionResult> RegisterAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _httpClient.PostAsJsonAsync("User/Register", model);
                var msg = result.Content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Login");

                }
                ModelState.AddModelError("", msg.Result);
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _httpClient.PostAsJsonAsync("User/Login", model);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var content = await result.Content.ReadAsStringAsync();
                UserManagerResponse userManager = JsonConvert.DeserializeObject<UserManagerResponse>(content);

                Response.Cookies.Append("userToken", userManager.Message);
                Response.Cookies.Append("Email", userManager.Email);
                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("", "Data can't added Try again!");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            LogoutModel logout = new LogoutModel();
            string data = Request.Cookies["Email"].ToString();
            Response.Cookies.Delete("Email");
            Response.Cookies.Delete("userToken");

            logout.Email = data;
            var result = await _httpClient.PostAsJsonAsync("User/Logout", logout);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
