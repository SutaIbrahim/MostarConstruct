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
        #region DI
        private DatabaseContext db;
        private IDropdown dropdown;
        private IEmailSender emailSender;
        public int PageSize = 4;

        public KorisniciController(DatabaseContext db, IDropdown dropdown, IEmailSender emailSender)
        {
            this.db = db;
            this.dropdown = dropdown;
            this.emailSender = emailSender;
        }
        #endregion

        #region Index
        public IActionResult Index(string sortOrder, int page = 1, string searchString = null)
        {
            ViewBag.ImeSortParm = String.IsNullOrEmpty(sortOrder) ? "Ime_desc" : "";
            ViewBag.DatumSortParm = sortOrder == "Datum" ? "Datum_desc" : "Datum";

            KorisniciIndexViewModel vm = new KorisniciIndexViewModel();

            if (string.IsNullOrEmpty(searchString))
            {
                vm.Rows = db.Korisnici.Include(x => x.Osoba).Select(k => new KorisniciIndexViewModel.Row()
                {
                    KorisnikID = k.KorisnikID,
                    Ime = k.Osoba.Ime,
                    Prezime = k.Osoba.Prezime,
                    DatumRegistracije = k.DatumRegistracije,
                    Email = k.Osoba.Email,
                    KorisnickoIme = k.KorisnickoIme,
                    Aktivan = k.Aktivan == true ? "Da" : "Ne"
                }).OrderBy(x => x.KorisnikID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                vm.PagingInfo = new Web.ViewModels.PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Korisnici.Count()
                };
            }
            else
            {
                vm.Rows = db.Korisnici.Include(x => x.Osoba).Select(k => new KorisniciIndexViewModel.Row()
                {
                    KorisnikID = k.KorisnikID,
                    Ime = k.Osoba.Ime,
                    Prezime = k.Osoba.Prezime,
                    DatumRegistracije = k.DatumRegistracije,
                    Email = k.Osoba.Email,
                    KorisnickoIme = k.KorisnickoIme,
                    Aktivan = k.Aktivan == true ? "Da" : "Ne"
                }).Where(x => x.Ime.Contains(searchString) || x.Prezime.Contains(searchString) || x.KorisnickoIme.Contains(searchString) || x.Email.Contains(searchString)).OrderBy(x => x.KorisnikID).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                vm.PagingInfo = new Web.ViewModels.PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Korisnici.Where(x => x.Osoba.Ime.Contains(searchString) || x.Osoba.Prezime.Contains(searchString) || x.KorisnickoIme.Contains(searchString) || x.Osoba.Email.Contains(searchString)).OrderBy(x => x.KorisnikID).Count()
                };
            }            

            switch (sortOrder)
            {
                case "Ime_desc":
                    vm.Rows = vm.Rows.OrderByDescending(x => x.Ime);
                    break;
                case "Datum":
                    vm.Rows = vm.Rows.OrderBy(x => x.DatumRegistracije);
                    break;
                case "Datum_desc":
                    vm.Rows = vm.Rows.OrderByDescending(x => x.DatumRegistracije);
                    break;
                default:
                    vm.Rows = vm.Rows.OrderBy(x => x.Ime);
                    break;
            }

            return View(vm);
        }
        #endregion

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

            string lozinka = Sigurnost.GenerisiPassword(10, false);

            korisnik.LozinkaHash = Sigurnost.GenerisiHash(lozinka);
            korisnik.DatumRegistracije = DateTime.Now;
            korisnik.Aktivan = true;
            korisnik.PromijenioLozinku = false;

            string poruka = $"Poštovani {osoba.Ime} {osoba.Prezime}, na sistem se možete logovati sa sljedećim podacima:\nEmail: [<strong>{osoba.Email}</strong>]\nLozinka: [<strong>{lozinka}</strong>]\n\nNapomena: Prilikom prvog logiranja morate promijeniti vašu lozinku.";

            emailSender.SendEmailAsync(osoba.Email, "Pristupni podaci", poruka);

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

            return RedirectToAction(nameof(Index));
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
            k.KorisnikID = o.OsobaID;
            db.Korisnici.Update(k);

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Aktiviraj
        public IActionResult ChangeStatus(int id)
        {
            Korisnik user = db.Korisnici.First(x => x.KorisnikID == id);
            if (user.Aktivan)
                user.Aktivan = false;
            else
                user.Aktivan = true;

            db.Korisnici.Update(user);
            db.SaveChanges();

            string status = user.Aktivan ? "aktivan" : "neaktivan";

            return Json(new { success = true, status });
        }
        #endregion

        #region Helpers
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

        #endregion
    }
}