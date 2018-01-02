using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MostarConstruct.Web.Areas.ClanUprave.Controllers
{
    public class IzvjestajiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}