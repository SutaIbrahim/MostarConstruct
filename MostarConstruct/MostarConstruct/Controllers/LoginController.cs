using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostarConstruct.Web.ViewModels;

namespace MostarConstruct.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index() => View(new LoginViewModel());

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            return View();
        }

    }
}