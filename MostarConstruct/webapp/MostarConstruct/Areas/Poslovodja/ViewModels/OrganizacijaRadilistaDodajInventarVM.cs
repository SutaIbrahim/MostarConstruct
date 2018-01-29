using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Poslovodja.ViewModels
{
    public class OrganizacijaRadilistaDodajInventarVM
    {
        public int RadilisteId { get; set; }
        [Required]
        public int InventarId { get; set; }
        public List<SelectListItem> listaInventara { get; set; }
        public int RadnikId { get; set; }
        public List<SelectListItem> listaRadnika { get; set; }
        public DateTime ZauzetoDo { get; set; }
    }
}
