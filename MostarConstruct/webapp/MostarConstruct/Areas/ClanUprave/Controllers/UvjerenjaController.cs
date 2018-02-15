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
using MostarConstruct.Models;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using MostarConstruct.Web.Areas.ClanUprave.Helpers;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]
    public class UvjerenjaController : Controller
    {
        private DatabaseContext _db;
        private IHttpContextAccessor _context;
        public UvjerenjaController(DatabaseContext db,IHttpContextAccessor context)
        {
            _db = db;
            _context = context;
        }
        public IActionResult Index()
        {
            UvjerenjaIndexVM Model = new UvjerenjaIndexVM();
            Model.listaUvjerenja = new List<UvjerenjaIndexVM.Row>();
            Model.listaUvjerenja = _db.Uvjerenja.Select(x => new UvjerenjaIndexVM.Row
            {
                UvjerenjeId = x.UvjerenjeID,
                BrojProtokola = x.BrojProtokola,
                DatumIzdavanja = x.DatumIzdavanja,
                Radnik = x.Radnik.Osoba.Ime + " " + x.Radnik.Osoba.Prezime
            }).ToList();
            return View(Model);
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
        public IActionResult Snimi(UvjerenjaDodajVM model)
        {
            Korisnik korisnik = _context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            Uvjerenje novo = new Uvjerenje
            {
                BrojProtokola = _db.Uvjerenja.Count().ToString() + "/" + (100 + _db.Uvjerenja.Count()).ToString(),
                DatumIzdavanja = DateTime.Now,
                RadnikID = model.RadnikId,
                Napomena = model.Napomena,
                Svrha = model.Svrha,
                ClanUpraveID = korisnik.KorisnikID
            };
            _db.Uvjerenja.Add(novo);
            _db.SaveChanges();
            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(_db);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, _context.HttpContext.Connection.RemoteIpAddress.ToString(), _context.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Dodavanje uvjerenja", "Uvjerenja");
            return RedirectToAction("Index");
        }
        public IActionResult Detalji(int UvjerenjeId)
        {
            UvjerenjaDetaljiVM Model = new UvjerenjaDetaljiVM();
            Model.uvjerenje = new Uvjerenje();
            Model.uvjerenje = _db.Uvjerenja.Where(x => x.UvjerenjeID == UvjerenjeId).FirstOrDefault();
            Model.listaRadnika = new List<SelectListItem>();
            Model.listaRadnika = _db.Radnici.Select(x => new SelectListItem
            {
                Value = x.RadnikID.ToString(),
                Text = x.Osoba.Ime + " " + x.Osoba.Prezime
            }).ToList();
            return View(Model);
        }
        public IActionResult SnimiUvjerenje(UvjerenjaDetaljiVM model)
        {
            Uvjerenje dbUvjerenje = _db.Uvjerenja.Where(x => x.UvjerenjeID == model.uvjerenje.UvjerenjeID).FirstOrDefault();
            dbUvjerenje.BrojProtokola = model.uvjerenje.BrojProtokola;
            dbUvjerenje.ClanUpraveID = model.uvjerenje.ClanUpraveID;
            dbUvjerenje.DatumIzdavanja = model.uvjerenje.DatumIzdavanja;
            dbUvjerenje.Napomena = model.uvjerenje.Napomena;
            dbUvjerenje.RadnikID = model.uvjerenje.RadnikID;
            dbUvjerenje.Svrha = model.uvjerenje.Svrha;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult PrikaziDokument(int UvjerenjeId)
        {
            UvjerenjaReport dokument = new UvjerenjaReport(_db);
            byte[] dokumentBytes = dokument.PrepareReport(_db.Uvjerenja.Where(x => x.UvjerenjeID == UvjerenjeId).FirstOrDefault());
            return File(dokumentBytes, "application/pdf");
        }
    }
}