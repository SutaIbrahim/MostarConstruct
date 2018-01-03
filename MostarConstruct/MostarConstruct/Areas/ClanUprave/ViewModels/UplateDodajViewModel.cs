using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class UplateDodajViewModel
    {

        public Uplata Uplata { get; set; }
        public int ProjektID { get; set; }
        public int KlijentID { get; set; }

        public List<SelectListItem> Projekti { get; set; }
        public List<SelectListItem> Klijenti { get; set; }




    }
}
