using GuarderiaApp.Data;
using GuarderiaApp.Models;
using Microsoft.AspNetCore.Mvc;
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
            var niños = await _context.Niños.ToListAsync();
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
        public async Task<IActionResult> Create(Niño niño)
        {
            if (ModelState.IsValid)
            {
                _context.Add(niño);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "¡Niño registrado exitosamente!";

                return RedirectToAction(nameof(Index)); // Redirige para limpiar el formulario
                
            }
            return View(niño);
        }

        // GET: Niños/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var niño = await _context.Niños.FirstOrDefaultAsync(m => m.Id == id);
            if (niño == null) return NotFound();

            return View(niño);
        }

        // GET: Niños/Edit/5 Editar carajitos
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var niño = await _context.Niños.FindAsync(id);
            if (niño == null) return NotFound();

            return View(niño);
        }

        // POST: Niños/Edit/5 Editar carajitos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Niño niño)
        {
            if (id != niño.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: Niños/Delete/5 Borrar carajitos
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var niño = await _context.Niños.FirstOrDefaultAsync(m => m.Id == id);
            if (niño == null) return NotFound();

            return View(niño);
        }

        // POST: Niños/Delete/5 Borrar carajitos
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var niño = await _context.Niños.FindAsync(id);
            if (niño != null)
            {
                _context.Niños.Remove(niño);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
