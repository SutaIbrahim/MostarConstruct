using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class VrstaVozila
    {
        [Key]
        public int VrstaVozilaID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Naziv { get; set; }
    }
}
