using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper.IHelper
{
    public interface IDropdown
    {
        IEnumerable<SelectListItem> Drzave(bool praznaLista = true);
        IEnumerable<SelectListItem> Regije(bool praznaLista = true);
        IEnumerable<SelectListItem> Regije(int drzavaId, bool praznaLista = true);
        IEnumerable<SelectListItem> Gradovi(bool praznaLista = true);
        IEnumerable<SelectListItem> Gradovi(int regijaId, bool praznaLista = true);
        IEnumerable<SelectListItem> Uloge(bool praznaLista = true);
        IEnumerable<SelectListItem> Pozicije(bool praznaLista = true);
        IEnumerable<SelectListItem> Kategorije(bool praznaLista = true);
        IEnumerable<SelectListItem> Rezultati(bool praznaLista = true);

    }
}
