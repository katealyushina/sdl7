using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using sdl7.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sdl7.Controllers
{
    [Authorize] // Требуется аутентификация для всех методов в контроллере
    public class DataController : Controller
    {
        private readonly DatabaseContext _context;

        public DataController(DatabaseContext context)
        {
            _context = context;
        }

        // Просмотр данных
        public async Task<IActionResult> ViewData()
        {
            var materials = await _context.Materials.ToListAsync();
            return View(materials);
        }

        // Добавление новой сущности
        [HttpGet]
        public IActionResult CreateEntity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntity(Material model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ViewData));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ошибка при добавлении данных: {ex.Message}");
                }
            }

            return View(model);
        }

        // Обновление сущности
        [HttpGet]
        public async Task<IActionResult> UpdateEntity(int id)
        {
            var model = await _context.Materials.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEntity(Material model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ViewData));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ошибка при обновлении данных: {ex.Message}");
                }
            }

            return View(model);
        }
    }
}
