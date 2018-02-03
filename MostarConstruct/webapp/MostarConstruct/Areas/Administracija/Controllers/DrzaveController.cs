using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{
    [Area(nameof(Administracija))]
    [Autorizacija(false, TipKorisnika.Administrator)]
    public class DrzaveController : Controller
    {
        #region DI
        private DatabaseContext db;

        public DrzaveController(DatabaseContext db)
        {
            this.db = db;
        }
        #endregion

        #region Index
        public IActionResult Index() => View(db.Drzave);
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
            {
                return View(drzava);
                //return Json(new { success = false, errors = messages });
            }

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


        #endregion


        #region Delete

        #endregion
    }
}