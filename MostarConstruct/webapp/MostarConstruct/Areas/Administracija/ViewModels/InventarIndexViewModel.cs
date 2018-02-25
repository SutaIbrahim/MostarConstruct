using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.ViewModels
{
    public class InventarIndexViewModel
    {
        public List<Row> Rows{ get; set; }
        public PagingInfo PagingInfo{ get; set; }
        public string SearchString { get; set; }
        public class Row
        {
            public int InventarID { get; set; }
            public string Naziv { get; set; }
            public string SerijskiBroj { get; set; }
            public string Kategorija { get; set; }
            public string Zauzet { get; set; }
            public string Ispravan { get; set; }
            public DateTime? DatumKupovine { get; set; }
        }
    }
}
