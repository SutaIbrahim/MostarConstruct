using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MostarConstruct.Data.Models
{
    public class Fajl
    {
        [Key]
        public int FajlId { get; set; }
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Naziv { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public string Lokacija { get; set; }

    }
}
