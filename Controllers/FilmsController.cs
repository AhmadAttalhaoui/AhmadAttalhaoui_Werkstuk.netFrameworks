#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API3.Data;
using API3.Models;
using RestSharp;
using System.Net;
using Nancy.Json;

namespace API3
{
    public class FilmsController : Controller
    {
        private readonly API3Context _context;

        public FilmsController(API3Context context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index(string? movies, string? movie)
        {
            if (movies != null)
            {
                string url = "http://www.omdbapi.com/?i=tt3896198&apikey=738dd7e3&s=" + movies;
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString(url);
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    Root obj = oJS.Deserialize<Root>(json);
                    ViewBag.Movies = obj.Search;
                }
                 
            }else if (movie != null)
            {
                string url = "http://www.omdbapi.com/?i=tt3896198&apikey=738dd7e3&t=" + movie;
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString(url);
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    Root obj = oJS.Deserialize<Root>(json);
                    ViewBag.Movies = obj.Search;
                }
            }

            
            return View(await _context.Film.ToListAsync());
        }

       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> check(string button, string Search, string div, string movie)
        {

            //TODO: transform the response here to suit your needs

            
            string url = "http://www.omdbapi.com/?i=tt3896198&apikey=738dd7e3&s=" + movie;
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(url);
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                Root obj = oJS.Deserialize<Root>(json);

                if (obj.Response == "True")
                {
                    TempData["buttonoval"] = "kaka";

                }
                    
            return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> searchResult()
        {
            string[] Countries = { "Russia", "France", "Austria" };
            ViewBag.Countries = Countries;
            return RedirectToAction("Index");
        }*/

        // GET: Films/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.imdbID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("imdbID,Title,Year,Type,Poster,Plot,Runtime")] Search film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("imdbID,Title,Year,Type,Poster,Plot,Runtime")] Search film)
        {
            if (id != film.imdbID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.imdbID))
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
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.imdbID == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(string id)
        {
            return _context.Film.Any(e => e.imdbID == id);
        }
    }
}
