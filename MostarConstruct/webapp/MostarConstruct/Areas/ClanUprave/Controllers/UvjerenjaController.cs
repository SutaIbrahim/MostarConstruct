using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Web.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Web.ViewModels;
using MostarConstruct.Web.Areas.ClanUprave.ViewModels;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]
    public class UvjerenjaController : Controller
    {
        private DatabaseContext _db;
        public UvjerenjaController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dodaj()
        {
            UvjerenjaDodajVM Model = new UvjerenjaDodajVM();
            Model.listaRadnika = new List<SelectListItem>();
            Model.listaRadnika = _db.Radnici.Select(x => new SelectListItem
            {
                Value = x.RadnikID.ToString(),
                Text = x.Osoba.Ime + " " + x.Osoba.Prezime
            }).ToList();
            
            return View(Model);
        }
    }
}