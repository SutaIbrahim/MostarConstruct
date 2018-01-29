using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Poslovodja.ViewModels
{
    public class RadniciGrupniUploadSlikaViewModel
    {
        public IEnumerable<IFormFile> Slike { get; set; }
    }
}
