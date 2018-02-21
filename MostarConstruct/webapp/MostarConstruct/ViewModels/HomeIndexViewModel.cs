using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.ViewModels
{
    public class HomeIndexViewModel
    {
        public Detalji Informacije { get; set; }

        public class Detalji
        {
            public int UkupnoProjekata { get; set; }
            public int UkupnoRadnihNaloga { get; set; }
            public int UkupnoRadnika { get; set; }
            public decimal UkupnaZarada { get; set; }
        }
    }
}
