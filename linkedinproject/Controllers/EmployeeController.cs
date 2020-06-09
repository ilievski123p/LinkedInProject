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

namespace linkedinproject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly LinkedInProjectDataContext _context;
        private readonly LinkedInProjectDataContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeController(LinkedInProjectDataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            dbContext = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                string uniqueFileNameCover = UploadedFileCover(model);
                string FileNameCV = UploadedCV(model);
                string uniqueFileCoverLetter = UploadedFileCoverLetter(model);
                Employee employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CurrentPosition = model.CurrentPosition,
                    CV = FileNameCV,
                    Age = model.Age,
                    GitHubLink = model.GitHubLink,
                    Mail = model.Mail,
                    Password = model.Password,
                    ProfilePicutre = uniqueFileName,
                    Description = model.Description,
                    Location = model.Location,
                    WantedPosition = model.WantedPosition,
                    CoverPhoto = uniqueFileNameCover,
                    CoverLetter = uniqueFileCoverLetter,
                    PhoneNumber = model.PhoneNumber,
                    Skills = model.Skills,
                };
                dbContext.Add(employee);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Age,ProfilePicutre,CoverPhoto,CV,CoverLetter,GitHubLink,CurrentPosition,WantedPosition,Description,Location,PhoneNumber,Mail,Password,Skills")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }





















        //Za CV
        public string UploadedCV(EmployeeViewModel model)
        {
            string uniqueFileName = null;

            if (model.CVFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "files");
                uniqueFileName = /* Guid.NewGuid().ToString() + "_" +*/ Path.GetFileName(model.CVFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CVFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        //Za Motivaciono Pismo
        public string UploadedFileCoverLetter(EmployeeViewModel model)
        {
            string uniqueFileName = null;
            if (model.CoverLetterFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "files");
                uniqueFileName = /*Guid.NewGuid().ToString() + "_" +*/ Path.GetFileName(model.CoverLetterFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverLetterFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }




        //Za porfilna
        public string UploadedFile(EmployeeViewModel model)
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
        
        //Za Cover photo
        public string UploadedFileCover(EmployeeViewModel model)
        {
            string uniqueFileName = null;
            if (model.CoverImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.CoverImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
       





        //Profil na  Employee
        public async Task<IActionResult> EmployeeHomePage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.Include(e => e.Interests).ThenInclude(e =>e.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        //Profil na  Employee koga otvore Employer
        public async Task<IActionResult> EmployerSeeProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.Include(e => e.Interests).ThenInclude(e => e.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }




        //Edit za dodavanje Skills
        public async Task<IActionResult> AddSkills(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSkills(int id, [Bind("Id,FirstName,LastName,Age,ProfilePicutre,CoverPhoto,CV,CoverLetter,GitHubLink,CurrentPosition,WantedPosition,Description,Location,PhoneNumber,Mail,Password,Skills")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }

        //Interests View
        public async Task<IActionResult> Interests(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.Include(e => e.Oglasi).
                Include(e => e.Interests).ThenInclude(e => e.Employer).ThenInclude(e => e.Oglasi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }








        //Edit za description
        public async Task<IActionResult> EditDescription(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDescription(int id, [Bind("Id,FirstName,LastName,Age,ProfilePicutre,CoverPhoto,CV,CoverLetter,GitHubLink,CurrentPosition,WantedPosition,Description,Location,PhoneNumber,Mail,Password,Skills")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            return View(employee);
        }




        //Edit za contact
        public async Task<IActionResult> EditContact(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeViewModelContact vm = new EmployeeViewModelContact
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                GitHubLink = employee.GitHubLink,
                CurrentPosition = employee.CurrentPosition,
                WantedPosition = employee.WantedPosition,
                Description = employee.Description,
                Location = employee.Location,
                PhoneNumber = employee.PhoneNumber,
                Mail = employee.Mail,
                Password = employee.Password,
                Skills = employee.Skills,
                ProfileImage = employee.ProfilePicutre,
                CoverImage = employee.CoverPhoto,
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(int id, EmployeeViewModelContact employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string FileNameCV = UploadedCVContact(employee);
                    string uniqueFileCoverLetter = UploadedFileCoverLetterContact(employee);
                    Employee vm = new Employee
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Age = employee.Age,
                        GitHubLink = employee.GitHubLink,
                        CurrentPosition = employee.CurrentPosition,
                        WantedPosition = employee.WantedPosition,
                        Description = employee.Description,
                        Location = employee.Location,
                        PhoneNumber = employee.PhoneNumber,
                        Mail = employee.Mail,
                        Password = employee.Password,
                        Skills = employee.Skills,
                        ProfilePicutre = employee.ProfileImage,
                        CoverPhoto = employee.CoverImage,
                        CV = FileNameCV,
                        CoverLetter = uniqueFileCoverLetter,
                        

                    };
                    _context.Update(vm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("");

            }
            return View(employee);
        }
        //Za CV
        public string UploadedCVContact(EmployeeViewModelContact model)
        {
            string uniqueFileName = null;

            if (model.CVFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "files");
                uniqueFileName = /* Guid.NewGuid().ToString() + "_" +*/ Path.GetFileName(model.CVFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CVFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        //Za Motivaciono Pismo
        public string UploadedFileCoverLetterContact(EmployeeViewModelContact model)
        {
            string uniqueFileName = null;
            if (model.CoverLetterFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "files");
                uniqueFileName = /*Guid.NewGuid().ToString() + "_" +*/ Path.GetFileName(model.CoverLetterFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverLetterFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }












        //Edit za contact
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeViewModelProfile vm = new EmployeeViewModelProfile
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                GitHubLink = employee.GitHubLink,
                CurrentPosition = employee.CurrentPosition,
                WantedPosition = employee.WantedPosition,
                Description = employee.Description,
                Location = employee.Location,
                PhoneNumber = employee.PhoneNumber,
                Mail = employee.Mail,
                Password = employee.Password,
                Skills = employee.Skills,
                CVFile = employee.CV,
                CoverLetterFile = employee.CoverLetter,
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id, EmployeeViewModelProfile employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string FileNameProfile = UploadedFileProfile(employee);
                    string uniqueFileCoverProfile = UploadedFileCoverProfile(employee);
                    Employee vm = new Employee
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Age = employee.Age,
                        GitHubLink = employee.GitHubLink,
                        CurrentPosition = employee.CurrentPosition,
                        WantedPosition = employee.WantedPosition,
                        Description = employee.Description,
                        Location = employee.Location,
                        PhoneNumber = employee.PhoneNumber,
                        Mail = employee.Mail,
                        Password = employee.Password,
                        Skills = employee.Skills,
                        ProfilePicutre = FileNameProfile,
                        CoverPhoto = uniqueFileCoverProfile,
                        CV = employee.CVFile,
                        CoverLetter = employee.CoverLetterFile,


                    };
                    _context.Update(vm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("");

            }
            return View(employee);
        }
        //Za porfilna
        public string UploadedFileProfile(EmployeeViewModelProfile model)
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

        //Za Cover photo
        public string UploadedFileCoverProfile(EmployeeViewModelProfile model)
        {
            string uniqueFileName = null;
            if (model.CoverImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.CoverImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }



        //Za gledanje na site aplikacii

        public async Task<IActionResult> Applications(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.Include(e => e.Aplikacii).Include(e => e.Oglasi).ThenInclude( e => e.Employer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

    }
}
