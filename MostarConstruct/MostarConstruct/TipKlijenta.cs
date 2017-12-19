using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class TipKlijenta
    {
        [Key]
        public int TipKlijentaID { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Naziv { get; set; }
    }
}
