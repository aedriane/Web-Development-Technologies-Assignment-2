using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class SessionTimesController : Controller
    {
        private readonly Assignment2Context _context;

        public SessionTimesController(Assignment2Context context)
        {
            _context = context;    
        }

        // GET: SessionTimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SessionTime.ToListAsync());
        }

        // GET: SessionTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionTime = await _context.SessionTime.SingleOrDefaultAsync(m => m.SessionTimeId == id);
            if (sessionTime == null)
            {
                return NotFound();
            }

            return View(sessionTime);
        }

        // GET: SessionTimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SessionTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessionTimeId,SessionTime1,SessionTime2")] SessionTime sessionTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessionTime);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sessionTime);
        }

        // GET: SessionTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionTime = await _context.SessionTime.SingleOrDefaultAsync(m => m.SessionTimeId == id);
            if (sessionTime == null)
            {
                return NotFound();
            }
            return View(sessionTime);
        }

        // POST: SessionTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionTimeId,SessionTime1,SessionTime2")] SessionTime sessionTime)
        {
            if (id != sessionTime.SessionTimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionTimeExists(sessionTime.SessionTimeId))
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
            return View(sessionTime);
        }

        // GET: SessionTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionTime = await _context.SessionTime.SingleOrDefaultAsync(m => m.SessionTimeId == id);
            if (sessionTime == null)
            {
                return NotFound();
            }

            return View(sessionTime);
        }

        // POST: SessionTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionTime = await _context.SessionTime.SingleOrDefaultAsync(m => m.SessionTimeId == id);
            _context.SessionTime.Remove(sessionTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SessionTimeExists(int id)
        {
            return _context.SessionTime.Any(e => e.SessionTimeId == id);
        }
    }
}
