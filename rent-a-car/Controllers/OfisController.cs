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
    public class OfisController : Controller
    {
        private readonly rentacarContext _context;

        public OfisController(rentacarContext context)
        {
            _context = context;
        }

        // GET: Ofis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ofis.ToListAsync());
        }

        // GET: Ofis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofi = await _context.Ofis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ofi == null)
            {
                return NotFound();
            }

            return View(ofi);
        }

        // GET: Ofis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ofis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Ofi ofi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ofi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ofi);
        }

        // GET: Ofis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofi = await _context.Ofis.FindAsync(id);
            if (ofi == null)
            {
                return NotFound();
            }
            return View(ofi);
        }

        // POST: Ofis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Ofi ofi)
        {
            if (id != ofi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ofi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfiExists(ofi.Id))
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
            return View(ofi);
        }

        // GET: Ofis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofi = await _context.Ofis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ofi == null)
            {
                return NotFound();
            }

            return View(ofi);
        }

        // POST: Ofis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ofi = await _context.Ofis.FindAsync(id);
            _context.Ofis.Remove(ofi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfiExists(int id)
        {
            return _context.Ofis.Any(e => e.Id == id);
        }
    }
}
