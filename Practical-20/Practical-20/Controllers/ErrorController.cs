using Microsoft.AspNetCore.Mvc;

namespace Practical_20.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult BadRequest()
        {
            return View();
        }
        public IActionResult InternalServer()
        {
            return View();
        }
        public IActionResult RequestURLTooLong()
        {
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult HttpVersionNotSupport()
        {
            return View();
        }
        public IActionResult Ambiguous()
        {
            return View();
        }
        public IActionResult MethodNotAllowed()
        {
            return View();
        }
    }
}
