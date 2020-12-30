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
    public class IlanKoysController : Controller
    {
        private readonly rentacarContext _context;

        public IlanKoysController(rentacarContext context)
        {
            _context = context;
        }

        // GET: IlanKoys
        public async Task<IActionResult> Index()
        {
            var rentacarContext = _context.IlanKoys.Include(i => i.Araba).Include(i => i.Personel);
            return View(await rentacarContext.ToListAsync());
        }

        // GET: IlanKoys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilanKoy = await _context.IlanKoys
                .Include(i => i.Araba)
                .Include(i => i.Personel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilanKoy == null)
            {
                return NotFound();
            }

            return View(ilanKoy);
        }

        // GET: IlanKoys/Create
        public IActionResult Create()
        {
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad");
            ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id");
            return View();
        }

        // POST: IlanKoys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArabaId,PersonelId,Tarih,Fiyat")] IlanKoy ilanKoy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilanKoy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", ilanKoy.ArabaId);
            ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", ilanKoy.PersonelId);
            return View(ilanKoy);
        }

        // GET: IlanKoys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilanKoy = await _context.IlanKoys.FindAsync(id);
            if (ilanKoy == null)
            {
                return NotFound();
            }
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", ilanKoy.ArabaId);
            ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", ilanKoy.PersonelId);
            return View(ilanKoy);
        }

        // POST: IlanKoys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArabaId,PersonelId,Tarih,Fiyat")] IlanKoy ilanKoy)
        {
            if (id != ilanKoy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilanKoy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlanKoyExists(ilanKoy.Id))
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
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", ilanKoy.ArabaId);
            ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", ilanKoy.PersonelId);
            return View(ilanKoy);
        }

        // GET: IlanKoys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilanKoy = await _context.IlanKoys
                .Include(i => i.Araba)
                .Include(i => i.Personel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilanKoy == null)
            {
                return NotFound();
            }

            return View(ilanKoy);
        }

        // POST: IlanKoys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilanKoy = await _context.IlanKoys.FindAsync(id);
            _context.IlanKoys.Remove(ilanKoy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlanKoyExists(int id)
        {
            return _context.IlanKoys.Any(e => e.Id == id);
        }
    }
}
