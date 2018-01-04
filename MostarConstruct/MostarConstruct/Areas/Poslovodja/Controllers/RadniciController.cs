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

namespace MostarConstruct.Web.Areas.Poslovodja.Controllers
{

    [Autorizacija(false, TipKorisnika.Poslovodja)]

    [Area("Poslovodja")]


    public class RadniciController : Controller
    {
        private DatabaseContext db;
        private IDropdown dropdown;

        public RadniciController(DatabaseContext db, IDropdown dropdown)
        {
            this.db = db;
            this.dropdown = dropdown;

        }


        #region Index
        public IActionResult Index()
        {
            var model = db.Radnici.Include(p => p.Pozicija).Include(o => o.Osoba).ThenInclude(g => g.Grad);
            return View(model);
        }
        #endregion

        #region Dodaj
        public IActionResult Dodaj()
        {
            return View(GetDefaultViewModel(new RadniciDodajViewModel()));
        }

        [HttpPost]
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

            return RedirectToAction(nameof(Index));
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