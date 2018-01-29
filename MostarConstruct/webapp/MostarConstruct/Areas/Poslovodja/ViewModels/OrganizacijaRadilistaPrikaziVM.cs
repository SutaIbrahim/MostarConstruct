using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Poslovodja.ViewModels
{
    public class OrganizacijaRadilistaPrikaziVM
    {
        public Radiliste Radiliste { get; set; }
        public List<RadniNalog> listaRadnika { get; set; }
        public List<InventarRadiliste> listaInventara { get; set; }
    }
}
