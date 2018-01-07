using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Web.Areas.Administracija.ViewModels;
using MostarConstruct.Web.Helper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{
    [Autorizacija(false, TipKorisnika.Administrator)]
    [Area("Administracija")]
    public class AktivnostiController : Controller
    {
        private DatabaseContext _db;
        public AktivnostiController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            AktivnostiIndexViewModel Model = new AktivnostiIndexViewModel();
            Model.listaAktivnosti = new List<Models.Log>();
            Model.listaAktivnosti = _db.Logovi.OrderByDescending(model => model.Datum).ToList();
            return View(Model);
        }
    }
}