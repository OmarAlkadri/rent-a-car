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
    public class ArabaFirmasController : Controller
    {
        private readonly rentacarContext _context;

        public ArabaFirmasController(rentacarContext context)
        {
            _context = context;
        }

        // GET: ArabaFirmas
        public async Task<IActionResult> Index()
        {
            var rentacarContext = _context.ArabaFirmas.Include(a => a.Adres);
            return View(await rentacarContext.ToListAsync());
        }

        // GET: ArabaFirmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arabaFirma = await _context.ArabaFirmas
                .Include(a => a.Adres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arabaFirma == null)
            {
                return NotFound();
            }

            return View(arabaFirma);
        }

        // GET: ArabaFirmas/Create
        public IActionResult Create()
        {
            ViewData["AdresId"] = new SelectList(_context.Adres, "Id", "Il");
            return View();
        }

        // POST: ArabaFirmas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,FirmaSahibi,Telefon,Email,AdresId")] ArabaFirma arabaFirma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arabaFirma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresId"] = new SelectList(_context.Adres, "Id", "Il", arabaFirma.AdresId);
            return View(arabaFirma);
        }

        // GET: ArabaFirmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arabaFirma = await _context.ArabaFirmas.FindAsync(id);
            if (arabaFirma == null)
            {
                return NotFound();
            }
            ViewData["AdresId"] = new SelectList(_context.Adres, "Id", "Il", arabaFirma.AdresId);
            return View(arabaFirma);
        }

        // POST: ArabaFirmas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,FirmaSahibi,Telefon,Email,AdresId")] ArabaFirma arabaFirma)
        {
            if (id != arabaFirma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arabaFirma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArabaFirmaExists(arabaFirma.Id))
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
            ViewData["AdresId"] = new SelectList(_context.Adres, "Id", "Il", arabaFirma.AdresId);
            return View(arabaFirma);
        }

        // GET: ArabaFirmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arabaFirma = await _context.ArabaFirmas
                .Include(a => a.Adres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arabaFirma == null)
            {
                return NotFound();
            }

            return View(arabaFirma);
        }

        // POST: ArabaFirmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arabaFirma = await _context.ArabaFirmas.FindAsync(id);
            _context.ArabaFirmas.Remove(arabaFirma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArabaFirmaExists(int id)
        {
            return _context.ArabaFirmas.Any(e => e.Id == id);
        }
    }
}
