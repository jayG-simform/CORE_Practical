using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using View_Crud.Models;
using Newtonsoft.Json;
using DataAccessLayer.Model;
using DataAccessLayer.ViewModel;

namespace View_Crud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<StudentView> students = new List<StudentView>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7054/");
            HttpResponseMessage response = await client.GetAsync("api/Students"); 
            if(response.IsSuccessStatusCode) 
            { 
                var res = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<StudentView>>(res);
            }
            return View(students);
        }
        public async Task<IActionResult> Detail(int id)
        {
            StudentView student = await GetStudentById(id);
            return View(student);
        }

        private static async Task<StudentView> GetStudentById(int id)
        {
            StudentView student = new StudentView();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7054/");
            HttpResponseMessage response = await client.GetAsync($"api/Students/{id}");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentView>(res);
            }

            return student;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentView student)
        {
            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7054/");
                var response = await client.PostAsJsonAsync<StudentView>("api/Students",student);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            StudentView student = new StudentView();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7054/");
            HttpResponseMessage response = await client.DeleteAsync($"api/Students/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            StudentView student = await GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentView student)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7054/");
            var response = await client.PutAsJsonAsync<StudentView>($"api/Students/{student.Id}", student);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}