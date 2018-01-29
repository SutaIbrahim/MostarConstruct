using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Regija
    {
        [Key]
        public int RegijaID { get; set; }
        [Required]
        [ForeignKey(nameof(Drzava))]
        public int DrzavaID { get; set; }
        public Drzava Drzava { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Naziv { get; set; }
        [StringLength(10, MinimumLength = 2)]
        public string Oznaka { get; set; }
    }
}
