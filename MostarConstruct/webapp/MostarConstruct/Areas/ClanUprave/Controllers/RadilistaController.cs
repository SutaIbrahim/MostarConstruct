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
using MostarConstruct.Web.Areas.ClanUprave.ViewModels;
using Microsoft.AspNetCore.Http;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]

    public class RadilistaController : Controller
    {


        private DatabaseContext db;
        IHttpContextAccessor context;


        public RadilistaController(DatabaseContext db, IHttpContextAccessor context)
        {
            this.db = db;
            this.context = context;
            
        }

        public IActionResult Index()
        {
            RadilistaIndexViewModel model = new RadilistaIndexViewModel();
            model.Radilista = db.Radilista.Include(p => p.Projekt).Include(g => g.Grad).ToList();

            return View(model);
        }


        public IActionResult Pretraga(string srchTxt)
        {
            if (srchTxt == null)
                return RedirectToAction(nameof(Index));


            RadilistaIndexViewModel model = new RadilistaIndexViewModel();
               model.Radilista = db.Radilista.Include(p => p.Projekt).Include(g => g.Grad).Where(y => y.Naziv.StartsWith(srchTxt)).ToList();
            model.srchTxt = srchTxt;

            return View("Index",model);
        }


        public IActionResult Dodaj()
        {
            return View(GetDefaultViewModel(new RadilistaDodajViewModel()));
        }


        [HttpPost]
        public IActionResult Dodaj(RadilistaDodajViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(GetDefaultViewModel(model));
            }

            Radiliste radiliste = model.Radiliste;

            radiliste.ProjektID = model.ProjektID;
            radiliste.GradID = model.GradID;
        

            db.Radilista.Add(radiliste);
            db.SaveChanges();

            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            try
            {
                logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Dodavanje radilista", "Radilista");

            }
            catch(Exception e)
            {
                logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), "Mozzila Firefox","Dodavanje radilista!","Radilista");

            }



            return RedirectToAction(nameof(Index));
        }


        public IActionResult Obrisi(int id)
        {
            Radiliste x = db.Radilista.Where(y => y.RadilisteID == id).FirstOrDefault();

            db.Radilista.Remove(x);

            db.SaveChanges();

            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Brisanje radilista", "Radilista");


            return RedirectToAction(nameof(Index));
        }


        #region Uredi
        public IActionResult Uredi(int RadilisteId)
        {
            Radiliste radiliste = db.Radilista.Where(r => r.RadilisteID == RadilisteId).SingleOrDefault();
            


            RadilistaDodajViewModel vm = GetDefaultViewModel(new RadilistaDodajViewModel()
            {
              Radiliste=radiliste,
              ProjektID=radiliste.ProjektID,
              GradID=radiliste.GradID

            });

            return View(vm);
        }


        [HttpPost]
        public IActionResult Uredi(RadilistaDodajViewModel model)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(model));


            Radiliste r = model.Radiliste;
            r.ProjektID = model.ProjektID;
            r.GradID = model.GradID;

            db.Radilista.Update(r);
            db.SaveChanges();

            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Uredjivanje radilista", "Radilista");



            return RedirectToAction(nameof(Index));
        }

        #endregion



        //
        private RadilistaDodajViewModel GetDefaultViewModel(RadilistaDodajViewModel model)
        {

            model.Radiliste = model.Radiliste ?? new Radiliste();
            model.Gradovi = model.Gradovi ?? db.Gradovi.Select(g => new SelectListItem { Value = g.GradID.ToString(), Text = g.Naziv }).ToList();
            model.Projekti= model.Projekti ?? db.Projekti.Select(s => new SelectListItem { Value = s.ProjektID.ToString(), Text = s.Naziv }).ToList();

            return model;
        }


     

    }
}