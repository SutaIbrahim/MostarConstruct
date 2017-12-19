using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class InventarRadiliste
    {
        [Key]
        public int InventarRadilisteID { get; set; }
        [Required]
        [ForeignKey(nameof(Inventar))]
        public int InventarID { get; set; }
        public Inventar Inventar { get; set; }
        [Required]
        [ForeignKey(nameof(Radiliste))]
        public int RadilisteID { get; set; }
        public Radiliste Radiliste { get; set; }
        [Required]
        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
        [Required]
        [ForeignKey(nameof(Poslovodja))]
        public int PoslovodjaID { get; set; }
        public Korisnik Poslovodja{ get; set; }
        [Required]
        [Display(Name = "Datum zauzimanja")]
        public DateTime DatumZauzimanja { get; set; }
        [Display(Name = "Zauzeto do")]
        public DateTime? ZauzetoDo { get; set; }
        [Required]
        public bool Vraceno { get; set; }
    }
}
