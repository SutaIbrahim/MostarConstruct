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

namespace MostarConstruct.Web.Areas.Poslovodja.Controllers
{
    [Autorizacija(false, TipKorisnika.Poslovodja)]

    [Area("Poslovodja")]

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


        //
        private RadilistaDodajViewModel GetDefaultViewModel(RadilistaDodajViewModel model)
        {

            model.Radiliste = model.Radiliste ?? new Radiliste();
            model.Gradovi = model.Gradovi ?? db.Gradovi.Select(g => new SelectListItem { Value = g.GradID.ToString(), Text = g.Naziv }).ToList();
            model.Projekti= model.Projekti ?? db.Projekti.Select(s => new SelectListItem { Value = s.ProjektID.ToString(), Text = s.Naziv }).ToList();

            return model;
        }


        public IActionResult Obrisi(int id)
        {
            Radiliste x = db.Radilista.Where(y => y.RadilisteID == id).FirstOrDefault();

            db.Radilista.Remove(x);

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}