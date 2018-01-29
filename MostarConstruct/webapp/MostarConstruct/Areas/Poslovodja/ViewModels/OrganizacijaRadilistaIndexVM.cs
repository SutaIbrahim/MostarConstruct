using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Poslovodja.ViewModels
{
    public class OrganizacijaRadilistaIndexVM
    {
        public int ProjekatId { get; set; }
        public List<SelectListItem> listaProjekata { get; set; }
        public int RadilisteId { get; set; }
        public List<SelectListItem> listaRadilista { get; set; }
    }
}
