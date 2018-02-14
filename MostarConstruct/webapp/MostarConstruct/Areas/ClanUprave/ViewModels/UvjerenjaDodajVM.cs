using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class UvjerenjaDodajVM
    {
        public int UvjerenjeId { get; set; }
        public int RadnikId { get; set; }
        public List<SelectListItem> listaRadnika { get; set; }
        public string Svrha { get; set; }
        public string Napomena { get; set; }
    }
}
