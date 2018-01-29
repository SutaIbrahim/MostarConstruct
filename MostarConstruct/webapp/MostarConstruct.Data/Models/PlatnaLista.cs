using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class PlatnaLista
    {
        [Key]
        public int PlatnaListaID { get; set; }
        [Required]
        [ForeignKey(nameof(ClanUprave))]
        public int ClanUpraveID { get; set; }
        public Korisnik ClanUprave { get; set; }
        [Required]
        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
        [Required]
        [Display(Name = "Broj protokola")]
        [StringLength(30)]
        public string BrojProtokola { get; set; }
        [Required]
        [Display(Name = "Broj mjeseci")]
        [StringLength(5)]
        public string BrojMjeseci { get; set; }
        [Required]
        public string Svrha { get; set; }
        [Required]
        [Display(Name = "Datum izdavanja")]
        public DateTime DatumIzdavanja { get; set; }
        public string Napomena { get; set; }
    }
}
