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
    public class AdresController : Controller
    {
        private readonly rentacarContext _context;

        public AdresController(rentacarContext context)
        {
            _context = context;
        }

        // GET: Adres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adres.ToListAsync());
        }

        // GET: Adres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adre = await _context.Adres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adre == null)
            {
                return NotFound();
            }

            return View(adre);
        }

        // GET: Adres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ilce,Il,Satir1,Satir2")] Adre adre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adre);
        }

        // GET: Adres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adre = await _context.Adres.FindAsync(id);
            if (adre == null)
            {
                return NotFound();
            }
            return View(adre);
        }

        // POST: Adres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ilce,Il,Satir1,Satir2")] Adre adre)
        {
            if (id != adre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdreExists(adre.Id))
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
            return View(adre);
        }

        // GET: Adres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adre = await _context.Adres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adre == null)
            {
                return NotFound();
            }

            return View(adre);
        }

        // POST: Adres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adre = await _context.Adres.FindAsync(id);
            _context.Adres.Remove(adre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdreExists(int id)
        {
            return _context.Adres.Any(e => e.Id == id);
        }
    }
}
