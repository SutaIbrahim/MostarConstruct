using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class UvjerenjaIndexVM
    {
        public class Row
        {
            public int UvjerenjeId { get; set; }
            public string BrojProtokola { get; set; }
            public string Radnik { get; set; }
            public DateTime DatumIzdavanja { get; set; }
        }
        public List<Row> listaUvjerenja { get; set; }
    }
}
