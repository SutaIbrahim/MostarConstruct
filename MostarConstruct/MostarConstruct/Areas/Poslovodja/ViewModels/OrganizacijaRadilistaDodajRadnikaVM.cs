using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Poslovodja.ViewModels
{
    public class OrganizacijaRadilistaDodajRadnikaVM
    {
        public int RadilisteId { get; set; }
        public int RadnikId { get; set; }
        public DateTime DatumDo { get; set; }
        public string Napomena { get; set; }
        public string Zaduzenje { get; set; }
        public List<SelectListItem> listaRadnika { get; set; }
    }
}
