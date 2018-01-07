using Fiver.Mvc.FileUpload.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using MostarConstruct.Data;
using MostarConstruct.Data.Models;
using MostarConstruct.Models;
using MostarConstruct.Web.Areas.ClanUprave.ViewModels;
using MostarConstruct.Web.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]
    public class ProjektiController : Controller
    {
        DatabaseContext _db;
        IHttpContextAccessor context;
        public ProjektiController(DatabaseContext db,IHttpContextAccessor context)
        {
            _db = db;
            this.context = context;
        }
        public IActionResult Index()
        {
            ProjektiIndexViewModel Model = new ProjektiIndexViewModel();
            Model.listaProjekata = new List<ProjektiIndexViewModel.Row>();
            Model.listaProjekata = _db.Projekti.Select(x => new ProjektiIndexViewModel.Row
            {
                Id = x.ProjektID,
                BrojRata = x.BrojRata.ToString(),
                Cijena = x.Cijena.ToString(),
                Naziv = x.Naziv,
                stvarniPocetak = x.StvarniPocetak.ToString(),
                stvarniZavrsetak = x.StvarniZavrsetak.ToString(),
                zavrsen = x.Zavrsen ? "DA" : "NE"
            }).ToList();
            return View(Model);
        }
        public IActionResult Dodaj()
        {
            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            ProjektiDodajViewModel Model = new ProjektiDodajViewModel();
            Model.projekt = new Projekt();
            return View(Model);

            
        }
        public IActionResult Snimi(ProjektiDodajViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj",model);
            }
            Projekt novi;
            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            if (model.projekt.ProjektID == 0)
            {
                novi = new Projekt();
                novi.ClanUprave = new Korisnik();
                novi = model.projekt;
                novi.ClanUpraveID = korisnik.KorisnikID;
                _db.Projekti.Add(novi);
                _db.SaveChanges();
            }
            else
            {
                _db.Projekti.Update(model.projekt);
                _db.SaveChanges();
            }
            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(_db);
            Korisnik k = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Dodavanje/Uredjivanje projekata", "Projekti");

            return RedirectToAction("Index");
        }
        public IActionResult Uredi(int ProjektID)
        {
            ProjektiDodajViewModel Model = new ProjektiDodajViewModel();
            Model.projekt = new Projekt();
            Model.projekt = _db.Projekti.Where(x => x.ProjektID == ProjektID).FirstOrDefault();
            return View("Dodaj", Model);
        }
        public IActionResult Detalji(int ProjektID)
        {
            ProjektiDetaljiViewModel Model = new ProjektiDetaljiViewModel();
            Model.projekt = new Projekt();
            Model.projekt.ClanUprave = new Korisnik();
            Model.listaFajlova = new List<Fajl>();

            Model.projekt = _db.Projekti.Where(x => x.ProjektID == ProjektID).FirstOrDefault();
            Model.listaFajlova = _db.ProjektiFajlovi.Where(x=>x.ProjektID==ProjektID).Select(x => new Fajl
            {
                FajlId = x.FajlID,
                DatumDodavanja = x.Fajl.DatumDodavanja,
                Lokacija = x.Fajl.Lokacija,
                Naziv = x.Fajl.Naziv
            }).ToList();
          

            return View(Model);
        }
        public IActionResult Obrisi(int ProjektID)
        {
            Projekt p = new Projekt();
            p = _db.Projekti.Where(x => x.ProjektID == ProjektID).FirstOrDefault();
            _db.Projekti.Remove(p);
            _db.SaveChanges();
            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(_db);
            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, context.HttpContext.Connection.RemoteIpAddress.ToString(), context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Brisanje projekta", "Projekti");

            return RedirectToAction("Index");
        }
       
    }
}
