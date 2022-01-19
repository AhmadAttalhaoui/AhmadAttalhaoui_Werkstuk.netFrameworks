#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API3.Data;
using API3.Models;

namespace API3
{
    public class ApiFilmsController : Controller
    {
        private readonly API3Context _context;

        public ApiFilmsController(API3Context context)
        {
            _context = context;
        }

        // GET: ApiFilms
        public async Task<IActionResult> Index()
        {
            var aPI3Context = _context.ApiFilm.Include(a => a.User);
            return View(await aPI3Context.ToListAsync());
        }

        // GET: ApiFilms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiFilm = await _context.ApiFilm
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApiFilmId == id);
            if (apiFilm == null)
            {
                return NotFound();
            }

            return View(apiFilm);
        }

        // GET: ApiFilms/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Achternaam");
            return View();
        }

        // POST: ApiFilms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApiFilmId,imdbID,UserId")] ApiFilm apiFilm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apiFilm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Achternaam", apiFilm.UserId);
            return View(apiFilm);
        }

        // GET: ApiFilms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiFilm = await _context.ApiFilm.FindAsync(id);
            if (apiFilm == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Achternaam", apiFilm.UserId);
            return View(apiFilm);
        }

        // POST: ApiFilms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApiFilmId,imdbID,UserId")] ApiFilm apiFilm)
        {
            if (id != apiFilm.ApiFilmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apiFilm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApiFilmExists(apiFilm.ApiFilmId))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Achternaam", apiFilm.UserId);
            return View(apiFilm);
        }

        // GET: ApiFilms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiFilm = await _context.ApiFilm
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApiFilmId == id);
            if (apiFilm == null)
            {
                return NotFound();
            }

            return View(apiFilm);
        }

        // POST: ApiFilms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apiFilm = await _context.ApiFilm.FindAsync(id);
            _context.ApiFilm.Remove(apiFilm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApiFilmExists(int id)
        {
            return _context.ApiFilm.Any(e => e.ApiFilmId == id);
        }
    }
}
