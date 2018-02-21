using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper
{
    public static class Konfiguracija
    {
        public static string LogiraniKorisnik = "logirani_korisnik";
        public static string Admin = "Administrator";
        public static string Poslovodja = "Poslovodja";
        public static string ClanUprave = "Clan uprave";

        public static string Sesija1 = "sesija_1";
        public static string Sesija2 = "sesija_2";
        public static string Sesija3 = "sesija_3";

        #region Boje
        public static string[] Boje = typeof(System.Drawing.Color)
        .GetProperties()
        .Where(x => x.PropertyType == typeof(System.Drawing.Color))
        .Select(x => x.Name.ToLower())
        .ToArray();

        #endregion
    }
}
