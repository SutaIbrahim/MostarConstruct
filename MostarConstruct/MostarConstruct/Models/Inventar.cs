using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Inventar
    {
        [Key]
        public int InventarID { get; set; }
        [Required]
        [ForeignKey(nameof(Kategorija))]
        public int KategorijaID { get; set; }
        public Kategorija Kategorija { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Naziv { get; set; }
        [Display(Name = "Datum kupovine")]
        public DateTime? DatumKupovine { get; set; }
        [Required]
        public bool Zauzeto { get; set; }
        [Required]
        public bool Ispravno { get; set; }
        [StringLength(30)]
        public string Tezina { get; set; }
        [StringLength(30)]
        public string Pogon { get; set; }
        [Required]
        [Display(Name = "Serijski broj")]
        [StringLength(30, MinimumLength = 3)]
        public string SerijskiBroj { get; set; }
        public string Opis { get; set; }
    }
}
