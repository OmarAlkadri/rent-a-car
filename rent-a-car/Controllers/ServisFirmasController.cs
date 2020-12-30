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
    public class ServisFirmasController : Controller
    {
        private readonly rentacarContext _context;

        public ServisFirmasController(rentacarContext context)
        {
            _context = context;
        }

        // GET: ServisFirmas
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServisFirmas.ToListAsync());
        }

        // GET: ServisFirmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servisFirma = await _context.ServisFirmas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servisFirma == null)
            {
                return NotFound();
            }

            return View(servisFirma);
        }

        // GET: ServisFirmas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServisFirmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Servis")] ServisFirma servisFirma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servisFirma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servisFirma);
        }

        // GET: ServisFirmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servisFirma = await _context.ServisFirmas.FindAsync(id);
            if (servisFirma == null)
            {
                return NotFound();
            }
            return View(servisFirma);
        }

        // POST: ServisFirmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Servis")] ServisFirma servisFirma)
        {
            if (id != servisFirma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servisFirma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServisFirmaExists(servisFirma.Id))
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
            return View(servisFirma);
        }

        // GET: ServisFirmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servisFirma = await _context.ServisFirmas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servisFirma == null)
            {
                return NotFound();
            }

            return View(servisFirma);
        }

        // POST: ServisFirmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servisFirma = await _context.ServisFirmas.FindAsync(id);
            _context.ServisFirmas.Remove(servisFirma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServisFirmaExists(int id)
        {
            return _context.ServisFirmas.Any(e => e.Id == id);
        }
    }
}
