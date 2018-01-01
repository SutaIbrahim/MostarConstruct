using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
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
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(model));

            Osoba osoba = model.Osoba;
            db.Osobe.Add(osoba);

            Korisnik korisnik = model.Korisnik;
            korisnik.KorisnikID = osoba.OsobaID;
            korisnik.LozinkaHash = Sigurnost.GenerisiPassword();
            korisnik.DatumRegistracije = DateTime.Now;
            korisnik.Aktivan = true;
            korisnik.PromijenioLozinku = false;

            if(model.TipKorisnika == TipKorisnika.Administrator)
            {
                korisnik.IsAdmin = true;
                korisnik.IsClanUprave = korisnik.IsPoslovodja = false;
            }
            else if(model.TipKorisnika == TipKorisnika.Poslovodja)
            {
                korisnik.IsPoslovodja = true;
                korisnik.IsAdmin = korisnik.IsClanUprave = false;
            }
            else
            {
                korisnik.IsClanUprave = true;
                korisnik.IsPoslovodja = korisnik.IsAdmin = false;
            }

            db.Korisnici.Add(korisnik);
            db.SaveChanges();

            return Content("De pgoedaj bazu");
        }

        private KorisniciDodajViewModel GetDefaultViewModel(KorisniciDodajViewModel model)
        {
            model.Osoba = model.Osoba ?? new Models.Osoba();
            model.Korisnik = model.Korisnik ?? new Models.Korisnik();
            model.Gradovi = model.Gradovi ?? dropdown.Gradovi();
            model.Regije = model.Regije ?? dropdown.Regije();
            model.Drzave = model.Drzave ?? dropdown.Drzave();
            model.Uloge = model.Uloge ?? dropdown.Uloge();

            return model;
        }
    }
}