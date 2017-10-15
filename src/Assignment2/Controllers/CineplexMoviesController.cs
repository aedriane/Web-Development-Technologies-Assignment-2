using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2.Models;
using System.Globalization;

namespace Assignment2.Controllers
{
    public class CineplexMoviesController : Controller
    {
        private readonly Assignment2Context _context;

        public CineplexMoviesController(Assignment2Context context)
        {
            _context = context;    
        }

        // GET: CineplexMovies
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            /*var assignment2Context = _context.CineplexMovie.Include(c => c.Cineplex).Include(c => c.Movie).Include(c => c.Session);
            return View(await assignment2Context.ToListAsync());*/
            ViewData["MovieSortParm"] = String.IsNullOrEmpty(sortOrder) ? "movie_desc" : "";
            ViewData["CinemaSortParm"] = sortOrder == "cin" ? "cinema_loc" : "cin";
            ViewData["CurrentFilter"] = searchString;
            
            var cinemovie = from m in _context.CineplexMovie.Include(c => c.Cineplex).Include(c => c.Movie).Include(c => c.Session)
                            select m;
            /*var currentCulture = CultureInfo.CurrentCulture;
            foreach (var m in cinemovie)
            {
                var date = m.Session.SessionTime1;
                var mdate = currentCulture.Calendar.GetWeekOfYear(
                            DateTime.Now,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);
                var now = currentCulture.Calendar.GetWeekOfYear(
                            DateTime.Now,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);

                cinemovie = cinemovie.Where(mdate==now);
            }


            IQueryable <CineplexMovie> movies;
            
            var weekNo = currentCulture.Calendar.GetWeekOfYear(
                            DateTime.Now,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);

            foreach (var y in cinemovie)
            {
                var weekNo2 = currentCulture.Calendar.GetWeekOfYear(
                                y.Session.SessionTime1,
                                currentCulture.DateTimeFormat.CalendarWeekRule,
                                currentCulture.DateTimeFormat.FirstDayOfWeek);
                var weekNo3 = currentCulture.Calendar.GetWeekOfYear(
                                y.Session.SessionTime2,
                                currentCulture.DateTimeFormat.CalendarWeekRule,
                                currentCulture.DateTimeFormat.FirstDayOfWeek);
                if (weekNo == weekNo2 && weekNo == weekNo3)
                {
                    cinemovie = cinemovie.Where(weekNo2);
                }

            }*/


            if (!String.IsNullOrEmpty(searchString))
            {
                cinemovie = cinemovie.Where(s => s.Movie.Title.Contains(searchString)
                                            || s.Cineplex.Location.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "cinema_loc":
                    cinemovie = cinemovie.OrderByDescending(s => s.Cineplex.Location);
                    break;
                case "cin":
                    cinemovie = cinemovie.OrderBy(s => s.Cineplex.Location);
                    break;
                case "movie_desc":
                    cinemovie = cinemovie.OrderByDescending(s => s.Movie.Title);
                    break;
                default:
                    cinemovie = cinemovie.OrderBy(s => s.Movie.Title);
                    break;
            }
            return View(await cinemovie.AsNoTracking().ToListAsync());
        }

        // GET: CineplexMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var cineplexMovie = await _context.CineplexMovie
                .Include(c => c.Cineplex)
                .Include(c => c.Movie)
                .Include(c => c.Session)
                .AsNoTracking()
                   .SingleOrDefaultAsync(m => m.CineplexId == id);
            /*var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexId == id);*/
            if (cineplexMovie == null)
            {
                return NotFound();
            }

            return View(cineplexMovie);
        }

        // GET: CineplexMovies/Create
        public IActionResult Create()
        {
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location");
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription");
            ViewData["SessionId"] = new SelectList(_context.SessionTime, "SessionTimeId", "SessionTimeId");
            return View();
        }

        // POST: CineplexMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CineplexId,MovieId,SessionId")] CineplexMovie cineplexMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cineplexMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            ViewData["SessionId"] = new SelectList(_context.SessionTime, "SessionTimeId", "SessionTimeId", cineplexMovie.SessionId);
            return View(cineplexMovie);
        }

        // GET: CineplexMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexId == id);
            if (cineplexMovie == null)
            {
                return NotFound();
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            ViewData["SessionId"] = new SelectList(_context.SessionTime, "SessionTimeId", "SessionTimeId", cineplexMovie.SessionId);
            return View(cineplexMovie);
        }

        // POST: CineplexMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CineplexId,MovieId,SessionId")] CineplexMovie cineplexMovie)
        {
            if (id != cineplexMovie.CineplexId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cineplexMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CineplexMovieExists(cineplexMovie.CineplexId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", cineplexMovie.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", cineplexMovie.MovieId);
            ViewData["SessionId"] = new SelectList(_context.SessionTime, "SessionTimeId", "SessionTimeId", cineplexMovie.SessionId);
            return View(cineplexMovie);
        }

        // GET: CineplexMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexId == id);
            if (cineplexMovie == null)
            {
                return NotFound();
            }

            return View(cineplexMovie);
        }

        // POST: CineplexMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cineplexMovie = await _context.CineplexMovie.SingleOrDefaultAsync(m => m.CineplexId == id);
            _context.CineplexMovie.Remove(cineplexMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CineplexMovieExists(int id)
        {
            return _context.CineplexMovie.Any(e => e.CineplexId == id);
        }
    }
}
