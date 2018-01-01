using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.ViewModels
{
    public class KorisniciDodajViewModel
    {
        public Osoba Osoba { get; set; }
        public Korisnik Korisnik { get; set; }
        public int DrzavaID { get; set; }
        public int RegijaID { get; set; }
        public TipKorisnika TipKorisnika{ get; set; }
        public IEnumerable<SelectListItem> Drzave{ get; set; }
        public IEnumerable<SelectListItem> Regije{ get; set; }
        public IEnumerable<SelectListItem> Gradovi { get; set; }
        public IEnumerable<SelectListItem> Uloge { get; set; }
        public IFormFile Slika{ get; set; }
    }
}
