using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class RadilistaDodajViewModel
    {
        public Radiliste Radiliste { get; set; }
        public int ProjektID { get; set; }
        public int GradID { get; set; }

        public List<SelectListItem> Projekti { get; set; }
        public List<SelectListItem> Gradovi { get; set; }



    }
}
