using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MostarConstruct.Web.Areas.Poslovodja.ViewModels;
using MostarConstruct.Web.Helper.IHelper;
using System.Text.RegularExpressions;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using DNTCaptcha.Core;
using DNTCaptcha.Core.Providers;

namespace MostarConstruct.Web.Areas.Poslovodja.Controllers
{

    [Autorizacija(false, TipKorisnika.Poslovodja)]

    [Area("Poslovodja")]


    public class RadniciController : Controller
    {
        private DatabaseContext db;
        private IDropdown dropdown;
        IHttpContextAccessor context;


        public RadniciController(DatabaseContext db, IDropdown dropdown, IHttpContextAccessor context)
        {
            this.db = db;
            this.dropdown = dropdown;
            this.context = context;

        }


        #region Index
        public IActionResult Index()
        {
            RadniciIndexViewModel model = new RadniciIndexViewModel();

            model.Radnici = db.Radnici.Include(p => p.Pozicija).Include(o => o.Osoba).ThenInclude(g => g.Grad).ToList();

            return View(model);
        }
        #endregion

        #region Pretraga
        public IActionResult Pretraga(string srchTxt)
        {
            if (srchTxt == null)
                return RedirectToAction(nameof(Index));

            RadniciIndexViewModel model = new RadniciIndexViewModel();

            model.Radnici = db.Radnici.Include(p => p.Pozicija).Include(o => o.Osoba).ThenInclude(g => g.Grad).Where(x => (x.Osoba.Ime + " " + x.Osoba.Prezime).StartsWith(srchTxt) || (x.Osoba.Prezime + " " + x.Osoba.Ime).StartsWith(srchTxt)).ToList();

            model.srchTxt = srchTxt;
            
            return View("Index",model);
        }
        #endregion


        #region Dodaj
        public IActionResult Dodaj()
        {
            return View(GetDefaultViewModel(new RadniciDodajViewModel()));
        }

        [HttpPost]
        [ValidateDNTCaptcha(ErrorMessage = "Unesite broj sa slike",
                            IsNumericErrorMessage = "Unesite u obliku brojeva",
                            CaptchaGeneratorLanguage = Language.English)]
        public IActionResult Dodaj(RadniciDodajViewModel model)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(model));

            Osoba osoba = model.Osoba;

            osoba.GradID = model.GradID;

            db.Osobe.Add(osoba);


            Radnik radnik = model.Radnik;

            radnik.RadnikID = osoba.OsobaID;
            radnik.PozicijaID = model.PozicijaID;
                 radnik.Aktivan = true;

            db.Radnici.Add(radnik);
            db.SaveChanges();

            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Dodavanje radnika", "Radnici");


            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Obrisi
        public IActionResult Obrisi(int id)
        {
            Radnik x = db.Radnici.Where(r => r.RadnikID == id).FirstOrDefault();

            x.Aktivan = false;

            db.Radnici.Update(x);
            db.SaveChanges();

            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Brisanje radnika", "Radnici");


            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Uredi
        public IActionResult Uredi(int RadnikID) {

            Osoba o = db.Osobe.Include(x => x.Grad).ThenInclude(k => k.Regija).Where(x => x.OsobaID == RadnikID).SingleOrDefault();
            Radnik radnik = db.Radnici.Where(y => y.RadnikID == RadnikID).SingleOrDefault();


            RadniciDodajViewModel vm = GetDefaultViewModel(new RadniciDodajViewModel()
            {
                Osoba = o,
                Radnik = radnik,
                DrzavaID = o.Grad.Regija.DrzavaID,
                RegijaID = o.Grad.RegijaID,
                GradID = o.GradID,
                PozicijaID = radnik.PozicijaID

            });

            return View(vm);
        }


        [HttpPost]
        public IActionResult Uredi(RadniciDodajViewModel model)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(model));


            Osoba osoba = model.Osoba;
            osoba.GradID = model.GradID;

            db.Osobe.Update(osoba);


            Radnik radnik = model.Radnik;

            radnik.PozicijaID = model.PozicijaID;
            radnik.Aktivan = true;


            db.Radnici.Update(radnik);
            db.SaveChanges();


            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Uredjivanje radnika", "Radnici");


            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region GrupniUploadSlika
        public IActionResult Slike()
        {
            RadniciGrupniUploadSlikaViewModel viewModel = new RadniciGrupniUploadSlikaViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Slike(RadniciGrupniUploadSlikaViewModel viewModel)
        {
            if (viewModel.Slike == null)
            {
                TempData["greske"] += "<li>Morate odabrati najmanje jednu sliku za upload</li>";
                return View(nameof(Slike), viewModel);
            }

            List<string> dozvoljeneEkstenzijeSlika = new List<string>() { ".jpg", ".jpeg", ".png", ".JPG", ".JPEG", ".PNG" };
            Regex pravilo = new Regex(@"[0-9]{13}\.[a-z]{3,}");

            List<Radnik> radnici = db.Radnici.Include(x => x.Osoba).ToList();

            foreach (var slika in viewModel.Slike)
            {
                if (slika.ContentType.Contains("images"))
                    TempData["greske"] += $"<li>{slika.FileName} nije slika</li>";
                if(!pravilo.IsMatch(slika.FileName))
                    TempData["greske"] += $"<li>{slika.FileName} naziv nije validan</li>";
                if(dozvoljeneEkstenzijeSlika.All(x => !slika.FileName.EndsWith(x)))
                    TempData["greske"] += $"<li>{slika.FileName} naziv u validnom formatu</li>";

                string JMBGRadnika = "";

                string[] naziv = slika.FileName.Split(".");
                if (naziv[0].Length == 13)
                    JMBGRadnika = slika.FileName.Substring(0, 13);

                Radnik radnik = null;

                radnik = radnici.Where(x => x.Osoba.JMBG == JMBGRadnika).FirstOrDefault();

                if (radnik == null)
                    TempData["greske"] += $"<li>Radnik sa {slika.FileName} ne postoji.</li>";
                else
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        slika.CopyTo(memoryStream);
                        radnik.Osoba.Slika = memoryStream.ToArray();
                        radnik.Osoba.ContentType = slika.ContentType;

                        db.Radnici.Update(radnik);
                        TempData["uspjeh"] += $"<li>Uspjesno pohranjena slika za {radnik.Osoba.Ime} {radnik.Osoba.Prezime} ({radnik.Osoba.JMBG})</li>";

                    }
                }
            }

            db.SaveChanges();
            //return Json(new { success = true});
            return RedirectToAction(nameof(Slike));
        }
        #endregion

        #region Helper
        private RadniciDodajViewModel GetDefaultViewModel(RadniciDodajViewModel model)
        {
            model.Osoba = model.Osoba ?? new Osoba();
            model.Radnik = model.Radnik ?? new Radnik();
            model.Gradovi = model.Gradovi ?? dropdown.Gradovi();
            model.Regije = model.Regije ?? dropdown.Regije();
            model.Drzave = model.Drzave ?? dropdown.Drzave();
            model.Pozicije = model.Pozicije ?? dropdown.Pozicije();


            return model;
        }

        #endregion




    }
}