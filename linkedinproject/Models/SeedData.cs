using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace linkedinproject.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("Employee");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Employee"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("Employer");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Employer"));
            }

            AppUser user = await UserManager.FindByEmailAsync("admin@findjobs.com");
            if (user == null)
            {
                var User = new AppUser();
                User.Email = "admin@findjobs.com";
                User.UserName = "admin@findjobs.com";
                User.Role = "Admin";
                string userPWD = "Admin123.";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
               
                /*var korisnik = new AppUser();
                korisnik.Email = "employee@findjobs.com";
                korisnik.UserName = "employee@findjobs.com";
                User.Role = "Employee";
                string korisnikPWD = "employee123";
                IdentityResult checkKor = await UserManager.CreateAsync(korisnik, korisnikPWD);
                */
                //Add default User to Role Admin      
                if (chkUser.Succeeded)
                {
                    var result1 = await UserManager.AddToRoleAsync(User, "Admin");
                }
               
            }
        }



        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LinkedInProjectDataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LinkedInProjectDataContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                // Look for any movies.
                if (context.Employee.Any() || context.Employer.Any())
                {
                    return;   // DB has been seeded
                }

                context.Employer.AddRange(
                    new Employer {/*Id = 1*/Name = "Google", CEOName = "Sundar Pichai", Description = "We are looking for new Googlers to join us",
                        Location = "Silicon Valley",
                        PhoneNumber = "123456789",
                      
                    },
                    new Employer
                    {/*Id = 2*/
                        Name = "Facebook",
                        CEOName = "Mark Zuckerberg",
                        Description = "We are looking for new Facebookers to join us",
                        Location = "Silicon Valley",
                        PhoneNumber = "123456789"
                    },
                    new Employer
                    {/*Id = 3*/
                        Name = "Amazon",
                        CEOName = "Jeff Bezos",
                        Description = "We are looking for new Amazons to join us",
                        Location = "Silicon Valley",
                        PhoneNumber = "123456789"
                    }
                );
                context.SaveChanges();
                context.Employee.AddRange(
                    new Employee {/*Id = 3*/ FirstName="Petar", LastName="Ilievski",Age= 21, CurrentPosition="Student", WantedPosition="Developer"},
                    new Employee {/*Id = 2*/ FirstName = "Filip", LastName = "Ilievski", Age = 20, CurrentPosition = "Student", WantedPosition = "Developer" },
                    new Employee {/*Id = 1*/ FirstName = "Jim", LastName = "Morrison", Age = 25, CurrentPosition = "Junior Developer", WantedPosition = "Senior Developer" }

                    );
                context.SaveChanges();
                context.Oglas.AddRange(
                    new Oglas {/*Id = 1*/ JobTitle = "Junior Developer", Description = "We are looking for a junior developer who has at least 1 year of experience", EmployerId = 1 },
                    new Oglas {/*Id = 2*/ JobTitle = "Senior Developer", Description = "We are looking for a senior developer who has at least 5 year of experience", EmployerId = 2 },
                    new Oglas {/*Id = 3*/ JobTitle = "Developer", Description = "We are looking for a developer who has at least 3 year of experience", EmployerId = 3 }
                    );
                context.SaveChanges();
            }


        }

    }
}
