using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using linkedinproject.Models;
using Microsoft.AspNetCore.Authorization;

namespace linkedinproject.Controllers
{
    [Authorize(Roles ="Employer, Admin")]
    public class OglasController : Controller
    {
        private readonly LinkedInProjectDataContext _context;

        public OglasController(LinkedInProjectDataContext context)
        {
            _context = context;
        }

        // GET: Oglas
        public async Task<IActionResult> Index()
        {
            var oglas = _context.Oglas.Include(o => o.Employee).Include(o => o.Employer);
            return View(await oglas.ToListAsync());
        }
        public async Task<IActionResult> EmployerJobs(string? name)
        {
            
                string[] employername = name.Split('@');
                var oglasi = _context.Oglas.Where(o => o.Employer.Name.Contains(employername[0]));
                return View(await oglasi.ToListAsync());
            
               
            
        }

        // GET: Oglas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas
                .Include(o => o.Employee)
                .Include(o => o.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        // GET: Oglas/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name");
            return View();
        }

        // POST: Oglas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobTitle,Description,EmployeeId,EmployerId")] Oglas oglas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oglas);
                await _context.SaveChangesAsync();
                return RedirectToAction("EmployerHomePage","Employer", new {id = oglas.EmployerId });
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", oglas.EmployeeId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name", oglas.EmployerId);
            return View(oglas);
        }

        // GET: Oglas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas.FindAsync(id);
            if (oglas == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", oglas.EmployeeId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name", oglas.EmployerId);
            return View(oglas);
        }

        // POST: Oglas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTitle,Description,EmployeeId,EmployerId")] Oglas oglas)
        {
            if (id != oglas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oglas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OglasExists(oglas.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", oglas.EmployeeId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name", oglas.EmployerId);
            return View(oglas);
        }

        // GET: Oglas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas
                .Include(o => o.Employee)
                .Include(o => o.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        // POST: Oglas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oglas = await _context.Oglas.FindAsync(id);
            _context.Oglas.Remove(oglas);
            await _context.SaveChangesAsync();
            return RedirectToAction("EmployerHomePage", "Employer", new { id = oglas.EmployerId });
        }

        private bool OglasExists(int id)
        {
            return _context.Oglas.Any(e => e.Id == id);
        }




        //Firma da kreira oglas

        public IActionResult EmployerJob()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name");
            return View();
        }

        // POST: Oglas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployerJob([Bind("Id,JobTitle,Description,EmployeeId,EmployerId")] Oglas oglas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oglas);
                await _context.SaveChangesAsync();
                return RedirectToPage("");
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", oglas.EmployeeId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name", oglas.EmployerId);
            return View(oglas);
        }



        //Make oglas



        public IActionResult MakeOglas([FromRoute]int? id)
        {
            ViewData["EmployerId"] = new SelectList(_context.Employer.Where( e => e.Id == id), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeOglas([FromRoute]int id, [Bind("Id,JobTitle,Description,EmployeeId")] Oglas oglass)
        {
            var employer = await _context.Employer.FirstOrDefaultAsync(e => e.Id == id);
            Oglas oglas = new Oglas
            {
                EmployerId = employer.Id,
                JobTitle= oglass.JobTitle,
                Description = oglass.Description,
                EmployeeId = oglass.EmployeeId,
            };
                _context.Add(oglas);
                await _context.SaveChangesAsync();
                return RedirectToAction("EmployerHomePage", "Employer", new { id = oglas.EmployerId });
           
        }








        //Za gledanje na aplikanti

        public async Task<IActionResult> SeeApplicants(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas
                .Include(o => o.Employee).Include(o => o.Aplikacii).ThenInclude( e => e.Employee)
                .Include(o => o.Employer)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }



    }
}
