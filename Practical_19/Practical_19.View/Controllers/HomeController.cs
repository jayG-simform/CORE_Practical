using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practical_19.Models.Models;
using Practical_19.View.Models;
using System.Diagnostics;
using System.Net;

namespace Practical_19.View.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("api");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllUser()
        {
            if (Request.Cookies["Email"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var token = Request.Cookies["userToken"].ToString();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("User/Users");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                List<RegisterUser> users = JsonConvert.DeserializeObject<List<RegisterUser>>(data);
                return View(users);
            }
            return View(new List<RegisterUser>());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}