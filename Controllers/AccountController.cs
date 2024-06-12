using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sdl7.Data;
using sdl7.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace sdl7.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;

        public AccountController(ILogger<AccountController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Для простоты просто проверяем пароль на "password"
                if (model.Password == "password")
                {
                    // Сохраняем пользователя в сессии
                    HttpContext.Session.SetString("UserName", model.UserName);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    _logger.LogWarning("Invalid login attempt for user {UserName}", model.UserName);
                }
            }
            return View(model);
        }
    }
}
