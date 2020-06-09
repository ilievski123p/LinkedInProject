using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace linkedinproject.Models
{
    public class Oglas
    {
        [Key]
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        [ForeignKey("EmployeeId")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        [ForeignKey("EmployerId")]
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        [ForeignKey("AplicirajId")]
        public ICollection<Apliciraj> Aplikacii { get; set; }
    }
}
