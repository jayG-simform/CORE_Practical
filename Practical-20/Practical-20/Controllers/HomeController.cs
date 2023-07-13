using Microsoft.AspNetCore.Mvc;

namespace Practical_20.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BadRequest()
        {
            return StatusCode(400);
        }
        public IActionResult InternalServer()
        {
            return StatusCode(500);
        }
        public IActionResult RequestURLTooLong()
        {
            return StatusCode(414);
        }
        public IActionResult NotFound()
        {
            return StatusCode(404);
        }
        public IActionResult HttpVersionNotSupport() 
        {
            return StatusCode(505);
        }
        public IActionResult Ambiguous()
        {
            return StatusCode(300);
        }
        public IActionResult MethodNotAllowed()
        {
            return StatusCode(405);
        }

    }
}
