using Fiver.Mvc.FileUpload.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
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
        public ProjektiController(DatabaseContext db, IHttpContextAccessor context)
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
                return View("Dodaj", model);
            }
            Projekt novi;
            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            if (model.projekt.ProjektID == 0)
            {
                novi = new Projekt();
                novi.ClanUprave = new Korisnik();
                novi = model.projekt;
                novi.ClanUpraveID = korisnik.KorisnikID;
                novi.Boja = Konfiguracija.Boje.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                _db.Projekti.Add(novi);
                _db.SaveChanges();
            }
            else
            {
                novi=_db.Projekti.Where(x=>x.ProjektID==model.projekt.ProjektID).FirstOrDefault();
                int clan = novi.ClanUpraveID;
                novi.BrojRata = model.projekt.BrojRata;
                novi.Cijena = model.projekt.Cijena;
                novi.ClanUpraveID = clan;
                novi.Naziv = model.projekt.Naziv;
                novi.Opis = model.projekt.Opis;
                novi.PredlozeniPocetak = model.projekt.PredlozeniPocetak;
                novi.PredlozeniZavrsetak = model.projekt.PredlozeniZavrsetak;
                novi.StvarniPocetak = model.projekt.StvarniPocetak;
                novi.StvarniZavrsetak = model.projekt.StvarniZavrsetak;
                novi.Zavrsen = model.projekt.Zavrsen;
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
        public IActionResult Detalji(int ProjektID,int ? greska)
        {
            ProjektiDetaljiViewModel Model = new ProjektiDetaljiViewModel();
            Model.projekt = new Projekt();
            Model.projekt.ClanUprave = new Korisnik();
            Model.listaFajlova = new List<Fajl>();
            if(greska!=null)
            {
                ViewData["greska"] = greska;
            }

            Model.projekt = _db.Projekti.Where(x => x.ProjektID == ProjektID).FirstOrDefault();
            Model.listaFajlova = _db.ProjektiFajlovi.Where(x => x.ProjektID == ProjektID).Select(x => new Fajl
            {
                FajlId = x.FajlID,
                DatumDodavanja = x.Fajl.DatumDodavanja,
                Naziv = x.Fajl.Naziv,
                Podaci = x.Fajl.Podaci,
                Tip = x.Fajl.Tip
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
        public IActionResult UploadFajl(IFormFile dokument, int projektid)
        {
            Fajl noviFajl = new Fajl();
            if (dokument.Length > 1000000)
            {
                return RedirectToAction("Detalji", new { ProjektID = projektid, greska=1 });
            }
            else
            {
                noviFajl.DatumDodavanja = DateTime.Now;
                noviFajl.Naziv = dokument.FileName;
                noviFajl.Tip = dokument.ContentType;
                using (MemoryStream ms = new MemoryStream())
                {
                    dokument.CopyTo(ms);
                    noviFajl.Podaci = ms.ToArray();
                }
                _db.Fajlovi.Add(noviFajl);
                _db.SaveChanges();

                ProjektiFajlovi novaStavka = new ProjektiFajlovi();
                novaStavka.FajlID = noviFajl.FajlId;
                novaStavka.ProjektID = projektid;
                _db.ProjektiFajlovi.Add(novaStavka);
                _db.SaveChanges();

            }
            return RedirectToAction("Detalji", new { ProjektID = projektid, greska = 0 });

        }
        public IActionResult DownloadFajl(int fajlid)
        {
            Fajl zaDownload = new Fajl();
            zaDownload = _db.Fajlovi.Where(x => x.FajlId == fajlid).FirstOrDefault();
            return File(zaDownload.Podaci, zaDownload.Tip, zaDownload.Naziv);
        }
    }
}
