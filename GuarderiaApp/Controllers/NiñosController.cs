using GuarderiaApp.Data;
using GuarderiaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GuarderiaApp.Controllers
{
    public class NiñosController : Controller
    {
        private readonly GuarderiaDbContext _context;

        public NiñosController(GuarderiaDbContext context)
        {
            _context = context;
        }

        // GET: Niños
        public async Task<IActionResult> Index()
        {
            var niños = await _context.Niños
                .Include(n => n.PersonasAutorizadas)
                .ToListAsync();
            return View(niños);
        }

        // GET: Niños/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Niños/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroMatricula,Nombre,FechaNacimiento,FechaIngreso,FechaBaja,Alergias,ResponsablePagoId,PersonasAutorizadas")] Niño niño)
        {
            if (ModelState.IsValid)
            {
                // Relacionar personas autorizadas con el niño
                if (niño.PersonasAutorizadas != null && niño.PersonasAutorizadas.Count > 0)
                {
                    foreach (var persona in niño.PersonasAutorizadas)
                    {
                        persona.Niño = niño;
                    }
                }

                _context.Add(niño);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay error, recargar responsables disponibles por si la vista los necesita
            ViewData["ResponsablePagoId"] = new SelectList(_context.Responsables, "Id", "Nombre", niño.ResponsablePagoId);
            return View(niño);
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

        // GET: Niños/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var niño = await _context.Niños
                .Include(n => n.PersonasAutorizadas)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (niño == null) return NotFound();

            return View(niño);
        }

        // POST: Niños/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Niño niño)
        {
            if (id != niño.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Eliminar personas actuales y volver a agregarlas para evitar conflictos
                    var personasExistentes = _context.PersonasAutorizadas.Where(p => p.NiñoId == niño.Id);
                    _context.PersonasAutorizadas.RemoveRange(personasExistentes);

                    if (niño.PersonasAutorizadas != null)
                    {
                        foreach (var persona in niño.PersonasAutorizadas)
                        {
                            persona.NiñoId = niño.Id;
                            _context.PersonasAutorizadas.Add(persona);
                        }
                    }

                    _context.Update(niño);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Niños.Any(e => e.Id == niño.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(niño);
        }

        // GET: Niños/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var niño = await _context.Niños
                .Include(n => n.PersonasAutorizadas)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (niño == null) return NotFound();

            return View(niño);
        }

        // POST: Niños/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var niño = await _context.Niños
                .Include(n => n.PersonasAutorizadas)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (niño != null)
            {
                _context.PersonasAutorizadas.RemoveRange(niño.PersonasAutorizadas);
                _context.Niños.Remove(niño);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
