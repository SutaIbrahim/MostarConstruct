using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Areas.Administracija.ViewModels;
using MostarConstruct.Web.Helper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{
    [Area(nameof(Administracija))]
    [Autorizacija(false, TipKorisnika.Administrator)]
    public class DrzaveController : Controller
    {
        #region DI
        private DatabaseContext db;
        public int PageSize = 4;

        public DrzaveController(DatabaseContext db)
        {
            this.db = db;
        }
        #endregion

        #region Index
        public IActionResult Index(int page = 1)
        {
            DrzaveIndexViewModel vm = new DrzaveIndexViewModel()
            {
                Drzave = db.Drzave.OrderBy(x => x.DrzavaID)
                                  .Skip((page - 1) * PageSize)
                                  .Take(PageSize),
                PagingInfo = new Web.ViewModels.PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Drzave.Count()
                }
            };

            return View(vm);
        }
        #endregion

        #region Create
        public IActionResult Dodaj() => View("_Dodaj", new Drzava());

        [HttpPost]
        public IActionResult Dodaj(Drzava drzava)
        {
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            if (!ModelState.IsValid)
                return View(drzava);
            
            db.Drzave.Add(drzava);
            db.SaveChanges();

            return Json(new { success = true});
        }
        #endregion

        #region Edit
        public IActionResult Uredi(int id)
        {
            Drzava drzava = db.Drzave.Where(x => x.DrzavaID == id).FirstOrDefault();

            return PartialView("_Uredi", drzava);
        }

        [HttpPost]
        public IActionResult Uredi(Drzava drzava)
        {
            db.Drzave.Update(drzava);
            db.SaveChanges();

            return Json(new { success = true });
        }

        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Obrisi(int id)
        {
            db.Drzave.Remove(db.Drzave.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index), new { page = 1 });
        }
        #endregion
    }
}