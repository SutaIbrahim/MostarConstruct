using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MostarConstruct.Data.Models
{
    public class ProjektiFajlovi
    {
        [Key]
        public int ProjektFajlID { get; set; }

        [ForeignKey("Projekt")]
        public int ProjektID { get; set; }
        public Projekt Projekt { get; set; }

        [ForeignKey("Fajl")]
        public int FajlID { get; set; }
        public Fajl Fajl { get; set; }
    }
}
