using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Korisnik
    {
        [Key]
        [ForeignKey(nameof(Osoba))]
        [Required]
        public int KorisnikID { get; set; }
        public Osoba Osoba { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string KorisnickoIme { get; set; }
        public string LozinkaHash { get; set; }
        [Required]
        [Display(Name = "Datum registracije")]
        public DateTime DatumRegistracije { get; set; }
        [Display(Name = "Datum zadnje prijave")]
        public DateTime? DatumZadnjePrijave { get; set; }
        [Required]
        public bool Aktivan { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public bool IsPoslovodja { get; set; }
        [Required]
        public bool IsClanUprave { get; set; }
        [Required]
        public bool PromijenioLozinku { get; set; }
    }
}
