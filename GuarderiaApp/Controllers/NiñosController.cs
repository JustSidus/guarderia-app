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
                .Include(n => n.PersonasAutorizadas) // Incluir personas autorizadas
                .ToListAsync();
            return View(niños);
        }

        // Acción para mostrar el formulario de creación
        public IActionResult Create()
        {
            var model = new NiñoConPersonasViewModel
            {
                Niño = new Niño(),
                PersonasAutorizadas = new List<PersonaAutorizada> { new PersonaAutorizada() }
            };
            return View(model);
        }

        // Acción para procesar la creación de un niño con personas autorizadas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NiñoConPersonasViewModel model)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Guardar el niño primero
                    _context.Niños.Add(model.Niño);
                    await _context.SaveChangesAsync();

                    // Guardar cada persona autorizada y establecer la relación
                    foreach (var persona in model.PersonasAutorizadas)
                    {
                        if (!string.IsNullOrWhiteSpace(persona.Nombre)) // Solo guardar si tiene datos
                        {
                            persona.Niños = new List<Niño> { model.Niño };
                            _context.PersonasAutorizadas.Add(persona);
                        }
                    }
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Ocurrió un error al guardar los datos.");
                }
            }

            // Si hay errores, volver a mostrar el formulario
            return View(model);
        }
    }
}