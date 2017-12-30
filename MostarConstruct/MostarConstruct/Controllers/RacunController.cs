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
            if (!ModelState.IsValid)
                return View(vm);

            // poslovodja
            Korisnik k = db.Korisnici.Where(x => x.KorisnickoIme == vm.LoginData).FirstOrDefault();
            Autentifikacija.PokreniNovuSesiju(k, httpContext.HttpContext);

            return RedirectToAction("Index", "Home");
        }
    }
}