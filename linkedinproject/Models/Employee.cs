using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace linkedinproject.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string ProfilePicutre { get; set; }
        public string CoverPhoto { get; set; }
        public string CV { get; set; }
        public string CoverLetter { get; set; }
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
        [ForeignKey("OglasId")]
        public ICollection<Oglas> Oglasi { get; set; }
        [ForeignKey("InterestId")]
        public ICollection<Interest> Interests { get; set; }
        [ForeignKey("AplicirajId")]
        public ICollection<Apliciraj> Aplikacii { get; set; }

    }
}
