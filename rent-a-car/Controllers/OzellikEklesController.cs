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
    public class OzellikEklesController : Controller
    {
        private readonly rentacarContext _context;

        public OzellikEklesController(rentacarContext context)
        {
            _context = context;
        }

        // GET: OzellikEkles
        public async Task<IActionResult> Index()
        {
            var rentacarContext = _context.OzellikEkles.Include(o => o.Araba).Include(o => o.Ozellik);
            return View(await rentacarContext.ToListAsync());
        }

        // GET: OzellikEkles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ozellikEkle = await _context.OzellikEkles
                .Include(o => o.Araba)
                .Include(o => o.Ozellik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ozellikEkle == null)
            {
                return NotFound();
            }

            return View(ozellikEkle);
        }

        // GET: OzellikEkles/Create
        public IActionResult Create()
        {
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad");
            ViewData["OzellikId"] = new SelectList(_context.Ozelliks, "Id", "Id");
            return View();
        }

        // POST: OzellikEkles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArabaId,OzellikId,Tarih")] OzellikEkle ozellikEkle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ozellikEkle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", ozellikEkle.ArabaId);
            ViewData["OzellikId"] = new SelectList(_context.Ozelliks, "Id", "Id", ozellikEkle.OzellikId);
            return View(ozellikEkle);
        }

        // GET: OzellikEkles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ozellikEkle = await _context.OzellikEkles.FindAsync(id);
            if (ozellikEkle == null)
            {
                return NotFound();
            }
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", ozellikEkle.ArabaId);
            ViewData["OzellikId"] = new SelectList(_context.Ozelliks, "Id", "Id", ozellikEkle.OzellikId);
            return View(ozellikEkle);
        }

        // POST: OzellikEkles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArabaId,OzellikId,Tarih")] OzellikEkle ozellikEkle)
        {
            if (id != ozellikEkle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ozellikEkle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OzellikEkleExists(ozellikEkle.Id))
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
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", ozellikEkle.ArabaId);
            ViewData["OzellikId"] = new SelectList(_context.Ozelliks, "Id", "Id", ozellikEkle.OzellikId);
            return View(ozellikEkle);
        }

        // GET: OzellikEkles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ozellikEkle = await _context.OzellikEkles
                .Include(o => o.Araba)
                .Include(o => o.Ozellik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ozellikEkle == null)
            {
                return NotFound();
            }

            return View(ozellikEkle);
        }

        // POST: OzellikEkles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ozellikEkle = await _context.OzellikEkles.FindAsync(id);
            _context.OzellikEkles.Remove(ozellikEkle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OzellikEkleExists(int id)
        {
            return _context.OzellikEkles.Any(e => e.Id == id);
        }
    }
}
