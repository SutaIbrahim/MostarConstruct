using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using Microsoft.AspNetCore.Http;
using MostarConstruct.Web.Helper;
using MostarConstruct.Models;
using Microsoft.EntityFrameworkCore;
using MostarConstruct.Web.Areas.ClanUprave.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{

    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]
    public class UplateController : Controller
    {

        private DatabaseContext db;
        private IHttpContextAccessor httpContext;

        public UplateController(DatabaseContext db, IHttpContextAccessor httpContext)
        {
            this.db = db;
            this.httpContext = httpContext;
        }


        public IActionResult Index()
        {

            UplateIndexViewModel model = new UplateIndexViewModel();
            model.Uplate = db.Uplate.Include(p => p.Projekt).Include(k => k.Klijent).Include(c => c.ClanUprave).ThenInclude(o => o.Osoba).ToList();

            return View(model);
        }

        public IActionResult Izvjestaj(int UplataId)
        {
            UplateIndexViewModel model = new UplateIndexViewModel();

            if (UplataId != 0)
            {
                model.Uplate = db.Uplate.Include(p => p.Projekt).Include(k => k.Klijent).Include(c => c.ClanUprave).ThenInclude(o => o.Osoba).Where(u=>u.UplataID==UplataId).ToList();
            }
            else
            {
                model.Uplate = db.Uplate.Include(p => p.Projekt).Include(k => k.Klijent).Include(c => c.ClanUprave).ThenInclude(o => o.Osoba).ToList();
            }

            return View(model);
        }

        public IActionResult Pretraga(string srchTxt)
        {
            if (srchTxt == null)
                return RedirectToAction(nameof(Index));

            UplateIndexViewModel model = new UplateIndexViewModel();

            model.Uplate = db.Uplate.Include(p => p.Projekt).Include(k => k.Klijent).Include(c => c.ClanUprave).ThenInclude(o => o.Osoba).Where(x => x.Projekt.Naziv.Contains(srchTxt)).ToList();

            if (!model.Uplate.Any() )
            {
                model.Uplate = db.Uplate.Include(p => p.Projekt).Include(k => k.Klijent).Include(c => c.ClanUprave).ThenInclude(o => o.Osoba).Where(x => x.UplataID.ToString().Equals(srchTxt)).ToList();
            }

            model.srchTxt = srchTxt;

            return View("Index", model);
        }

    


        public IActionResult Dodaj()
        {
            return View(GetDefaultViewModel(new UplateDodajViewModel()));
        }


        [HttpPost]
        public IActionResult Dodaj(UplateDodajViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(GetDefaultViewModel(model));
            }

            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            Uplata uplata = model.Uplata;

            uplata.KlijentID = model.KlijentID;
            uplata.ProjektID = model.ProjektID;

            uplata.ClanUpraveID = korisnik.KorisnikID;


            db.Uplate.Add(uplata);
            db.SaveChanges();


            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, httpContext.HttpContext.Connection.RemoteIpAddress.ToString(), httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Dodavanje uplate", "Uplate");



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalji(int UplataID)
        {
            UplateDetaljiViewModel model = new UplateDetaljiViewModel();

            model.uplata = db.Uplate.Include(k => k.Klijent).Include(p => p.Projekt).Include(c => c.ClanUprave).ThenInclude(o => o.Osoba).Where(i => i.UplataID == UplataID).FirstOrDefault();


            return PartialView(model);
        }


        public IActionResult Obrisi(int id)
        {
            Uplata x = db.Uplate.Where(y => y.UplataID == id).FirstOrDefault();

            db.Uplate.Remove(x);
            db.SaveChanges();


            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, httpContext.HttpContext.Connection.RemoteIpAddress.ToString(), httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Brisanje uplate", "Uplate");


            return RedirectToAction(nameof(Index));
        }


        public IActionResult Uredi(int UplataId)
        {
            Uplata uplata = db.Uplate.Where(u => u.UplataID == UplataId).FirstOrDefault();

            UplateDodajViewModel model = GetDefaultViewModel(new UplateDodajViewModel()
            {
                Uplata = uplata,
                ProjektID = uplata.ProjektID,
                KlijentID = uplata.KlijentID
            });


            return View(model);
        }

        [HttpPost]
        public IActionResult Uredi(UplateDodajViewModel model)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(model));

            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            Uplata x = model.Uplata;

            x.ProjektID = model.ProjektID;
            x.KlijentID = model.KlijentID;
            x.ClanUpraveID = korisnik.KorisnikID;

            db.Uplate.Update(x);
            db.SaveChanges();


            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, httpContext.HttpContext.Connection.RemoteIpAddress.ToString(), httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Uredjivanje uplate", "Uplate");


            return RedirectToAction(nameof(Detalji), new { UplataID = x.UplataID });
        }




        private UplateDodajViewModel GetDefaultViewModel(UplateDodajViewModel model)
        {

            model.Uplata = model.Uplata ?? new Uplata();
            model.Klijenti = model.Klijenti ?? db.Klijenti.Select(g => new SelectListItem { Value = g.KlijentID.ToString(), Text = g.KontaktOsoba }).ToList();
            model.Projekti = model.Projekti ?? db.Projekti.Select(s => new SelectListItem { Value = s.ProjektID.ToString(), Text = s.Naziv }).ToList();

            return model;
        }







    }
}