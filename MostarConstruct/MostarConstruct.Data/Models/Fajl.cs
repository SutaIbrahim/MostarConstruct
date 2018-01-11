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
        public string Naziv { get; set; }
        public string Tip { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public byte[] Podaci { get; set; }
    }
}
