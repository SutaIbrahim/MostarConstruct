using MostarConstruct.Models;
using MostarConstruct.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Administracija.ViewModels
{
    public class DrzaveIndexViewModel
    {
        public IEnumerable<Drzava> Drzave{ get; set; }
        public PagingInfo PagingInfo{ get; set; }
    }
}
