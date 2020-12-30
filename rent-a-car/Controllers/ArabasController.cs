using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rent_a_car;

namespace rent_a_car.Controllers
{
    public class ArabasController : Controller
    {
        private readonly rentacarContext _context;

        public ArabasController(rentacarContext context)
        {
            _context = context;
        }

        // GET: Arabas
        public async Task<IActionResult> Index()
        {
            var rentacarContext = _context.Arabas.Include(a => a.ArabaFir).Include(a => a.Servis);
            return View(await rentacarContext.ToListAsync());
        }

        // GET: Arabas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var araba = await _context.Arabas
                .Include(a => a.ArabaFir)
                .Include(a => a.Servis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (araba == null)
            {
                return NotFound();
            }

            return View(araba);
        }

        // GET: Arabas/Create
        public IActionResult Create()
        {
            ViewData["ArabaFirId"] = new SelectList(_context.ArabaFirmas, "Id", "Ad");
            ViewData["ServisId"] = new SelectList(_context.ServisFirmas, "Id", "Ad");
            return View();
        }

        // POST: Arabas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArabaFirId,Ad,ServisId")] Araba araba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(araba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArabaFirId"] = new SelectList(_context.ArabaFirmas, "Id", "Ad", araba.ArabaFirId);
            ViewData["ServisId"] = new SelectList(_context.ServisFirmas, "Id", "Ad", araba.ServisId);
            return View(araba);
        }

        // GET: Arabas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var araba = await _context.Arabas.FindAsync(id);
            if (araba == null)
            {
                return NotFound();
            }
            ViewData["ArabaFirId"] = new SelectList(_context.ArabaFirmas, "Id", "Ad", araba.ArabaFirId);
            ViewData["ServisId"] = new SelectList(_context.ServisFirmas, "Id", "Ad", araba.ServisId);
            return View(araba);
        }

        // POST: Arabas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArabaFirId,Ad,ServisId")] Araba araba)
        {
            if (id != araba.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(araba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArabaExists(araba.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArabaFirId"] = new SelectList(_context.ArabaFirmas, "Id", "Ad", araba.ArabaFirId);
            ViewData["ServisId"] = new SelectList(_context.ServisFirmas, "Id", "Ad", araba.ServisId);
            return View(araba);
        }

        // GET: Arabas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var araba = await _context.Arabas
                .Include(a => a.ArabaFir)
                .Include(a => a.Servis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (araba == null)
            {
                return NotFound();
            }

            return View(araba);
        }

        // POST: Arabas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var araba = await _context.Arabas.FindAsync(id);
            _context.Arabas.Remove(araba);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArabaExists(int id)
        {
            return _context.Arabas.Any(e => e.Id == id);
        }
    }
}
