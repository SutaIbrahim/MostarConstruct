using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Klijent
    {
        [Key]
        public int KlijentID { get; set; }
        [Required]
        [ForeignKey(nameof(TipKlijenta))]
        public int TipKlijentaID { get; set; }
        public TipKlijenta TipKlijenta { get; set; }
        [Required]
        [Display(Name = "Kontakt osoba")]
        [StringLength(100, MinimumLength = 3)]
        public string KontaktOsoba { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Telefon { get; set; }
        [StringLength(100)]
        public string Adresa { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Kompanija { get; set; }
        [Required]
        [StringLength(30)]
        public string Ziroracun { get; set; }
        [StringLength(30)]
        public string Fax { get; set; }
        [Display(Name = "Broj narucenih projekata")]
        public int BrojNarucenihProjekata { get; set; }
    }
}
