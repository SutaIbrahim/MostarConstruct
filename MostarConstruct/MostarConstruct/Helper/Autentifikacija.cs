using Microsoft.AspNetCore.Http;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Web;

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
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void PokreniNovuSesiju(Korisnik korisnik, HttpContext context, bool zapamtiPassword)
        {
            context.Session.SetJson(LogiraniKorisnik, korisnik);

            if(zapamtiPassword)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(10);
                context.Response.Cookies.Append("_mvc_session", korisnik != null ? korisnik.KorisnikID.ToString() : "");
            }
        }

        public static Korisnik GetLogiraniKorisnik(HttpContext context)
        {
            Korisnik korisnik = context.Session.GetJson<Korisnik>(LogiraniKorisnik);

            if (korisnik != null)
                return korisnik;

            //context.Request.Cookies.Get("_mvc_session");

            return null;
        }
    }
}
