using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Uplata
    {
        [Key]
        public int UplataID { get; set; }
        [Required]
        [ForeignKey(nameof(Klijent))]
        public int KlijentID { get; set; }
        public Klijent Klijent { get; set; }
        [Required]
        [ForeignKey(nameof(ClanUprave))]
        public int ClanUpraveID { get; set; }
        public Korisnik ClanUprave{ get; set; }
        [Required]
        [ForeignKey(nameof(Projekt))]
        public int ProjektID { get; set; }
        public Projekt  Projekt { get; set; }
        [Required]
        [Display(Name = "Datum uplate")]
        public DateTime DatumUplate { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Broj uplate")]
        public int BrojUplate { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Iznos { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Svrha { get; set; }
        [Required]
        [StringLength(100)]
        public string Status { get; set; }
    }
}
