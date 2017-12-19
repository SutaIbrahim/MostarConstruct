using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class ProjektDokumenti
    {
        [Key]
        public int ProjektDokumentiID { get; set; }
        [Required]
        [ForeignKey(nameof(Projekt))]
        public int ProjektID { get; set; }
        public Projekt Projekt { get; set; }
        [Required]
        [ForeignKey(nameof(Poslovodja))]
        public int PoslovodjaID { get; set; }
        public Korisnik Poslovodja { get; set; }
        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }
        [Required]
        [Display(Name = "Datum kreiranja")]
        public DateTime DatumKreiranja { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Velicina { get; set; }
        public byte[] Dokument { get; set; }
        [Display(Name = "Broj preuzimanja")]
        public int BrojPreuzimanja { get; set; }
    }
}
