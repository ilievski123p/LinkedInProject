using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using linkedinproject.Models;

namespace linkedinproject.Controllers
{
    public class InterestController : Controller
    {
        private readonly LinkedInProjectDataContext _context;

        public InterestController(LinkedInProjectDataContext context)
        {
            _context = context;
        }

        // GET: Interest
        public async Task<IActionResult> Index()
        {
            var linkedInProjectDataContext = _context.Interest.Include(i => i.Employee).Include(i => i.Employer);
            return View(await linkedInProjectDataContext.ToListAsync());
        }

        // GET: Interest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interest = await _context.Interest
                .Include(i => i.Employee)
                .Include(i => i.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interest == null)
            {
                return NotFound();
            }

            return View(interest);
        }

        // GET: Interest/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName");
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name");
            return View();
        }

        // POST: Interest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromRoute]int id, [Bind("Id,EmployeeId,EmployerId")] Interest interest)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
               
                _context.Add(interest);
                await _context.SaveChangesAsync();
                return RedirectToAction("EmployeeHomePage","Employee", new {id = interest.EmployeeId });
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", interest.EmployeeId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name", interest.EmployerId);
            return View(interest);
        }

        // GET: Interest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interest = await _context.Interest.FindAsync(id);
            if (interest == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", interest.EmployeeId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name", interest.EmployerId);
            return View(interest);
        }

        // POST: Interest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,EmployerId")] Interest interest)
        {
            if (id != interest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterestExists(interest.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", interest.EmployeeId);
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name", interest.EmployerId);
            return View(interest);
        }

        // GET: Interest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interest = await _context.Interest
                .Include(i => i.Employee)
                .Include(i => i.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interest == null)
            {
                return NotFound();
            }

            return View(interest);
        }

        // POST: Interest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interest = await _context.Interest.FindAsync(id);
            _context.Interest.Remove(interest);
            await _context.SaveChangesAsync();
            return RedirectToAction("EmployeeHomePage","Employee", new {id = interest.EmployeeId });
        }

        private bool InterestExists(int id)
        {
            return _context.Interest.Any(e => e.Id == id);
        }










        //Make interest 


        public IActionResult MakeInterest([FromRoute]int? id)
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(e => e.Id == id), "Id", "FullName");
            ViewData["EmployerId"] = new SelectList(_context.Employer, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeInterest([FromRoute]int id, [Bind("Id,EmployerId")] Interest interesty)
        {
            
                var employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
            Interest interest = new Interest
            {
                EmployerId = interesty.EmployerId,
                EmployeeId = employee.Id,

            };
                _context.Add(interest);
                await _context.SaveChangesAsync();
                return RedirectToAction("EmployeeHomePage", "Employee", new { id = interest.EmployeeId });
            
        }






    }
}
