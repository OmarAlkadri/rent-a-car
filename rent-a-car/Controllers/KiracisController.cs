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
    public class KiracisController : Controller
    {
        private readonly rentacarContext _context;

        public KiracisController(rentacarContext context)
        {
            _context = context;
        }

        // GET: Kiracis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kiracis.ToListAsync());
        }

        // GET: Kiracis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kiraci = await _context.Kiracis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kiraci == null)
            {
                return NotFound();
            }

            return View(kiraci);
        }

        // GET: Kiracis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kiracis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Soyad,Yas")] Kiraci kiraci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kiraci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kiraci);
        }

        // GET: Kiracis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kiraci = await _context.Kiracis.FindAsync(id);
            if (kiraci == null)
            {
                return NotFound();
            }
            return View(kiraci);
        }

        // POST: Kiracis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Soyad,Yas")] Kiraci kiraci)
        {
            if (id != kiraci.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kiraci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KiraciExists(kiraci.Id))
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
            return View(kiraci);
        }

        // GET: Kiracis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kiraci = await _context.Kiracis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kiraci == null)
            {
                return NotFound();
            }

            return View(kiraci);
        }

        // POST: Kiracis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kiraci = await _context.Kiracis.FindAsync(id);
            _context.Kiracis.Remove(kiraci);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KiraciExists(int id)
        {
            return _context.Kiracis.Any(e => e.Id == id);
        }
    }
}
