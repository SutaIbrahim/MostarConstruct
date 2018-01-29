using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.ViewModels
{
    public class RegijeDodajViewModel
    {
        public Regija Regija{ get; set; }
        public List<SelectListItem> Drzave{ get; set; }
    }
}
