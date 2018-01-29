using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Osoba
    {
        [Key]
        public int OsobaID { get; set; }
        [Required]
        [ForeignKey(nameof(Grad))]
        public int GradID { get; set; }
        public Grad Grad { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Ime { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Prezime { get; set; }
        [Required]
        [Display(Name = "Datum rodjenja")]
        public DateTime DatumRodjenja { get; set; }
        [StringLength(13, MinimumLength = 13)]
        public string JMBG { get; set; }
        [StringLength(15)]
        public string Spol { get; set; }
        public string ContentType { get; set; }
        public byte[] Slika { get; set; }
        [StringLength(100)]
        public string Telefon { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(30)]
        public string BracniStatus{ get; set; }
        [StringLength(100)]
        public string Adresa { get; set; }
    }
}
