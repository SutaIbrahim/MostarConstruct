using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Areas.Poslovodja.ViewModels
{
    public class OrganizacijaRadilistaDodajRadnikaVM
    {
        public int RadilisteId { get; set; }
        [Required(ErrorMessage ="Radnik je obavezan!")]
        public int RadnikId { get; set; }
        [Required(ErrorMessage = "Datum je obavezan!")]
        public DateTime DatumDo { get; set; }
        public string Napomena { get; set; }
        [Required(ErrorMessage = "Zaduzenje je obavezno polje!")]
        public string Zaduzenje { get; set; }
        public List<SelectListItem> listaRadnika { get; set; }
    }
}
