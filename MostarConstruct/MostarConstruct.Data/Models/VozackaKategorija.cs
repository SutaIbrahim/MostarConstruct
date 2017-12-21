using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class VozackaKategorija
    {
        [Key]
        public int VozackaKategorijaID { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Naziv { get; set; }
    }
}
