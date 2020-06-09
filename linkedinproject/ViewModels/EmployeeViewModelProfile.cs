using linkedinproject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace linkedinproject.ViewModels
{
    public class EmployeeViewModelProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Age { get; set; }

        public string GitHubLink { get; set; }
        public string CurrentPosition { get; set; }
        public string WantedPosition { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Skills { get; set; }
        public string FullName
        {

            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        public IFormFile ProfileImage { get; set; }
        public IFormFile CoverImage { get; set; }
        public string CVFile { get; set; }
        public string CoverLetterFile { get; set; }


        [ForeignKey("OglasId")]
        public ICollection<Oglas> Oglasi { get; set; }
        [ForeignKey("InterestId")]
        public ICollection<Interest> Interests { get; set; }
    }
}
