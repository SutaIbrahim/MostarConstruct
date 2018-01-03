using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;
using MostarConstruct.Web.ViewModels;

namespace MostarConstruct.Web.Controllers
{
    public class RacunController : Controller
    {
        private DatabaseContext db;
        private IHttpContextAccessor httpContext;

        public RacunController(DatabaseContext db, IHttpContextAccessor httpContext)
        {
            this.db = db;
            this.httpContext = httpContext;  
        }

        public IActionResult Prijava()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Prijava(LoginViewModel vm)
        {
            // provjeriti i za mail!
            Korisnik korisnik = db.Korisnici.Where(x => x.KorisnickoIme == vm.LoginData).FirstOrDefault();

            if (korisnik == null)
                ModelState.AddModelError("", "Korisnicko ime ili lozinka nisu tacni");
            else
            {
                if (!Sigurnost.DaLiSePodudaraju(korisnik.LozinkaHash, vm.Password))
                    ModelState.AddModelError("", "Korisnicko ime ili lozinka nisu tacni");
            }
            if (!ModelState.IsValid)
                return View(vm);

            Autentifikacija.PokreniNovuSesiju(korisnik, httpContext.HttpContext);

            korisnik.DatumZadnjePrijave = DateTime.Now;
            db.Korisnici.Update(korisnik);
            db.SaveChanges();

            if (!korisnik.PromijenioLozinku)
                return RedirectToAction("Lozinka", "Racun");            
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Lozinka()
        {
            return View(new RacunLozinkaViewModel());
        }

        [HttpPost]
        public IActionResult Lozinka(RacunLozinkaViewModel viewModel)
        {
            if (viewModel.PotvrdaLozinke != viewModel.Lozinka)
                ModelState.AddModelError("", "Lozinke se ne podudaraju");

            if (!ModelState.IsValid)
                return View(viewModel);
            
            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            korisnik.PromijenioLozinku = true;
            korisnik.LozinkaHash = Sigurnost.GenerisiHash(viewModel.Lozinka);

            db.Korisnici.Update(korisnik);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}