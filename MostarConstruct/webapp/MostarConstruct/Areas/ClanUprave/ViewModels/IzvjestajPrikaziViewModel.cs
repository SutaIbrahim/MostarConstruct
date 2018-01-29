using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class IzvjestajPrikaziViewModel
    {
        public Izvjestaj izvjestaj { get; set; }

        public List<Radiliste> radilista { get; set; }

        public List<Uplata> uplate { get; set; }

        public int BrojNaloga { get; set; }

    }
}
