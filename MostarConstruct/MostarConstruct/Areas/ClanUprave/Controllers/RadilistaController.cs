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

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]

    public class RadilistaController : Controller
    {


        private DatabaseContext db;

        public RadilistaController(DatabaseContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Radilista.Include(p=>p.Projekt).Include(g=>g.Grad));
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


            return RedirectToAction(nameof(Index));
        }


        public IActionResult Obrisi(int id)
        {
            Radiliste x = db.Radilista.Where(y => y.RadilisteID == id).FirstOrDefault();

            db.Radilista.Remove(x);

            db.SaveChanges();
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