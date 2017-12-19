using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class VozaciKategorije
    {
        [Key]
        [Required]
        [ForeignKey(nameof(Vozac))]
        [Column(Order = 0)]
        public int VozacID { get; set; }
        public Radnik Vozac { get; set; }
        [Key]
        [Required]
        [ForeignKey(nameof(VozackaKategorija))]
        [Column(Order = 1)]
        public int KategorijaID { get; set; }
        public VozackaKategorija VozackaKategorija { get; set; }
        [Required]
        [Display(Name = "Datum polaganja")]
        public DateTime DatumPolaganja { get; set; }
        [Required]
        [Display(Name = "Vazi do")]
        public DateTime VaziDo { get; set; }
    }
}
