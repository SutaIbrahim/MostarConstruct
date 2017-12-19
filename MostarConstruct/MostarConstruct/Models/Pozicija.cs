using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Pozicija
    {
        [Key]
        public int PozicijaID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Naziv { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Satnica { get; set; }
    }
}
