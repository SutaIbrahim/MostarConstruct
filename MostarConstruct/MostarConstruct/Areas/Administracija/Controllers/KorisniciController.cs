using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            KorisniciIndexViewModel vm = new KorisniciIndexViewModel()
            {
                Rows = db.Korisnici.Include(x => x.Osoba).Select(k => new KorisniciIndexViewModel.Row()
                {
                    KorisnikID = k.KorisnikID,
                    Ime = k.Osoba.Ime,
                    Prezime = k.Osoba.Prezime, 
                    DatumRegistracije = k.DatumRegistracije,
                    Email = k.Osoba.Email,
                    KorisnickoIme = k.KorisnickoIme,
                    Aktivan = k.Aktivan == true ? "Da" : "Ne"
                }).ToList()
            };
            return View(vm);
        }

        #region Dodaj
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

            if (model.TipKorisnika == TipKorisnika.Administrator)
            {
                korisnik.IsAdmin = true;
                korisnik.IsClanUprave = korisnik.IsPoslovodja = false;
            }
            else if (model.TipKorisnika == TipKorisnika.Poslovodja)
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
        #endregion

        #region Obrisi
        [HttpPost]
        public IActionResult Obrisi(int korisnikID)
        {
            Korisnik korisnik = db.Korisnici.Where(x => x.KorisnikID == korisnikID).FirstOrDefault();

            korisnik.Aktivan = false;

            db.Korisnici.Update(korisnik);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Uredi
        public IActionResult Uredi(int korisnikID)
        {
            Osoba o = db.Osobe.Include(x => x.Grad).ThenInclude(k => k.Regija).FirstOrDefault(x => x.OsobaID == korisnikID);
            Korisnik korisnik = db.Korisnici.FirstOrDefault(x => x.KorisnikID == korisnikID);
            TipKorisnika tip;

            if (korisnik.IsAdmin)
                tip = TipKorisnika.Administrator;
            else if (korisnik.IsPoslovodja)
                tip = TipKorisnika.Poslovodja;
            else
                tip = TipKorisnika.ClanUprave;

            KorisniciDodajViewModel vm = GetDefaultViewModel(new KorisniciDodajViewModel()
            {
                Osoba = o,
                Korisnik = korisnik,
                DrzavaID = o.Grad.Regija.DrzavaID,
                RegijaID = o.Grad.RegijaID,
                TipKorisnika = tip
            });

            return View(vm);
        }

        [HttpPost]
        public IActionResult Uredi(KorisniciDodajViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(viewModel));

            Osoba o = viewModel.Osoba;
            db.Osobe.Update(o);

            Korisnik k = viewModel.Korisnik;
            db.Korisnici.Update(k);

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        #endregion

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