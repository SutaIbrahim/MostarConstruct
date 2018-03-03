using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Web.Areas.Poslovodja.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Web.Helper;
using MostarConstruct.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace MostarConstruct.Web.Areas.Poslovodja.Controllers
{
    [Autorizacija(false, TipKorisnika.Poslovodja)]

    [Area("Poslovodja")]
    public class OrganizacijaRadilistaController : Controller
    {
        private DatabaseContext _db;
        IHttpContextAccessor _context;
        public OrganizacijaRadilistaController(DatabaseContext db,IHttpContextAccessor context)
        {
            _db = db;
            _context = context;
        }
        public IActionResult Index(int? ProjekatId, int? RadilisteId)
        {
            OrganizacijaRadilistaIndexVM Model = new OrganizacijaRadilistaIndexVM();
            Model.listaProjekata = new List<SelectListItem>();
            Model.listaRadilista = new List<SelectListItem>();

            Model.listaProjekata = _db.Projekti.Select(x => new SelectListItem
            {
                Value = x.ProjektID.ToString(),
                Text = x.Naziv
            }).ToList();

            if (ProjekatId != null)
            {
                Model.ProjekatId = (int)ProjekatId;
                Model.listaRadilista = _db.Radilista.Where(x => x.ProjektID == ProjekatId).Select(x => new SelectListItem
                {
                    Value = x.RadilisteID.ToString(),
                    Text = x.Naziv
                }).ToList();
            }

            if (RadilisteId != null && RadilisteId != 0)
            {
                return RedirectToAction("Prikazi", new { ProjekatId, RadilisteId });
            }

            return View(Model);
        }
        public IActionResult Prikazi(int ProjekatId, int RadilisteId)
        {
            OrganizacijaRadilistaPrikaziVM Model = new OrganizacijaRadilistaPrikaziVM();
            Model.Radiliste = new Radiliste();
            Model.listaInventara = new List<InventarRadiliste>();
            Model.listaRadnika = new List<RadniNalog>();

            Model.Radiliste = _db.Radilista.Where(x => x.RadilisteID == RadilisteId).Include(x => x.Projekt).Include(x => x.Grad).FirstOrDefault();
            Model.listaInventara = _db.InventarRadiliste.Where(x => x.RadilisteID == RadilisteId).Include(x => x.Inventar).ThenInclude(x=>x.Kategorija).Include(x => x.Radnik).ToList();
            Model.listaRadnika = _db.RadniNalozi.Where(x => x.RadilisteID == RadilisteId).Include(x => x.Radnik).ThenInclude(x => x.Osoba).ToList();
            return View(Model);
        }
        public IActionResult DodajInventar(int RadilisteId)
        {
            OrganizacijaRadilistaDodajInventarVM Model = new OrganizacijaRadilistaDodajInventarVM();
            Model.RadilisteId = RadilisteId;
            Model.listaInventara = new List<SelectListItem>();
            Model.listaRadnika = new List<SelectListItem>();
            List<RadniNalog> listaNaloga = new List<RadniNalog>();
            listaNaloga = _db.RadniNalozi.Where(x => x.RadilisteID == RadilisteId).Include(x=>x.Radnik).ThenInclude(x=>x.Osoba).ToList();
            Model.listaRadnika = listaNaloga.Select(x => new SelectListItem
            {
                Value = x.Radnik.RadnikID.ToString(),
                Text = x.Radnik.Osoba.Ime + " " + x.Radnik.Osoba.Prezime
            }).ToList();

            Model.listaInventara = _db.Inventar.Where(x => x.Zauzeto == false).Select(x => new SelectListItem
            {
                Value = x.InventarID.ToString(),
                Text = x.Naziv
            }).ToList();
            return PartialView(Model);
        }
        public IActionResult DodajRadnika(int RadilisteId)
        {
            OrganizacijaRadilistaDodajRadnikaVM Model = new OrganizacijaRadilistaDodajRadnikaVM();
            Model.listaRadnika = new List<SelectListItem>();
            Model.RadilisteId = RadilisteId;
            List<Radnik> listaRadnika = new List<Radnik>();
            List<Radnik> slobodniRadnici = new List<Radnik>();
            listaRadnika = _db.Radnici.ToList();
            Model.DatumDo = DateTime.Now.AddDays(10);
            foreach (var item in listaRadnika)
            {
                if(_db.RadniNalozi.Where(x=>x.RadnikID==item.RadnikID).FirstOrDefault()==null)
                {
                    slobodniRadnici.Add(_db.Radnici.Where(x => x.RadnikID == item.RadnikID).Include(x=>x.Osoba).FirstOrDefault());
                }
            }
            Model.listaRadnika = slobodniRadnici.Select(x => new SelectListItem
            {
                Value = x.RadnikID.ToString(),
                Text = x.Osoba.Ime + " " + x.Osoba.Prezime
            }).ToList();
            return PartialView(Model);
        }
        public IActionResult SnimiInventar(OrganizacijaRadilistaDodajInventarVM model)
        {
            Korisnik korisnik = _context.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            InventarRadiliste novi = new InventarRadiliste
            {
                PoslovodjaID = korisnik.KorisnikID,
                InventarID = model.InventarId,
                RadilisteID = model.RadilisteId,
                RadnikID = model.RadnikId,
                DatumZauzimanja = DateTime.Now,
                Vraceno = false,
                ZauzetoDo = model.ZauzetoDo
            };
            _db.InventarRadiliste.Add(novi);
            _db.Inventar.Where(x => x.InventarID == model.InventarId).FirstOrDefault().Zauzeto = true;
            _db.SaveChanges();
            
           
            
            return RedirectToAction("Prikazi", new { RadilisteId = model.RadilisteId });
        }
        public IActionResult SnimiRadnika(OrganizacijaRadilistaDodajRadnikaVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("DodajRadnika", model);
            }
            RadniNalog novi = new RadniNalog
            {
                DatumDo = model.DatumDo,
                DatumDodjele = DateTime.Now,
                DatumOd = DateTime.Now,
                Napomena = model.Napomena,
                RadilisteID = model.RadilisteId,
                RadnikID = model.RadnikId,
                Zaduzenje = model.Zaduzenje
            };
            _db.RadniNalozi.Add(novi);
            _db.SaveChanges();
            return RedirectToAction("Prikazi", new { RadilisteId = model.RadilisteId });
        }
        public IActionResult OtpustiRadnika(int RadnikId,int RadilisteId)
        {
            RadniNalog dbNalog = _db.RadniNalozi.Where(x => x.RadnikID == RadnikId && x.RadilisteID == RadilisteId).FirstOrDefault();
            _db.RadniNalozi.Remove(dbNalog);
            _db.SaveChanges();
            return RedirectToAction("Prikazi", new { RadilisteId = RadilisteId });
        }
        public IActionResult OslobodiInventar(int InventarId,int RadilisteId)
        {
            InventarRadiliste dbInventar = _db.InventarRadiliste.Where(x => x.InventarID == InventarId && x.RadilisteID == RadilisteId).FirstOrDefault();
            _db.InventarRadiliste.Remove(dbInventar);
            _db.Inventar.Where(x => x.InventarID == InventarId).FirstOrDefault().Zauzeto = false;
            _db.SaveChanges();
            return RedirectToAction("Prikazi", new { RadilisteId = RadilisteId });
        }
    }
}