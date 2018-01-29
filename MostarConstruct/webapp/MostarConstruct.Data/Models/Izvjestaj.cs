using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Izvjestaj
    {
        [Key]
        public int IzvjestajID { get; set; }
        [Required]
        [ForeignKey(nameof(Projekt))]
        public int ProjektID { get; set; }
        public Projekt Projekt { get; set; }
        [Required]
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        [Required]
        [Display(Name = "Broj protokola")]
        public string BrojProtokola { get; set; }
        [Required]
        [Display(Name = "Datum izdavanja")]
        public DateTime DatumIzdavanja { get; set; }
        [Required]
        public string Svrha { get; set; }
        public string Napomena { get; set; }
    }
}
