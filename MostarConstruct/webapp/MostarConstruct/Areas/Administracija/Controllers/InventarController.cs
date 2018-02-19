using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Areas.Administracija.ViewModels;
using MostarConstruct.Web.Helper;
using MostarConstruct.Web.Helper.IHelper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{
    [Autorizacija(false, TipKorisnika.Administrator)]
    [Area("Administracija")]
    public class InventarController : Controller
    {
        private DatabaseContext db;
        private IDropdown dropdown;
        public int PageSize = 4;

        public InventarController(DatabaseContext db, IDropdown dropdown)
        {
            this.db = db;
            this.dropdown = dropdown;
        }

        public IActionResult Kategorije()
        {
            return View(db.Kategorije.ToList());
        }

        #region Index
        public IActionResult Index(int page = 1)
        {
            InventarIndexViewModel vm = new InventarIndexViewModel()
            {
                Rows = db.Inventar.Include(x => x.Kategorija).Select(x => new InventarIndexViewModel.Row()
                {
                    InventarID = x.InventarID,
                    DatumKupovine = x.DatumKupovine,
                    Ispravan = x.Ispravno == true ? "Da" : "Ne",
                    Zauzet = x.Zauzeto == true ? "Da" : "Ne",
                    Kategorija = x.Kategorija.Naziv,
                    Naziv = x.Naziv,
                    SerijskiBroj = x.SerijskiBroj
                }).OrderBy(x => x.InventarID).Skip((page - 1) * PageSize).Take(PageSize).ToList(),
                PagingInfo = new Web.ViewModels.PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Inventar.Count()
                }
            };

            return View(vm);
        }
        #endregion

        #region Dodaj
        public IActionResult Dodaj()
        {
            return View(GetDefaultViewModel(new InventarDodajViewModel()));
        }

        [HttpPost]
        public IActionResult Dodaj(InventarDodajViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(viewModel));

            Inventar inventar = viewModel.Inventar;
            inventar.Ispravno = true;
            inventar.Zauzeto = false;

            db.Inventar.Add(inventar);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Uredi
        public IActionResult Uredi(int inventarID)
        {
            InventarDodajViewModel viewModel = GetDefaultViewModel(new InventarDodajViewModel());
            viewModel.Inventar = db.Inventar.FirstOrDefault(x => x.InventarID == inventarID);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Uredi(InventarDodajViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(GetDefaultViewModel(viewModel));

            Inventar inventar = viewModel.Inventar;

            db.Inventar.Update(inventar);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Obrisi
        [HttpPost]
        public IActionResult Obrisi(int inventarID)
        {
            db.Inventar.Remove(db.Inventar.Find(inventarID));
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper
        private InventarDodajViewModel GetDefaultViewModel(InventarDodajViewModel viewModel)
        {
            viewModel.Inventar = viewModel.Inventar ?? new Models.Inventar();
            viewModel.Kategorije = viewModel.Kategorije ?? dropdown.Kategorije().ToList();

            return viewModel;
        }
        #endregion
    }
}