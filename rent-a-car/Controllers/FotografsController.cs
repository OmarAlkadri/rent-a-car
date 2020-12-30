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
    public class FotografsController : Controller
    {
        private readonly rentacarContext _context;

        public FotografsController(rentacarContext context)
        {
            _context = context;
        }

        // GET: Fotografs
        public async Task<IActionResult> Index()
        {
            var rentacarContext = _context.Fotografs.Include(f => f.Araba);
            return View(await rentacarContext.ToListAsync());
        }

        // GET: Fotografs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotograf = await _context.Fotografs
                .Include(f => f.Araba)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotograf == null)
            {
                return NotFound();
            }

            return View(fotograf);
        }

        // GET: Fotografs/Create
        public IActionResult Create()
        {
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad");
            return View();
        }

        // POST: Fotografs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArabaId,Fotograf1")] Fotograf fotograf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fotograf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", fotograf.ArabaId);
            return View(fotograf);
        }

        // GET: Fotografs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotograf = await _context.Fotografs.FindAsync(id);
            if (fotograf == null)
            {
                return NotFound();
            }
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", fotograf.ArabaId);
            return View(fotograf);
        }

        // POST: Fotografs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArabaId,Fotograf1")] Fotograf fotograf)
        {
            if (id != fotograf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotograf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotografExists(fotograf.Id))
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
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", fotograf.ArabaId);
            return View(fotograf);
        }

        // GET: Fotografs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotograf = await _context.Fotografs
                .Include(f => f.Araba)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotograf == null)
            {
                return NotFound();
            }

            return View(fotograf);
        }

        // POST: Fotografs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotograf = await _context.Fotografs.FindAsync(id);
            _context.Fotografs.Remove(fotograf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotografExists(int id)
        {
            return _context.Fotografs.Any(e => e.Id == id);
        }
    }
}
