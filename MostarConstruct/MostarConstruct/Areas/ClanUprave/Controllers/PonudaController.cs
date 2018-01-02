using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using Microsoft.AspNetCore.Http;
using MostarConstruct.Web.Helper;
using MostarConstruct.Models;
using Microsoft.EntityFrameworkCore;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{

    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]
    public class PonudaController : Controller
    {

        private DatabaseContext db;
        private IHttpContextAccessor httpContext;

        public PonudaController(DatabaseContext db, IHttpContextAccessor httpContext)
        {
            this.db = db;
            this.httpContext = httpContext;
        }


        public IActionResult Index()
        {
            var model = db.Ponude.Include(k => k.ClanUprave).ThenInclude(o => o.Osoba);

            return View(model);
        }


        public IActionResult Dodaj()
        {
            return View(new Ponuda());
        }


        [HttpPost]
        public IActionResult Dodaj(Ponuda _ponuda)
        {
            if (!ModelState.IsValid)
            {
                return View(_ponuda);
            }

            Ponuda ponuda = _ponuda;
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            ponuda.DatumIzdavanja= DateTime.Now;
            ponuda.ClanUpraveID = k.KorisnikID;

            db.Ponude.Add(ponuda);
            db.SaveChanges();



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Obrisi(int id)
        {
            Ponuda x = db.Ponude.Where(y => y.PonudaID == id).FirstOrDefault();

            db.Ponude.Remove(x);

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }




    }
}