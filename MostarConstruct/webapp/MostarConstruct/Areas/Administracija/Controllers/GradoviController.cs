using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Areas.Administracija.ViewModels;
using MostarConstruct.Web.Helper;
using MostarConstruct.Web.Helper.IHelper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{

    [Area(nameof(Administracija))]
    [Autorizacija(false, TipKorisnika.Administrator)]
    public class GradoviController : Controller
    {
        #region DI
        private DatabaseContext db;
        private IDropdown dd;
        public int PageSize = 4;

        public GradoviController(DatabaseContext db, IDropdown dd)
        {
            this.db = db;
            this.dd = dd;
        }
        #endregion

        #region Index
        public IActionResult Index(int page = 1)
        {
            GradoviIndexViewModel vm = new GradoviIndexViewModel()
            {
                Rows = db.Gradovi.Select(x => new GradoviIndexViewModel.Row()
                {
                    GradId = x.GradID,
                    Drzava = x.Regija.Drzava.Naziv,
                    Naziv = x.Naziv,
                    Regija = x.Regija.Naziv,
                    PostanskiBroj = x.PostanskiBroj
                }).OrderBy(x => x.GradId).Skip((page - 1) * PageSize).Take(PageSize).ToList(),
                PagingInfo = new Web.ViewModels.PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Gradovi.Count()
                }
            };

            return View(vm);
        }
        #endregion

        #region Add
        public IActionResult Dodaj() => PartialView("_Dodaj", GetDefaultViewModel(new GradoviDodajViewModel()));

        [HttpPost]
        public IActionResult Dodaj(GradoviDodajViewModel vm)
        {
            //if (!ModelState.IsValid)
            //{              
            //    return PartialView("_Dodaj", GetDefaultViewModel(vm));
            //}

            db.Gradovi.Add(vm.Grad);
            db.SaveChanges();

            return Json(new { success = true });
        }

        #endregion

        #region Edit
        public IActionResult Uredi(int gradId) => PartialView("_Uredi", GetDefaultViewModel(new GradoviUrediViewModel() { Grad = db.Gradovi.Find(gradId) }));

        [HttpPost]
        public IActionResult Uredi(GradoviUrediViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_Uredi", GetDefaultViewModel(vm));

            db.Gradovi.Update(vm.Grad);
            db.SaveChanges();

            return Json(new { success = true });
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Obrisi(int gradId)
        {
            db.Gradovi.Remove(db.Gradovi.Find(gradId));
            db.SaveChanges();
            return RedirectToAction(nameof(Index), new { page = 1 });
        }
        #endregion

        #region Helpers
        private GradoviDodajViewModel GetDefaultViewModel(GradoviDodajViewModel vm)
        {
            vm.Grad = vm.Grad ?? new Grad();
            vm.Regije = vm.Regije ?? dd.Regije().ToList();

            return vm;
        }

        private GradoviUrediViewModel GetDefaultViewModel(GradoviUrediViewModel vm)
        {
            vm.Grad = vm.Grad ?? new Grad();
            vm.Regije = vm.Regije ?? dd.Regije().ToList();

            return vm;
        }

        public JsonResult GetGradoviByRegijaId(int regijaId)
        {
            List<SelectListItem> gradovi = dd.Gradovi(regijaId).ToList();
            return Json(gradovi);
        }
        #endregion
    }
}