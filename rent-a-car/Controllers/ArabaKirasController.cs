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
    public class ArabaKirasController : Controller
    {
        private readonly rentacarContext _context;

        public ArabaKirasController(rentacarContext context)
        {
            _context = context;
        }

        // GET: ArabaKiras
        public async Task<IActionResult> Index()
        {
            var rentacarContext = _context.ArabaKiras.Include(a => a.Araba).Include(a => a.Kiraci).Include(a => a.Personel);
            return View(await rentacarContext.ToListAsync());
        }

        // GET: ArabaKiras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arabaKira = await _context.ArabaKiras
                .Include(a => a.Araba)
                .Include(a => a.Kiraci)
                .Include(a => a.Personel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arabaKira == null)
            {
                return NotFound();
            }

            return View(arabaKira);
        }

        // GET: ArabaKiras/Create
        public IActionResult Create()
        {
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad");
            ViewData["KiraciId"] = new SelectList(_context.Kiracis, "Id", "Ad");
            ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id");
            return View();
        }

        // POST: ArabaKiras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonelId,ArabaId,KiraciId,KiraFiyati,Sure")] ArabaKira arabaKira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arabaKira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", arabaKira.ArabaId);
            ViewData["KiraciId"] = new SelectList(_context.Kiracis, "Id", "Ad", arabaKira.KiraciId);
            ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", arabaKira.PersonelId);
            return View(arabaKira);
        }

        // GET: ArabaKiras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arabaKira = await _context.ArabaKiras.FindAsync(id);
            if (arabaKira == null)
            {
                return NotFound();
            }
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", arabaKira.ArabaId);
            ViewData["KiraciId"] = new SelectList(_context.Kiracis, "Id", "Ad", arabaKira.KiraciId);
            ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", arabaKira.PersonelId);
            return View(arabaKira);
        }

        // POST: ArabaKiras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonelId,ArabaId,KiraciId,KiraFiyati,Sure")] ArabaKira arabaKira)
        {
            if (id != arabaKira.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arabaKira);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArabaKiraExists(arabaKira.Id))
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
            ViewData["ArabaId"] = new SelectList(_context.Arabas, "Id", "Ad", arabaKira.ArabaId);
            ViewData["KiraciId"] = new SelectList(_context.Kiracis, "Id", "Ad", arabaKira.KiraciId);
            ViewData["PersonelId"] = new SelectList(_context.Personels, "Id", "Id", arabaKira.PersonelId);
            return View(arabaKira);
        }

        // GET: ArabaKiras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arabaKira = await _context.ArabaKiras
                .Include(a => a.Araba)
                .Include(a => a.Kiraci)
                .Include(a => a.Personel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arabaKira == null)
            {
                return NotFound();
            }

            return View(arabaKira);
        }

        // POST: ArabaKiras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arabaKira = await _context.ArabaKiras.FindAsync(id);
            _context.ArabaKiras.Remove(arabaKira);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArabaKiraExists(int id)
        {
            return _context.ArabaKiras.Any(e => e.Id == id);
        }
    }
}
