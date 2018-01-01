using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Models;
using MostarConstruct.Web.Helper;

namespace MostarConstruct.Web.Areas.Poslovodja.Controllers
{
    [Area("Poslovodja")]
    public class ZaposleniciController : Controller
    {

        private DatabaseContext db;


        public IActionResult Index()
        {


            return View();
        }




        public IActionResult Dodaj()
        {
            return View(new Drzava());
        }



        [HttpPost]
        public IActionResult Dodaj(Radnik radnik)
        {
            if (!ModelState.IsValid)
                return View(radnik);

            db.Radnici.Add(radnik);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}