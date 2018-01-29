using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Poslovodja.ViewModels
{
    public class RadniciIndexViewModel
    {


        public List<Radnik> Radnici;
        public string srchTxt { get; set; }
    }
}
