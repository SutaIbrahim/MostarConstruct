using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.ViewModels
{
    public class VozilaDodajViewModel
    {
        public Vozilo Vozilo { get; set; }
        public List<SelectListItem> vrsteVozila { get; set; }
        public List<SelectListItem> vozackeKategorije { get; set; }
    }
}
