using Microsoft.AspNetCore.Mvc;

namespace Practical_16.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> logger;
        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }
        [HttpGet(Name ="GetString")]
        public string Get()
        {
            logger.LogInformation("Hello World with Api");
            return "Hello World";
        }
    }
}
