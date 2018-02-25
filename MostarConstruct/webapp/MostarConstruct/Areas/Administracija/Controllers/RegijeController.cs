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
        public int PageSize = 4;

        public RegijeController(DatabaseContext db, IDropdown dd, IHttpContextAccessor httpContext)
        {
            this.db = db;
            this.dd = dd;
            this.httpContext = httpContext;
        }

        #endregion

      

        #region Index
        public IActionResult Index(int page = 1)
        {
            RegijeIndexViewModel vm = new RegijeIndexViewModel()
            {
                Rows = db.Regije.Select(x => new RegijeIndexViewModel.Row()
                {
                    RegijaID = x.RegijaID,
                    Drzava = x.Drzava.Naziv,
                    Naziv = x.Naziv,
                    Oznaka = x.Oznaka
                }).OrderBy(x => x.RegijaID).Skip((page - 1) * PageSize).Take(PageSize).ToList(),
                PagingInfo = new Web.ViewModels.PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Regije.Count()
                }
            };

            return View(vm);
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

            return RedirectToAction(nameof(Index), new { page = 1});
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
            db.SaveChanges();
            return RedirectToAction(nameof(Index), new { page = 1 });
        }
        #endregion

        #region Obrisi
        [HttpPost]
        public IActionResult Obrisi(int id)
        {
            db.Regije.Remove(db.Regije.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index), new { page = 1 });
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

            viewModel.Rows = db.Regije.Select(x => new RegijeIndexViewModel.Row()
            {
                RegijaID = x.RegijaID,
                Drzava = x.Drzava.Naziv,
                Naziv = x.Naziv,
                Oznaka = x.Oznaka
            }).ToList();

            return viewModel;
        }
        #endregion


    }
}