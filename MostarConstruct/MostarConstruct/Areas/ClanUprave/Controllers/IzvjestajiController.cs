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


namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    [Autorizacija(false, TipKorisnika.ClanUprave)]

    [Area("ClanUprave")]


    public class IzvjestajiController : Controller
    {

        private DatabaseContext db;

        public IzvjestajiController(DatabaseContext db)
        {
            this.db = db;
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
            return View();
        }



    }
}