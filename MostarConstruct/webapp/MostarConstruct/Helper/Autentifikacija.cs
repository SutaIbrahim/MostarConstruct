using Microsoft.AspNetCore.Http;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Web;
using MostarConstruct.Data;

namespace MostarConstruct.Web.Helper
{
    public enum TipKorisnika
    {
        Administrator = 1,
        Poslovodja,
        ClanUprave
    }
    
    public class Autentifikacija
    {
        private const string _logiraniKorisnik = "logirani_korisnik";
        private IHttpContextAccessor httpContextAccessor;

        public Autentifikacija(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public static void PokreniNovuSesiju(Korisnik korisnik, HttpContext context)
        {
            context.Session.SetJson(_logiraniKorisnik, korisnik);           
        }

        public static void OcistiSesiju(HttpContext httpContext) => httpContext.Session.SetJson(_logiraniKorisnik, null);
        
        public static Korisnik GetLogiraniKorisnik(HttpContext context)
        {
            Korisnik korisnik = context.Session.GetJson<Korisnik>(_logiraniKorisnik);

            if (korisnik != null)
                return korisnik;
            
            PokreniNovuSesiju(korisnik, context);

            return korisnik;
        }


    }
}
