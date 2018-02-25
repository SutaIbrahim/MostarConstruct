using MostarConstruct.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.ViewModels
{
    public class GradoviIndexViewModel
    {
        public List<Row> Rows{ get; set; }
        public PagingInfo PagingInfo{ get; set; }
        public class Row
        {
            public int GradId { get; set; }
            public string Naziv { get; set; }
            public string Regija { get; set; }
            public string PostanskiBroj { get; set; }
            public string Drzava { get; set; }
        }
    }
}
