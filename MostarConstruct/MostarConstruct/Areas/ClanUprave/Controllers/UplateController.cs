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

            var model = db.Uplate.Include(p => p.Projekt).Include(k => k.Klijent).Include(c => c.ClanUprave).ThenInclude(o => o.Osoba);

            return View(model);
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

            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            Uplata uplata = model.Uplata;

            uplata.KlijentID = model.KlijentID;
            uplata.ProjektID = model.ProjektID;

            uplata.ClanUpraveID = k.KorisnikID;


            db.Uplate.Add(uplata);
            db.SaveChanges();


            return RedirectToAction(nameof(Index));
        }


        private UplateDodajViewModel GetDefaultViewModel(UplateDodajViewModel model)
        {

            model.Uplata = model.Uplata ?? new Uplata();
            model.Klijenti = model.Klijenti ?? db.Klijenti.Select(g => new SelectListItem { Value =g.KlijentID.ToString(), Text = g.KontaktOsoba }).ToList();
            model.Projekti = model.Projekti ?? db.Projekti.Select(s => new SelectListItem { Value = s.ProjektID.ToString(), Text = s.Naziv }).ToList();

            return model;
        }



        public IActionResult Obrisi(int id)
        {
            Uplata x = db.Uplate.Where(y => y.UplataID == id).FirstOrDefault();

            db.Uplate.Remove(x);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



    }
}