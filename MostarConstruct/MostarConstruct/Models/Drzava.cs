using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Drzava
    {
        [Key]
        public int DrzavaID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Naziv { get; set; }
        [StringLength(10, MinimumLength = 2)]
        public string Oznaka { get; set; }
    }
}
