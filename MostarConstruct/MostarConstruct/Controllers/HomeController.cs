using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;

namespace MostarConstruct.Controllers
{
    [Autorizacija(true)]
    public class HomeController : Controller
    {
        private DatabaseContext context;
        private IHttpContextAccessor httpContextAccessor;
        public HomeController(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            ViewData[Konfiguracija.LogiraniKorisnik] = httpContextAccessor.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);
            return View();
            
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your aplication description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public string Prikaz()
        {
            return "Ja sam pozvan ajaxom!";
        }
        public IActionResult Testirajlogiranje()
        {
            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(context);
            logiranje.Logiraj(2, DateTime.Now.ToLocalTime(), Request.HttpContext.Connection.RemoteIpAddress.ToString(), Request.Headers["User-Agent"].ToString().Substring(0,100), "Dodavanje", "Korisnici");
            return View("Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
