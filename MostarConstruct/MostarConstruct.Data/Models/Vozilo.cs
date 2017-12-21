using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Vozilo
    {
        [Key]
        public int VoziloID { get; set; }
        [Required]
        [ForeignKey(nameof(VrstaVozila))]
        public int VrstaVozilaID { get; set; }
        public VrstaVozila VrstaVozila { get; set; }
        [Required]
        [ForeignKey(nameof(VozackaKategorija))]
        public int VozackaKategorijaID { get; set; }
        public VozackaKategorija VozackaKategorija { get; set; }
        [Required]
        [Display(Name = "Datum registracije")]
        public DateTime DatumRegistracije { get; set; }
        [StringLength(20)]
        public string Nosivost { get; set; }
        [Required]
        [Display(Name = "Broj sjedista")]
        [Range(1, 20)]
        public int BrojSjedista { get; set; }
        [Required]
        [Display(Name = "Godina proizvodnje")]
        public int GodinaProizvodnje { get; set; }
        [Required]
        [Display(Name = "Datum kupovine")]
        public DateTime DatumKupovine { get; set; }
        [Required]
        [StringLength(30)]
        public string Kubikaza { get; set; }
        [Display(Name = "Datum zadnjeg servisa")]
        public DateTime? DatumZadnjegServisa { get; set; }
        [Required]
        public bool Zauzeto { get; set; }
        [Required]
        [StringLength(20)]
        public string RegistarskaOznaka { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Proizvodjac { get; set; }
        [Display(Name = "Cijena po satu")]
        public decimal? CijenaPoSatu { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 5)]
        [Display(Name = "Broj sasije")]
        public string BrojSasije { get; set; }

    }
}
