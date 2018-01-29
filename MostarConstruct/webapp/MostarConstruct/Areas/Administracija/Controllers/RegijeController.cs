using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MostarConstruct.Data;
using MostarConstruct.Web.Areas.Administracija.ViewModels;
using MostarConstruct.Web.Helper;
using MostarConstruct.Web.Helper.IHelper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{
    [Autorizacija(false, TipKorisnika.Administrator)]
    [Area(nameof(Administracija))]
    public class RegijeController : Controller
    {
        #region DI
        private DatabaseContext db;
        private IHttpContextAccessor httpContext;
        private IDropdown dd;

        public RegijeController(DatabaseContext db, IDropdown dd, IHttpContextAccessor httpContext)
        {
            this.db = db;
            this.dd = dd;
            this.httpContext = httpContext;
        }

        #endregion

        private RegijeIndexViewModel SessionRegijeIndex
        {
            get { return httpContext.HttpContext.Session.GetJson<RegijeIndexViewModel>(Konfiguracija.Sesija1); }
            set { httpContext.HttpContext.Session.SetJson(Konfiguracija.Sesija1, value); }
        }

        #region Index
        public IActionResult Index()
        {
            if(SessionRegijeIndex != null)
            {
                return View(GetDefaultViewModel(new RegijeIndexViewModel()));
            }
            return View(GetDefaultViewModel(new RegijeIndexViewModel()));
        }

        [HttpPost]
        public IActionResult Index(RegijeIndexViewModel viewModel)
        {
            SessionRegijeIndex = viewModel;

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Dodaj
        public IActionResult Dodaj() => View(GetDefaultViewModel(new RegijeDodajViewModel()));

        [HttpPost]
        public IActionResult Dodaj(RegijeDodajViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(viewModel));

            db.Regije.Add(viewModel.Regija);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Uredi
        public IActionResult Uredi(int id) => View(GetDefaultViewModel(new RegijeDodajViewModel() { Regija = db.Regije.FirstOrDefault(x => x.RegijaID == id) }));

        [HttpPost]
        public IActionResult Uredi(RegijeDodajViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(viewModel));

            db.Regije.Update(viewModel.Regija);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Obrisi
        [HttpPost]
        public IActionResult Obrisi(int id)
        {
            db.Regije.Remove(db.Regije.Find(id));
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers

        public JsonResult GetRegijeByDrzavaId(int drzavaId)
        {
            List<SelectListItem> regije = dd.Regije(drzavaId).ToList();
            return Json(regije);
        }


        private RegijeDodajViewModel GetDefaultViewModel(RegijeDodajViewModel viewModel)
        {
            viewModel.Regija = viewModel.Regija ?? new Models.Regija();
            viewModel.Drzave = viewModel.Drzave ?? dd.Drzave().ToList();

            return viewModel;
        }

        private RegijeIndexViewModel GetDefaultViewModel(RegijeIndexViewModel viewModel)
        {
            viewModel.BrojRezultata = 5;
            viewModel.UkupnoRezultata = dd.Rezultati().ToList();

            viewModel.Rows = db.Regije.Select(x => new RegijeIndexViewModel.Row()
            {
                RegijaID = x.RegijaID,
                Drzava = x.Drzava.Naziv,
                Naziv = x.Naziv,
                Oznaka = x.Oznaka
            }).Take(viewModel.BrojRezultata).ToList();

            return viewModel;
        }
        #endregion
    }
}