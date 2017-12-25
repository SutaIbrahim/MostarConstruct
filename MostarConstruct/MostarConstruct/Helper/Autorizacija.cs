using Microsoft.AspNetCore.Mvc.Filters;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper
{
    public class Autorizacija : Attribute, IAuthorizationFilter
    {
        private readonly bool _sviZaposlenici;
        private readonly TipKorisnika[] _korisnickeUloge;

        public Autorizacija(bool sviZaposlenici, params TipKorisnika[] korisnickeUloge)
        {
            _sviZaposlenici = sviZaposlenici;
            _korisnickeUloge = korisnickeUloge;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Korisnik korisnik = Autentifikacija.GetLogiraniKorisnik(context.HttpContext);

            if(korisnik == null)
            {
                context.HttpContext.Response.Redirect("/Login");
                return;
            }

            if (_sviZaposlenici)
                return;

            context.HttpContext.Response.Redirect("/Login");
        }
    }
}
