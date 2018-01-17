using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using MostarConstruct.Web.Areas.Poslovodja.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Web.Helper;


namespace MostarConstruct.Web.Areas.Poslovodja.Controllers
{
    [Autorizacija(false, TipKorisnika.Poslovodja)]

    [Area("Poslovodja")]
    public class OrganizacijaRadilistaController : Controller
    {
        private DatabaseContext _db;
        public OrganizacijaRadilistaController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index(int ? ProjekatId,int ? RadilisteId)
        {
            OrganizacijaRadilistaIndexVM Model = new OrganizacijaRadilistaIndexVM();
            Model.listaProjekata = new List<SelectListItem>();
            Model.listaRadilista = new List<SelectListItem>();

            Model.listaProjekata = _db.Projekti.Select(x => new SelectListItem
            {
                Value = x.ProjektID.ToString(),
                Text = x.Naziv
            }).ToList();

            if(ProjekatId!=null)
            {
                Model.ProjekatId = (int)ProjekatId;
                Model.listaRadilista = _db.Radilista.Where(x => x.ProjektID == ProjekatId).Select(x => new SelectListItem
                {
                    Value = x.RadilisteID.ToString(),
                    Text = x.Naziv
                }).ToList();
            }

            if(RadilisteId!=null && RadilisteId!=0)
            {
                return RedirectToAction("Prikazi", new { ProjekatId = ProjekatId, RadilisteId = RadilisteId });
            }

            return View(Model);
        }
        public IActionResult Prikazi(int ProjekatId,int RadilisteId)
        {
            return View();
        }
        
    }
}