using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Uvjerenje
    {
        [Key]
        public int UvjerenjeID { get; set; }
        [Required]
        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
        [Required]
        [ForeignKey(nameof(ClanUprave))]
        public int ClanUpraveID { get; set; }
        public Korisnik ClanUprave { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Broj protokola")]
        public string BrojProtokola { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Svrha { get; set; }
        public string Napomena { get; set; }
        [Required]
        [Display(Name = "Datum izdavanja")]
        public DateTime DatumIzdavanja { get; set; }
    }
}
