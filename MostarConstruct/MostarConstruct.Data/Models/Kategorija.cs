using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Models
{
    public class Kategorija
    {
        [Key]
        public int KategorijaID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Naziv { get; set; }
    }
}
