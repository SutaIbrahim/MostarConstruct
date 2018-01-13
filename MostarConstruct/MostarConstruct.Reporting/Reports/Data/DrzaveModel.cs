using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MostarConstruct.Reporting.Reports.Data
{
    public class DrzaveModel
    {
        public class Osoba
        {
            public string ImePrezime { get; set; }
        }

        private List<Osoba> osobe = new List<Osoba>()
        {
            new Osoba(){ImePrezime = "Iva Zovko"},
            new Osoba(){ImePrezime = "Mirza Medar"},
            new Osoba(){ImePrezime = "Nisvet Mujkic"}
        };

        #region Header
        public class Header
        {
            public string Naslov { get; set; }
        }

        public static List<Header> GetHeader()
        {
            return new List<Header>()
            {
                new Header() {Naslov = "Lista osoba" }
            };
        }
        #endregion

        #region Footer

        public class Footer
        {
            public DateTime Datum { get; set; }
        }

        public static List<Footer> GetFooter()
        {
            return new List<Footer>()
            {
                new Footer() {Datum = DateTime.Now}
            };
        }
        #endregion

        #region Body

        public class Body
        {
            public string Osoba { get; set; }
        }

        public static List<Body> GetBody()
        {
            List<Body> o = osobe.Select(x => new Body()
            {
                Osoba = x.ImePrezime
            }).ToList();

            return lista;
        }

        #endregion
    }
}