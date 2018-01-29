using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class IzvjestajDodajVIewModel
    {

        public List<SelectListItem> projekti { get; set; }
        public Izvjestaj izvjestaj { get; set; }



    }
}
