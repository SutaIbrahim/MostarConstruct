using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;


namespace MostarConstruct.Web.Areas.Poslovodja.ViewModels
{
    public class RadniciDodajViewModel
    {

        public Osoba Osoba { get; set; }
        public Radnik Radnik { get; set; }

        public int DrzavaID { get; set; }
        public int RegijaID { get; set; }
        public int GradID { get; set; }
        public int PozicijaID { get; set; }



        public IEnumerable<SelectListItem> Drzave { get; set; }
        public IEnumerable<SelectListItem> Regije { get; set; }
        public IEnumerable<SelectListItem> Gradovi { get; set; }
        public IEnumerable<SelectListItem> Pozicije { get; set; }


        public IFormFile Slika { get; set; }



    }
}
