using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Client
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello");
        }
    }
}