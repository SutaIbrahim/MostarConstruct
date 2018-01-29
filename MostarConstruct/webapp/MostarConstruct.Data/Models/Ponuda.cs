using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Ponuda
    {
        [Key]
        public int PonudaID { get; set; }
        [ForeignKey(nameof(ClanUprave))]
        public int ClanUpraveID { get; set; }
        public Korisnik ClanUprave { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Naziv { get; set; }
        [Required]
        [Display(Name = "Datum izdavanja")]
        public DateTime DatumIzdavanja { get; set; }
        [Required]
        public string Sadrzaj { get; set; }
        [Required]
        [Display(Name = "Potrebno vrijeme (mjesec)")]
        [Range(0, double.MaxValue)]
        public decimal PotrebnoVrijeme { get; set; }
        [Required]
        [Display(Name = "Predlozena cijena")]
        [Range(0, double.MaxValue)]
        public decimal PredlozenaCijena { get; set; }
    }
}
