using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Data;
using MostarConstruct.Web.Helper;
using MostarConstruct.Web.Helper.IHelper;

namespace MostarConstruct.Web.Areas.Administracija.Controllers
{

    [Area(nameof(Administracija))]
    [Autorizacija(false, TipKorisnika.Administrator)]
    public class GradoviController : Controller
    {
        #region DI
        private DatabaseContext db;
        private IDropdown dd;

        public GradoviController(DatabaseContext db, IDropdown dd)
        {
            this.db = db;
            this.dd = dd;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Helpers
        public JsonResult GetGradoviByRegijaId(int regijaId)
        {
            List<SelectListItem> gradovi = dd.Gradovi(regijaId).ToList();
            return Json(gradovi);
        }
        #endregion
    }
}