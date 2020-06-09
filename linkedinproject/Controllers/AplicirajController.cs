using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using linkedinproject.Data;
using linkedinproject.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace linkedinproject.Controllers
{
    public class AplicirajController : Controller
    {
        private readonly LinkedInProjectDataContext _context;

        public AplicirajController(LinkedInProjectDataContext context)
        {
            _context = context;
        }

        // GET: Apliciraj
        public async Task<IActionResult> Index()
        {
            var linkedInProjectDataContext = _context.Apliciraj.Include(a => a.Employee).Include(a => a.Oglas);
            return View(await linkedInProjectDataContext.ToListAsync());
        }

        // GET: Apliciraj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apliciraj = await _context.Apliciraj
                .Include(a => a.Employee)
                .Include(a => a.Oglas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apliciraj == null)
            {
                return NotFound();
            }

            return View(apliciraj);
        }

        // GET: Apliciraj/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
            ViewData["OglasId"] = new SelectList(_context.Oglas, "Id", "JobTitle");
            return View();
        }

        // POST: Apliciraj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,OglasId")] Apliciraj apliciraj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apliciraj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var ime = apliciraj.Oglas.Employer.Name;
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", apliciraj.EmployeeId);
            ViewData["OglasId"] = new SelectList(_context.Oglas, "Id", "JobTitle", apliciraj.OglasId);
            return View(apliciraj);
        }

        // GET: Apliciraj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apliciraj = await _context.Apliciraj.FindAsync(id);
            if (apliciraj == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", apliciraj.EmployeeId);
            ViewData["OglasId"] = new SelectList(_context.Oglas, "Id", "JobTitle", apliciraj.OglasId);
            return View(apliciraj);
        }

        // POST: Apliciraj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,OglasId")] Apliciraj apliciraj)
        {
            if (id != apliciraj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apliciraj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AplicirajExists(apliciraj.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", apliciraj.EmployeeId);
            ViewData["OglasId"] = new SelectList(_context.Oglas, "Id", "JobTitle", apliciraj.OglasId);
            return View(apliciraj);
        }

        // GET: Apliciraj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apliciraj = await _context.Apliciraj
                .Include(a => a.Employee)
                .Include(a => a.Oglas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apliciraj == null)
            {
                return NotFound();
            }

            return View(apliciraj);
        }

        // POST: Apliciraj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apliciraj = await _context.Apliciraj.FindAsync(id);
            _context.Apliciraj.Remove(apliciraj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AplicirajExists(int id)
        {
            return _context.Apliciraj.Any(e => e.Id == id);
        }




        //Za apliciranje na oglasi
       
        public IActionResult EmployeeApply(int id3,int id2)
        {
            
            ViewData["OglasId"] = new SelectList(_context.Oglas.Where( o => o.Id == id3), "Id", "JobTitle");
            ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(e => e.Id == id2), "Id","FullName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeApply([Bind("Id,EmployeeId,OglasId")] Apliciraj aplicirajs)
        {
            Apliciraj apliciraj = new Apliciraj
            {
                Id = aplicirajs.Id,
                EmployeeId = aplicirajs.EmployeeId,
                OglasId = aplicirajs.OglasId,
            };
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", aplicirajs.EmployeeId);
            ViewData["OglasId"] = new SelectList(_context.Oglas, "Id", "JobTitle", aplicirajs.OglasId);
            _context.Add(apliciraj);
                await _context.SaveChangesAsync();
            return RedirectToPage("");
        }
    }
}
