using Microsoft.AspNetCore.Mvc;
using sdl7.Models;
using System.IO;
using Microsoft.Extensions.Configuration;
using System;

namespace sdl7.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var connString = System.IO.File.ReadAllText("sdl7.conf"); // Полный путь к System.IO.File
                var csb = new Npgsql.NpgsqlConnectionStringBuilder(connString)
                {
                    Username = model.UserName,
                    Password = model.Password
                };

                try
                {
                    using var connection = new Npgsql.NpgsqlConnection(csb.ToString());
                    connection.Open();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    using var logWriter = new StreamWriter(_configuration["LOG_FILE_PATH"], true);
                    logWriter.WriteLine($"{DateTime.Now}: Ошибка: Неправильное имя пользователя или пароль");
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View(model);
        }
    }
}


