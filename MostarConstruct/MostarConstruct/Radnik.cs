using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Radnik
    {
        [Key]
        [Required]
        [ForeignKey(nameof(Osoba))]
        public int RadnikID { get; set; }
        public Osoba Osoba { get; set; }
        [Required]
        [ForeignKey(nameof(Pozicija))]
        public int PozicijaID { get; set; }
        public Pozicija Pozicija { get; set; }
        [Required]
        [Display(Name = "Datum zaposlenja")]
        public DateTime DatumZaposlenja { get; set; }
        [Required]
        [Display(Name = "Dodatak na satnicu")]
        [Range(0, double.MaxValue)]
        public decimal DodatakNaSatnicu { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Staz { get; set; }
        [Required]
        public bool Aktivan { get; set; }
    }


}
