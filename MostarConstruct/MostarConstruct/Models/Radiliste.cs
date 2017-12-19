using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Radiliste
    {
        [Key]
        public int RadilisteID { get; set; }
        [Required]
        [ForeignKey(nameof(Projekt))]
        public int ProjektID { get; set; }
        public Projekt Projekt { get; set; }
        [Required]
        [ForeignKey(nameof(Grad))]
        public int GradID { get; set; }
        public Grad Grad { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Naziv { get; set; }
        [Required]
        [Display(Name = "Pocetak radova")]
        public DateTime PocetakRadova { get; set; }
        [Display(Name = "Zavrsetak radova")]
        public DateTime? ZavrsetakRadova { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string NadzorniOrgan { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Adresa { get; set; }
        public string Opis { get; set; }
    }
}
