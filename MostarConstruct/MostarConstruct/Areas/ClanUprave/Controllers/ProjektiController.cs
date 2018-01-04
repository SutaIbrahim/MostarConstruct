using Fiver.Mvc.FileUpload.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using MostarConstruct.Data;
using MostarConstruct.Data.Models;
using MostarConstruct.Models;
using MostarConstruct.Web.Areas.ClanUprave.ViewModels;
using MostarConstruct.Web.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]
    public class ProjektiController : Controller
    {
        DatabaseContext _db;
        IHttpContextAccessor context;
        public ProjektiController(DatabaseContext db,IHttpContextAccessor context)
        {
            _db = db;
            this.context = context;
        }
        public IActionResult Index()
        {
            ProjektiIndexViewModel Model = new ProjektiIndexViewModel();
            Model.listaProjekata = new List<ProjektiIndexViewModel.Row>();
            Model.listaProjekata = _db.Projekti.Select(x => new ProjektiIndexViewModel.Row
            {
                Id = x.ProjektID,
                BrojRata = x.BrojRata.ToString(),
                Cijena = x.Cijena.ToString(),
                Naziv = x.Naziv,
                stvarniPocetak = x.StvarniPocetak.ToString(),
                stvarniZavrsetak = x.StvarniZavrsetak.ToString(),
                zavrsen = x.Zavrsen ? "DA" : "NE"
            }).ToList();
            return View(Model);
        }
        public IActionResult Dodaj()
        {
            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            ProjektiDodajViewModel Model = new ProjektiDodajViewModel();
            Model.projekt = new Projekt();
            return View(Model);

            
        }
        public IActionResult Snimi(ProjektiDodajViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Projekt novi;
            Korisnik korisnik = context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            if (model.projekt.ProjektID == 0)
            {
                novi = new Projekt();
                novi.ClanUprave = new Korisnik();
                novi = model.projekt;
                novi.ClanUpraveID = korisnik.KorisnikID;
                _db.Projekti.Add(novi);
                _db.SaveChanges();
            }
            else
            {
                _db.Projekti.Update(model.projekt);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Uredi(int ProjektID)
        {
            ProjektiDodajViewModel Model = new ProjektiDodajViewModel();
            Model.projekt = new Projekt();
            Model.projekt = _db.Projekti.Where(x => x.ProjektID == ProjektID).FirstOrDefault();
            return View("Dodaj", Model);
        }
        public IActionResult Obrisi(int ProjektID)
        {
            Projekt p = new Projekt();
            p = _db.Projekti.Where(x => x.ProjektID == ProjektID).FirstOrDefault();
            _db.Projekti.Remove(p);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string ProjektId)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "FileStorage",
                        file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            Fajl fajl = new Fajl();
            fajl.Naziv = file.GetFilename();
            _db.Fajlovi.Add(fajl);
            _db.SaveChanges();
            ProjektiFajlovi stavka = new ProjektiFajlovi();
            stavka.FajlID = fajl.FajlId;
            stavka.ProjektID= int.Parse(ProjektId);
            _db.ProjektiFajlovi.Add(stavka);
            _db.SaveChanges();
            return RedirectToAction("Detalji", new { ProjektId = int.Parse(ProjektId) });
        }
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "FileStorage", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
