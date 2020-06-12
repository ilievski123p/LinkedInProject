using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace linkedinproject.Models
{
    [Table("AspNetUsers")]
    public class AppUser : IdentityUser
    {
        [Display(Name ="Role")]
        public string Role { get; set; }
        public int? EmployeeId { get; set; }
        [Display(Name = "Employee")]
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int? EmployerId { get; set; }
        [Display(Name = "Employer")]
        [ForeignKey("EmployerId")]
        public Employer Employer { get; set; }
    }
}
