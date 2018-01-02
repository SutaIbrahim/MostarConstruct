using Microsoft.AspNetCore.Mvc.Filters;
using MostarConstruct.Data;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper
{
    public class Autorizacija : Attribute, IAuthorizationFilter
    {
        private readonly bool _sviKorisnici;
        private readonly TipKorisnika[] _korisnickeUloge;
        public Autorizacija(bool sviKorisnici, params TipKorisnika[] korisnickeUloge)
        {
            _sviKorisnici = sviKorisnici;
            _korisnickeUloge = korisnickeUloge;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Korisnik korisnik = Autentifikacija.GetLogiraniKorisnik(context.HttpContext);

            if (korisnik == null)
            {
                context.HttpContext.Response.Redirect("/Racun/Prijava");
                return;
            }

            if (_sviKorisnici && korisnik.Aktivan)
                return;

            if (!_sviKorisnici && korisnik.Aktivan && korisnik.IsAdmin && _korisnickeUloge.Contains(TipKorisnika.Administrator))
                return;

            if (!_sviKorisnici && korisnik.Aktivan && korisnik.IsClanUprave && _korisnickeUloge.Contains(TipKorisnika.ClanUprave))
                return;

            if (!_sviKorisnici && korisnik.Aktivan && korisnik.IsPoslovodja && _korisnickeUloge.Contains(TipKorisnika.Poslovodja))
                return;

            context.HttpContext.Response.Redirect("/Racun/Prijava");
        }
    }
}
