using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.ClanUprave.ViewModels
{
    public class UvjerenjaDodajVM
    {
        public int UvjerenjeId { get; set; }
        [Required]
        public int RadnikId { get; set; }
        public List<SelectListItem> listaRadnika { get; set; }
        [Required(ErrorMessage ="Polje \"Svrha\" je obavezno!")]
        [StringLength(100, MinimumLength = 5,ErrorMessage ="Dužina polja \"Svrha\" mora biti najmanje 5 karaktera!")]
        public string Svrha { get; set; }
        public string Napomena { get; set; }
    }
}
