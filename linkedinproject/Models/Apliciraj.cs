using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace linkedinproject.Models
{
    public class Apliciraj
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("OglasId")]
        public int? OglasId { get; set; }
        public Oglas Oglas { get; set; }
    }
}
