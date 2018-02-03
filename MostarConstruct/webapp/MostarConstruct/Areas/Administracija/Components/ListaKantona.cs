using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.Components
{
    public class ListaKantona : ViewComponent
    {
        private readonly DatabaseContext db;

        public ListaKantona(DatabaseContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke(int drzavaId)
        {
            return View(db.Regije.Where(x => x.DrzavaID == drzavaId).ToList());
        }
    }
}
