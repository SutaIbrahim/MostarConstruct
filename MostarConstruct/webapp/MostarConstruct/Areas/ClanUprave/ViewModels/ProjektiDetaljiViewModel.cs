using Microsoft.AspNetCore.Http;
using MostarConstruct.Data.Models;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class ProjektiDetaljiViewModel
    {
        public Projekt projekt { get; set; }
        public List<Fajl> listaFajlova { get; set; }
    }
}
