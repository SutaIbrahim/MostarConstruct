using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Plata
    {
        [Key]
        public int PlataID { get; set; }
        [Required]
        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
        [Required]
        [Display(Name = "Datum knjizenja")]
        public DateTime DatumKnjizenja { get; set; }
        [Required]
        [Display(Name = "Iznos bonusa")]
        [Range(0, double.MaxValue)]
        public decimal BonusIznos { get; set; }
        [Required]
        [Display(Name = "Bruto iznos")]
        [Range(0, double.MaxValue)]
        public decimal BrutoIznos { get; set; }
        [Required]
        [Display(Name = "Neto iznos")]
        [Range(0, double.MaxValue)]
        public decimal NetoIznos { get; set; }
        [Required]
        [Range(1, 12)]
        public int Mjesec { get; set; }
        [Required]
        [Range(2015, 2020)]
        public int Godina { get; set; }

    }
}
