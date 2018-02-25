using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.ViewModels
{
    public class RegijeIndexViewModel
    {
        public List<Row> Rows { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public class Row
        {
            public int RegijaID { get; set; }
            public string Naziv { get; set; }
            public string Oznaka { get; set; }
            public string Drzava { get; set; }
        }
    }
}
