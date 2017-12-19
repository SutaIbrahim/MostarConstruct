using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class PutniNalog
    {
        [Key]
        public int PutniNalogID { get; set; }
        [Required]
        [ForeignKey(nameof(Poslovodja))]
        public int KorisnikID { get; set; }
        public Korisnik Poslovodja { get; set; }
        [Required]
        [ForeignKey(nameof(Radiliste))]
        public int RadilisteID { get; set; }
        public Radiliste Radiliste { get; set; }
        [Required]
        [ForeignKey(nameof(Vozilo))]
        public int VoziloID { get; set; }
        public Vozilo Vozilo { get; set; }
        [Required]
        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
        [Required]
        [ForeignKey(nameof(Grad))]
        public int GradID { get; set; }
        public Grad Grad { get; set; }
        [Required]
        [Display(Name = "Datum izdavanja")]
        public DateTime DatumIzdavanja { get; set; }
        [Required]
        [Display(Name = "Vrijedi do")]
        public DateTime VrijediDo { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Svrha { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Troskovi putovanja")]
        public decimal TroskoviPutovanja { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Ukupno radnih sati")]
        public decimal UkupnoRadnihSati { get; set; }
        public string Napomena { get; set; }
    }
}
