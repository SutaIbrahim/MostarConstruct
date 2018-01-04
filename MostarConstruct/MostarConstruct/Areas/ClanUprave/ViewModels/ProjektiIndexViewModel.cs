using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class ProjektiIndexViewModel
    {
        public class Row
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string Cijena { get; set; }
            public string stvarniPocetak { get; set; }
            public string stvarniZavrsetak { get; set; }
            public string BrojRata { get; set; }
            public string zavrsen { get; set; }

        }
        public List<Row> listaProjekata { get; set; }
    }
}
