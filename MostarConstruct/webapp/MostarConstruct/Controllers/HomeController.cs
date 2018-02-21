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
using MostarConstruct.Web.ViewModels;
using static MostarConstruct.Web.ViewModels.ProjectEventsViewModel;

namespace MostarConstruct.Controllers
{
    [Autorizacija(true)]
    public class HomeController : Controller
    {
        #region DI
        private DatabaseContext db;
        private IHttpContextAccessor httpContextAccessor;
        public HomeController(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.db = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            ViewData[Konfiguracija.LogiraniKorisnik] = httpContextAccessor.HttpContext.Session.GetJson<Korisnik>(Konfiguracija.LogiraniKorisnik);

            HomeIndexViewModel vm = new HomeIndexViewModel()
            {
                Informacije = new HomeIndexViewModel.Detalji()
                {
                    UkupnoProjekata = db.Projekti.Count(),
                    UkupnaZarada = db.Uplate.Sum(x => x.Iznos),
                    UkupnoRadnihNaloga = db.RadniNalozi.Count(),
                    UkupnoRadnika = db.Radnici.Count()
                }
            };

            return View(vm);
        }
        #endregion

        
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
            LogiranjeAktivnosti logiranje = new LogiranjeAktivnosti(db);
            logiranje.Logiraj(2, DateTime.Now.ToLocalTime(), Request.HttpContext.Connection.RemoteIpAddress.ToString(), Request.Headers["User-Agent"].ToString().Substring(0,100), "Dodavanje", "Korisnici");
            return View("Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region FullCalendar       
        public JsonResult GetCalendarData()
        {
            ProjectEventsViewModel vm = new ProjectEventsViewModel()
            {
                Projects = this.LoadData()
            };
                        
            return Json(vm.Projects);
        }

        private List<ProjectEvent> LoadData()
        {
            List<ProjectEvent> projectEvents = new List<ProjectEvent>();
            var dostupneBoje = Konfiguracija.Boje;

            projectEvents = db.Projekti.Select(x => new ProjectEvent()
            {
                Desc = x.Opis,
                Start_Date = x.PredlozeniPocetak.ToString("yyyy-MM-dd"),
                End_Date = x.PredlozeniZavrsetak.ToString("yyyy-MM-dd"),
                Sr = x.ProjektID,
                Title = x.Naziv,
                Color = x.Boja
            }).ToList();            

            return projectEvents;
        }
        #endregion
    }
}
