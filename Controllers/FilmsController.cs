#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API3.Data;
using API3.Models;
using RestSharp;
using System.Net;
using Nancy.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;

namespace API3
{
    [Authorize]
    public class FilmsController : Controller
    {
        private readonly API3Context _context;

        public FilmsController(API3Context context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index(string? movies, string? movie, [Bind("imdbID,Title,Year,Type,Poster,Plot,Runtime")] Search search, string? watch)
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
                    Search film = oJS.Deserialize<Search>(json);
                    ViewBag.Movie = film;
                    search = film;
                   
                }
            }
            


            return View(await _context.Film.ToListAsync());
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("imdbID,Title,Year,Type,Poster,Plot,Runtime")] Search film)
        {

            if (ModelState.IsValid)
            {
                
                Console.WriteLine(film);
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return RedirectToAction(nameof(Index));
        }
        // GET: Films/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.Include(c => c.Critiques)
                .FirstOrDefaultAsync(m => m.imdbID == id);


            
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        public IActionResult CreateCritique(string id)
        {
            ViewBag.imdbID = id;
            return View();
        }

        // POST: Critiques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCritique(string id, [Bind("imdbID,Content")] Critique critique)
        {

            var film = await _context.Film.Include(c => c.Critiques)
                .FirstOrDefaultAsync(m => m.imdbID == critique.imdbID);



            // if(film.imdbID == Critique.imdbID) { 
            critique.imdbID = id;
            if (ModelState.IsValid) {
                _context.Add(critique);
                await _context.SaveChangesAsync();
                return Redirect($"https://localhost:7142/Films/Details/{film.imdbID}");
            }
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            return RedirectToAction("error");
        }

        // GET: Critiques/Edit/5
        public async Task<IActionResult> CritiqueEdit(int id)
        {
            if (id == null || _context.Critique == null)
            {
                return NotFound();
            }

            var critique = await _context.Critique
                .FirstOrDefaultAsync(i => i.Id == id);
            /*var critique = await _context.Critique.FindAsync(id);*/

            /*var film = await _context.Film.Include(c => c.Critiques)
                .FirstOrDefaultAsync(m => m.imdbID == critique.imdbID);*/

            if (critique == null)
            {
                return NotFound();
            }
            return View(critique);
            /* return Redirect($"https://localhost:7142/Films/Details/{film.imdbID}");*/
        }

        // POST: Critiques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("CritiqueEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CritiqueEdit(int id, Critique critique)
        {
            if (id != critique.Id)
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
                    if (!CritiqueExists(critique.Id))
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

        private bool CritiqueExists(int Id)
        {
            throw new NotImplementedException();
        }

        // GET: Films/Create
        /*public IActionResult Create()
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
        }*/


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



        // GET: Films/Delete/5
        public async Task<IActionResult> CritiqueDelete(int id)
        {
            if (id == null || _context.Critique == null)
            {
                return NotFound();
            }

            var critique = await _context.Critique
                .FirstOrDefaultAsync(i => i.Id == id);
            if (critique == null)
            {
                return NotFound();
            }

            return View(critique);
        }

        // POST: Critiques/Delete/5
        [HttpPost, ActionName("CritiqueDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CritiqueDeleteConfirmed(int id)
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

        private bool FilmExists(string id)
        {
            return _context.Film.Any(e => e.imdbID == id);
        }
    }
}
