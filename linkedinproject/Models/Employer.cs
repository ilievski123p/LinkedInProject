using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace linkedinproject.Models
{
    public class Employer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string Description { get; set; }
        public string CEOName { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }

        [ForeignKey("OglasId")]
        public ICollection<Oglas> Oglasi { get; set; }
        [ForeignKey("InterestId")]
        public ICollection<Interest> Interests { get; set; }
    }
}
