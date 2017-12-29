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
        private const string LogiraniKorisnik = "logirani_korisnik";
        private IServiceProvider service;

        public Autentifikacija(IServiceProvider service)
        {
            this.service = service;
        }

        public static void PokreniNovuSesiju(Korisnik korisnik, HttpContext context)
        {
            context.Session.SetJson(LogiraniKorisnik, korisnik);

           
        }
        
        public static Korisnik GetLogiraniKorisnik(HttpContext context)
        {
            Korisnik korisnik = context.Session.GetJson<Korisnik>(LogiraniKorisnik);

            if (korisnik != null)
                return korisnik;
            
            PokreniNovuSesiju(korisnik, context);

            return korisnik;
        }


    }
}
