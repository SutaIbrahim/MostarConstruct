using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;

namespace MostarConstruct.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext context;
        public HomeController(DatabaseContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
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
