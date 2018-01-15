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

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{

    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]
    public class PonudaController : Controller
    {

        private DatabaseContext db;
        private IHttpContextAccessor httpContext;

        public PonudaController(DatabaseContext db, IHttpContextAccessor httpContext)
        {
            this.db = db;
            this.httpContext = httpContext;
        }


        public IActionResult Index()
        {
            var model = db.Ponude.Include(k => k.ClanUprave).ThenInclude(o => o.Osoba);

            return View(model);
        }

        public IActionResult Izvjestaj(int PonudaId)
        {
            if (PonudaId !=0)
            {
                var model = db.Ponude.Include(k => k.ClanUprave).ThenInclude(o => o.Osoba).Where(p=>p.PonudaID==PonudaId);
                return View("IzvjestajPonuda", model);

            }
            else
            {
                var model = db.Ponude.Include(k => k.ClanUprave).ThenInclude(o => o.Osoba);
                return View("IzvjestajPonuda", model);

            }
        }

        public IActionResult Dodaj()
        {
            return View(new Ponuda()); // - za standardni view...

            //return View("_Dodaj", new Ponuda());
        }


        [HttpPost]
        public IActionResult Dodaj(Ponuda _ponuda)
        {
            if (!ModelState.IsValid)
            {
                return View(_ponuda);
            }

            Ponuda ponuda = _ponuda;
            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            ponuda.DatumIzdavanja= DateTime.Now;
            ponuda.ClanUpraveID = korisnik.KorisnikID;

            db.Ponude.Add(ponuda);
            db.SaveChanges();


            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, httpContext.HttpContext.Connection.RemoteIpAddress.ToString(), httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Dodavanje ponude", "Ponuda");


            return RedirectToAction(nameof(Index));
        }

       
        public IActionResult Obrisi(int id)
        {
            Ponuda x = db.Ponude.Where(y => y.PonudaID == id).FirstOrDefault();

            db.Ponude.Remove(x);

            db.SaveChanges();

            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);


            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, httpContext.HttpContext.Connection.RemoteIpAddress.ToString(), httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Brisanje ponude", "Ponuda");



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Uredi(int PonudaId)
        {
            Ponuda model = db.Ponude.Where(p => p.PonudaID == PonudaId).FirstOrDefault();

            return View(model);
        }

    

        [HttpPost]
        public IActionResult Uredi(Ponuda model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Ponuda ponuda = model;
            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            ponuda.ClanUpraveID = korisnik.KorisnikID;
            ponuda.DatumIzdavanja = DateTime.Now;

            db.Ponude.Update(ponuda);
            db.SaveChanges();



            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, httpContext.HttpContext.Connection.RemoteIpAddress.ToString(), httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Uredjivanje ponude", "Ponuda");

            return RedirectToAction(nameof(Index));
        }


    }
}