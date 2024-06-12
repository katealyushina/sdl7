using Microsoft.AspNetCore.Mvc;

namespace sdl7.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
