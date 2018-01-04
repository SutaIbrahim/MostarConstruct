using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Areas.Administracija.ViewModels;
using MostarConstruct.Web.Helper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{
    [Autorizacija(false, TipKorisnika.Administrator)]
    [Area("Administracija")]
    public class VozilaController : Controller
    {
        private DatabaseContext _db;
        public VozilaController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            VozilaIndexViewModel Model = new VozilaIndexViewModel();
            Model.listaVozila = new List<VozilaIndexViewModel.Row>();
            Model.listaVozila = _db.Vozila.Select(x => new VozilaIndexViewModel.Row
            {
                Id = x.VoziloID,
                CijenaPoSatu = x.CijenaPoSatu.ToString(),
                DatumRegistracije = x.DatumRegistracije,
                GodinaProizvodnje = x.GodinaProizvodnje.ToString(),
                Nosivost = x.Nosivost.ToString(),
                Proizvodjac = x.Proizvodjac.ToString(),
                RegistarskaOznaka = x.RegistarskaOznaka.ToString(),
                VozackaKategorija = x.VozackaKategorija.Naziv.ToString(),
                VrstaVozila = x.VrstaVozila.Naziv.ToString(),
                Zauzeto = x.Zauzeto ? "DA" : "NE"
            }).ToList();
            return View(Model);
        }
        public IActionResult Dodaj()
        {
            VozilaDodajViewModel Model = new VozilaDodajViewModel();
            Model.Vozilo = new Models.Vozilo();
            Model.vozackeKategorije = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            Model.vrsteVozila = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            Model.vozackeKategorije = _db.VozackeKategorije.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.VozackaKategorijaID.ToString(),
                Text = x.Naziv
            }).ToList();


            Model.vrsteVozila = _db.VrsteVozila.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.VrstaVozilaID.ToString(),
                Text = x.Naziv
            }).ToList();
            return View(Model);
        }
        public IActionResult Snimi(VozilaDodajViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Dodaj",model);
            Vozilo vozilo;
            if (model.Vozilo.VoziloID == 0)
            {
                vozilo = new Vozilo();
                vozilo.VrstaVozila = new VrstaVozila();
                vozilo.VozackaKategorija = new VozackaKategorija();
                vozilo = model.Vozilo;
                _db.Vozila.Add(vozilo);
                _db.SaveChanges();

            }
            else
            {
                _db.Vozila.Update(model.Vozilo);
                
                _db.SaveChanges();
            }
          


            return RedirectToAction("Index");
        }
        public IActionResult Uredi(int VoziloID)
        {
            VozilaDodajViewModel Model = new VozilaDodajViewModel();
            Model.Vozilo = _db.Vozila.Where(x => x.VoziloID == VoziloID).FirstOrDefault();

            Model.vozackeKategorije = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            Model.vrsteVozila = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            Model.vozackeKategorije = _db.VozackeKategorije.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.VozackaKategorijaID.ToString(),
                Text = x.Naziv
            }).ToList();


            Model.vrsteVozila = _db.VrsteVozila.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = x.VrstaVozilaID.ToString(),
                Text = x.Naziv
            }).ToList();

            return View("Dodaj", Model);
        }
        public IActionResult Obrisi(int VoziloID)
        {
            Vozilo v = _db.Vozila.Where(x => x.VoziloID == VoziloID).FirstOrDefault();
            _db.Vozila.Remove(v);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}