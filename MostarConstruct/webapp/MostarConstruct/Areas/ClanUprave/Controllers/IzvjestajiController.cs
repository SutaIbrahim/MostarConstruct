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
using MostarConstruct.Web.Helper.IHelper;
using Microsoft.AspNetCore.Http;
using MostarConstruct.Web.ViewModels;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]


    public class IzvjestajiController : Controller
    {

        private DatabaseContext db;
        IHttpContextAccessor httpContext;
        public int PageSize = 10;


        public IzvjestajiController(DatabaseContext db, IHttpContextAccessor httpContext)
        {
            this.db = db;
            this.httpContext = httpContext;

        }



        public IActionResult Index(int page = 1)
        {
            IzvjestajIndexViewModel model = new IzvjestajIndexViewModel();

            model.izvjestaji = db.Izvjestaji.Include(x => x.Korisnik).ThenInclude(y => y.Osoba).Include(p => p.Projekt).Skip((page-1)*PageSize).Take(PageSize).ToList();

            model.PagingInfo = new Web.ViewModels.PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = db.Izvjestaji.Count()
            };


            return View(model);
        }
        public IActionResult Pretraga(string srchTxt)
        {
            if (srchTxt == null)
                return RedirectToAction(nameof(Index));


            IzvjestajIndexViewModel model = new IzvjestajIndexViewModel();
            model.izvjestaji = db.Izvjestaji.Include(x => x.Korisnik).ThenInclude(y => y.Osoba).Include(p => p.Projekt).Where(y => y.Projekt.Naziv.StartsWith(srchTxt)).ToList();
            model.srchTxt = srchTxt;

            return View("Index", model);
        }


        public IActionResult Dodaj()
        {
            IzvjestajDodajVIewModel model = new IzvjestajDodajVIewModel();

            model.projekti = db.Projekti.Select(x => new SelectListItem
            {
                Value = x.ProjektID.ToString(),
                Text = x.Naziv

            }).ToList();

            model.izvjestaj = new Izvjestaj();
            model.izvjestaj.DatumIzdavanja = DateTime.Now;


            return View(model);
        }

        [HttpPost]
        public IActionResult Kreiraj(IzvjestajDodajVIewModel model)
        {
            if (!ModelState.IsValid)
            {
                IzvjestajDodajVIewModel mdl = new IzvjestajDodajVIewModel()
                {

                    projekti = db.Projekti.Select(x => new SelectListItem
                    {
                        Value = x.ProjektID.ToString(),
                        Text = x.Naziv

                    }).ToList(),

                    izvjestaj = model.izvjestaj
                };


                return View("Dodaj", mdl);
            }

            Izvjestaj novi = model.izvjestaj;

            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            novi.KorisnikID = korisnik.KorisnikID;

            db.Izvjestaji.Add(novi);

            db.SaveChanges();

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, httpContext.HttpContext.Connection.RemoteIpAddress.ToString(), httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Dodavanje izvjestaja", "Izvjestaji");


            return RedirectToAction("Prikazi", new { id = novi.IzvjestajID });
        }


        public IActionResult Prikazi(int id)
        {
            IzvjestajPrikaziViewModel model = new IzvjestajPrikaziViewModel();

            Izvjestaj izv = db.Izvjestaji.Include(p => p.Projekt).Where(x => x.IzvjestajID == id).FirstOrDefault();
            model.izvjestaj = izv;

            model.radilista = db.Radilista.Include(g => g.Grad).Where(x => x.ProjektID == izv.ProjektID).ToList();

            model.uplate = db.Uplate.Include(k => k.Klijent).Where(x => x.ProjektID == izv.ProjektID).ToList();

            model.BrojNaloga = db.RadniNalozi.Where(x => x.Radiliste.ProjektID == izv.ProjektID).Count();


            return View(model);
        }



    }
}