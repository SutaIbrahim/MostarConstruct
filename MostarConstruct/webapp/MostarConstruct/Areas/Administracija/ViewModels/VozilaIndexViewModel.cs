using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.ViewModels
{
    public class VozilaIndexViewModel
    {
        public class Row
        {
            public int Id { get; set; }
            public string VrstaVozila { get; set; }
            public string VozackaKategorija { get; set; }
            public DateTime DatumRegistracije { get; set; }
            public string Nosivost { get; set; }
            public string GodinaProizvodnje { get; set; }
            public string RegistarskaOznaka { get; set; }
            public string CijenaPoSatu { get; set; }
            public string Proizvodjac { get; set; }
            public string Zauzeto { get; set; }
        }
        public List<Row> listaVozila { get; set; }
    }
}
