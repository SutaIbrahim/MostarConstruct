using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MostarConstruct.Web.Areas.ClanUprave.ViewModels;
using MostarConstruct.Web.Helper.IHelper;
using Microsoft.AspNetCore.Http;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]


    public class IzvjestajiController : Controller
    {

        private DatabaseContext db;
        IHttpContextAccessor httpContext;


        public IzvjestajiController(DatabaseContext db, IHttpContextAccessor httpContext)
        {
            this.db = db;
            this.httpContext = httpContext;

        }



        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Dodaj()
        {



            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(Izvjestaj model)
        {





            Korisnik korisnik = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            Korisnik k = httpContext.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            logiranje.Logiraj(korisnik.KorisnikID, DateTime.Now, httpContext.HttpContext.Connection.RemoteIpAddress.ToString(), httpContext.HttpContext.Request.Headers["User-Agent"].ToString().Substring(0, 100), "Dodavanje izvjestaja", "Izvjestaji");


            return View();
        }



    }
}