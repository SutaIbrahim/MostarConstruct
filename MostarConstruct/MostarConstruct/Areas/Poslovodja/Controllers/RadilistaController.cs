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


namespace MostarConstruct.Web.Areas.Poslovodja.Controllers
{

    [Area("Poslovodja")]

    public class RadilistaController : Controller
    {


        private DatabaseContext db;

        public IActionResult Index()
        {

            return View(db.Radilista.Include(x=>x.Projekt));
        }



        public IActionResult Dodaj()
        {


            return View(new Radiliste());
        }




        public IActionResult Dodaj(Radiliste rad)
        {




            return View();
        }


    }
}