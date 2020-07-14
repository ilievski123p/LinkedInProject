using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using linkedinproject.Models;
using linkedinproject.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace linkedinproject.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployerController : Controller
    {
        private readonly LinkedInProjectDataContext _context;
        private readonly LinkedInProjectDataContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> userManager;


        public EmployerController(LinkedInProjectDataContext context, IWebHostEnvironment hostEnvironment, UserManager<AppUser> userMgr)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            dbContext = context;
            userManager = userMgr;
        }
        // GET: Employer
    /*    public async Task<IActionResult> Index()
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
    */
        // GET: Employer/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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
        /*
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
        */
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
                AppUser curruser = await userManager.GetUserAsync(User);
                if (curruser.EmployerId != null)
                    return RedirectToAction(nameof(Employer), new { id = curruser.EmployerId });
                else
                    return NotFound();
            }

            var employer = await _context.Employer.Include(e => e.Oglasi).Include(e => e.Interests).ThenInclude(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }
            AppUser user = await userManager.GetUserAsync(User);
            if (id != user.EmployerId)
            {
                return RedirectToAction("AccessDenied", "Account", null);
            }
            return View(employer);
        }

        public async Task<IActionResult> EmployerProfile(string? name)
        {
            string[] ime = name.Split('@');

            var employer = await _context.Employer.Where(e => e.Name.Contains(ime[0])).Include(e => e.Oglasi).Include(e => e.Interests).ThenInclude(e => e.Employee)
                .FirstOrDefaultAsync();
            if (employer == null)
            {
                return NotFound();
            }
            AppUser user = await userManager.GetUserAsync(User);
            if (employer.Id != user.EmployerId)
            {
                return RedirectToAction("AccessDenied", "Account", null);
            }
            return View(employer);
        }




        //Profil na  Employer koga otvore Employee
        [AllowAnonymous]
        public async Task<IActionResult> EmployeeSeeProfile(int? id)
        {
            if (id == null)
            {
                AppUser curruser = await userManager.GetUserAsync(User);
                if (curruser.EmployerId != null)
                    return RedirectToAction(nameof(Employer), new { id = curruser.EmployerId });
                else
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
                AppUser curruser = await userManager.GetUserAsync(User);
                if (curruser.EmployerId != null)
                    return RedirectToAction(nameof(Employer), new { id = curruser.EmployerId });
                else
                    return NotFound();
            }
            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            AppUser user = await userManager.GetUserAsync(User);
            if (employer.Id != user.EmployerId)
            {
                return RedirectToAction("AccessDenied", "Account", null);
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
            AppUser user = await userManager.GetUserAsync(User);
            if (employer.Id != user.EmployerId)
            {
                return RedirectToAction("AccessDenied", "Account", null);
            }
            return View(employer);
        }



        //Edit Profile


        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                AppUser curruser = await userManager.GetUserAsync(User);
                if (curruser.EmployerId != null)
                    return RedirectToAction(nameof(Employer), new { id = curruser.EmployerId });
                else
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
            AppUser user = await userManager.GetUserAsync(User);
            if (vm.Id != user.EmployerId)
            {
                return RedirectToAction("AccessDenied", "Account", null);
            }
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
            AppUser user = await userManager.GetUserAsync(User);
            if (model.Id != user.EmployerId)
            {
                return RedirectToAction("AccessDenied", "Account", null);
            }
            return RedirectToPage("");

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








        //Profil na  Employee koga otvore Employer
        public async Task<IActionResult> EmployerSeeProfile(int? id)
        {
            if (id == null)
            {
                AppUser curruser = await userManager.GetUserAsync(User);
                if (curruser.EmployerId != null)
                    return RedirectToAction(nameof(Employee), new { id = curruser.EmployerId });
                else
                    return NotFound();
            }

            var employee = await _context.Employee.Include(e => e.Interests).ThenInclude(e => e.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            AppUser user = await userManager.GetUserAsync(User);
           
            return View(employee);
        }
    }
}
