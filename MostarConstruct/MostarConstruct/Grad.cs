using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Grad
    {
        [Key]
        public int GradID { get; set; }
        [Required]
        [ForeignKey(nameof(Regija))]
        public int RegijaID { get; set; }
        public Regija Regija { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Naziv { get; set; }
        [StringLength(12, MinimumLength = 3)]
        public string PostanskiBroj { get; set; }
    }
}
