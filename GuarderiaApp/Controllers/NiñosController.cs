using GuarderiaApp.Data;
using GuarderiaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuarderiaApp.Controllers
{
    public class NiñosController : Controller
    {
        private readonly GuarderiaDbContext _context;

        public NiñosController(GuarderiaDbContext context)
        {
            _context = context;
        }

        // Acción para listar los niños
        public async Task<IActionResult> Index()
        {
            var niños = await _context.Niños
                .Include(n => n.PersonasAutorizadas)
                .ToListAsync();
            return View(niños);
        }

        // GET: Niños/Create - Mostrar formulario
        public IActionResult Create()
        {
            var model = new NiñoConPersonasViewModel
            {
                Niño = new Niño(),
                PersonasAutorizadas = new List<PersonaAutorizada> { new PersonaAutorizada() }
            };
            return View(model);
        }

        // POST: Niños/Create - Procesar formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NiñoConPersonasViewModel model)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Niños.Add(model.Niño);
                    await _context.SaveChangesAsync();

                    foreach (var persona in model.PersonasAutorizadas)
                    {
                        if (!string.IsNullOrWhiteSpace(persona.Nombre))
                        {
                            persona.Niños = new List<Niño> { model.Niño };
                            _context.PersonasAutorizadas.Add(persona);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    TempData["SuccessMessage"] = "¡Niño registrado exitosamente!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Ocurrió un error al guardar los datos.");
                }
            }

            return View(model);
        }

        // GET: Niños/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var niño = await _context.Niños
                .Include(n => n.PersonasAutorizadas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (niño == null) return NotFound();

            return View(niño);
        }
    }
}
