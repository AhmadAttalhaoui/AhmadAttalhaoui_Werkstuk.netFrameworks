using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API3.Data;
using API3.Models;
using Microsoft.AspNetCore.Localization;
using System.Net;
using Nancy.Json;

namespace API3.Controllers
{
    public class CritiquesController : Controller
    {
        private readonly API3Context _context;
        

        public CritiquesController(API3Context context)
        {
            _context = context;
        }

        // GET: Critiques
        public async Task<IActionResult> Index(string id, string? movie, Search search)
        {
            var Critique = await _context.Critique
                .FirstOrDefaultAsync ( m => m.imdbID == id );

            var Film = await _context.Film
                .FirstOrDefaultAsync(m => m.imdbID == id);

/*
            if (Film.imdbID == Critique.imdbID)
            {
                return _context.Critique != null ?
                    View(await _context.Critique.ToListAsync()) :
                          Problem("Entity set 'API3Context.Critique'  is null.");
            }*/

              return _context.Critique != null ? 
                          View(await _context.Critique.ToListAsync()) :
                          Problem("Entity set 'API3Context.Critique'  is null.");
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
        // GET: Critiques/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Critique == null)
            {
                return NotFound();
            }

            var critique = await _context.Critique
                .FirstOrDefaultAsync(m => m.imdbID == id);
            if (critique == null)
            {
                return NotFound();
            }

            return View(critique);
        }

        // GET: Critiques/Create
       /* public IActionResult Create()
        {
            return View();
        }

        // POST: Critiques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImdbID,Content")] Critique critique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(critique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(critique);
        }*/

        // GET: Critiques/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Critique == null)
            {
                return NotFound();
            }

            var critique = await _context.Critique.FindAsync(id);
            if (critique == null)
            {
                return NotFound();
            }
            return View(critique);
        }

        // POST: Critiques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ImdbID,Content")] Critique critique)
        {
            if (id != critique.imdbID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(critique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CritiqueExists(critique.imdbID))
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
            return View(critique);
        }

        // GET: Critiques/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Critique == null)
            {
                return NotFound();
            }

            var critique = await _context.Critique
                .FirstOrDefaultAsync(m => m.imdbID == id);
            if (critique == null)
            {
                return NotFound();
            }

            return View(critique);
        }

        // POST: Critiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Critique == null)
            {
                return Problem("Entity set 'API3Context.Critique'  is null.");
            }
            var critique = await _context.Critique.FindAsync(id);
            if (critique != null)
            {
                _context.Critique.Remove(critique);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CritiqueExists(string id)
        {
          return (_context.Critique?.Any(e => e.imdbID == id)).GetValueOrDefault();
        }
    }
}
