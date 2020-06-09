using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using linkedinproject.Data;
using linkedinproject.Models;
using linkedinproject.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;

namespace linkedinproject.Controllers
{
    public class EmployerController : Controller
    {
        private readonly LinkedInProjectDataContext _context;
        private readonly LinkedInProjectDataContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployerController(LinkedInProjectDataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Employer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employer.ToListAsync());
        }

        // GET: Employer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // GET: Employer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployerViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                    Employer employer = new Employer
                    {
                        Name=model.Name,
                        ProfilePicture = uniqueFileName,
                        Description=model.Description,
                        CEOName=model.CEOName,
                        Location=model.Location,
                        PhoneNumber=model.PhoneNumber,
                        Mail=model.Mail,
                        Password=model.Password,
                       

                    };
                _context.Add(employer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Employer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            return View(employer);
        }

        // POST: Employer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProfilePicture,Description,CEOName,Location,PhoneNumber,Mail,Password")] Employer employer)
        {
            if (id != employer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployerExists(employer.Id))
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
            return View(employer);
        }

        // GET: Employer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // POST: Employer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employer = await _context.Employer.FindAsync(id);
            _context.Employer.Remove(employer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployerExists(int id)
        {
            return _context.Employer.Any(e => e.Id == id);
        }


        //Za porfilna
        public string UploadedFile(EmployerViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfileImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        //Profil na  Employer
        public async Task<IActionResult> EmployerHomePage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer.Include(e => e.Oglasi).Include(e => e.Interests).ThenInclude(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        //Profil na  Employer koga otvore Employee
        public async Task<IActionResult> EmployeeSeeProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer.Include(e => e.Oglasi).Include(e => e.Interests).ThenInclude(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }







        //Edit Contact
        public async Task<IActionResult> EditContact(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            return View(employer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(int id, [Bind("Id,Name,ProfilePicture,Description,CEOName,Location,PhoneNumber,Mail,Password")] Employer employer)
        {
            if (id != employer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployerExists(employer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToPage("");
            }
            return View(employer);
        }



        //Edit Profile


        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Employer.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            EmployerViewModel vm = new EmployerViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CEOName = model.CEOName,
                Location = model.Location,
                PhoneNumber = model.PhoneNumber,
                Mail = model.Mail,
                Password = model.Password,
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id,EmployerViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    string uniqueFileName = UploadedFileProfile(model);
                    Employer employer = new Employer
                    {
                        Id = model.Id,
                        Name = model.Name,
                        ProfilePicture = uniqueFileName,
                        Description = model.Description,
                        CEOName = model.CEOName,
                        Location = model.Location,
                        PhoneNumber = model.PhoneNumber,
                        Mail = model.Mail,
                        Password = model.Password,


                    };
                    _context.Update(employer);
                    await _context.SaveChangesAsync();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployerExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("");

        }
        public string UploadedFileProfile(EmployerViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfileImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


    }
}
