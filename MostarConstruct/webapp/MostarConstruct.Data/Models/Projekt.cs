using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Projekt
    {
        [Key]
        public int ProjektID { get; set; }
        public string Naziv { get; set; }
        [Required]
        [ForeignKey(nameof(ClanUprave))]
        public int ClanUpraveID { get; set; }
        public Korisnik ClanUprave { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Cijena { get; set; }
        [Required]
        [Display(Name = "Predlozeni pocetak")]
        public DateTime PredlozeniPocetak { get; set; }
        [Required]
        [Display(Name = "Predlozeni zavrsetak")]
        public DateTime PredlozeniZavrsetak { get; set; }
        [Display(Name = "Stvarni pocetak")]
        public DateTime? StvarniPocetak { get; set; }
        [Display(Name = "Stvarni zavrsetak")]
        public DateTime? StvarniZavrsetak { get; set; }
        [Required]
        [Range(1, 12)]
        public int BrojRata { get; set; }
        [Required]
        public bool Zavrsen { get; set; }
        [Required]
        public string Opis { get; set; }
        public string Boja { get; set; }
    }
}
