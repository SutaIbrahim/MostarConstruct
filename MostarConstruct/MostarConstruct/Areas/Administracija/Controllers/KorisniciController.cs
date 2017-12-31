using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Web.Areas.Administracija.ViewModels;
using MostarConstruct.Web.Helper;
using MostarConstruct.Web.Helper.IHelper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{
    [Autorizacija(false, TipKorisnika.Administrator)]
    [Area("Administracija")]
    public class KorisniciController : Controller
    {
        private DatabaseContext db;
        private IDropdown dropdown; 

        public KorisniciController(DatabaseContext db, IDropdown dropdown)
        {
            this.db = db;
            this.dropdown = dropdown;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dodaj()
        {
            return View(GetDefaultViewModel(new KorisniciDodajViewModel()));
        }

        [HttpPost]
        public IActionResult Dodaj(KorisniciDodajViewModel model)
        {
            return View();
        }

        private KorisniciDodajViewModel GetDefaultViewModel(KorisniciDodajViewModel model)
        {
            model.Osoba = model.Osoba ?? new Models.Osoba();
            model.Korisnik = model.Korisnik ?? new Models.Korisnik();
            model.Gradovi = model.Gradovi ?? dropdown.Gradovi();
            model.Regije = model.Regije ?? dropdown.Regije();
            model.Drzave = model.Drzave ?? dropdown.Drzave();
            model.Uloge = model.Uloge ?? null;

            return model;
        }
    }
}