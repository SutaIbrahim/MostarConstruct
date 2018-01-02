using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.ViewModels
{
    public class RacunLozinkaViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PotvrdaLozinke { get; set; }
    }
}
