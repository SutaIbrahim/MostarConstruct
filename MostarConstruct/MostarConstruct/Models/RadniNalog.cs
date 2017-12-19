using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class RadniNalog
    {
        [Key]
        public int RadniNalogID { get; set; }
        [Required]
        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
        [Required]
        [ForeignKey(nameof(Radiliste))]
        public int RadilisteID { get; set; }
        public Radiliste Radiliste { get; set; }
        [Required]
        [Display(Name = "Datum dodjele")]
        public DateTime DatumDodjele { get; set; }
        [Required]
        [Display(Name = "Datum od")]
        public DateTime DatumOd { get; set; }
        [Required]
        [Display(Name = "Datum do")]
        public DateTime DatumDo { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Zaduzenje { get; set; }
        public string Napomena { get; set; }
    }
}
