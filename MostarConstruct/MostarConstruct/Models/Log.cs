using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }
        [Required]
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        [StringLength(100)]
        public string IPAdresa { get; set; }
        [Required]
        [StringLength(100)]
        public string Browser { get; set; }
        [Required]
        [StringLength(100)]
        public string Aktivnost { get; set; }
        [Required]
        [StringLength(100)]
        public string Tabela { get; set; }
    }
}
