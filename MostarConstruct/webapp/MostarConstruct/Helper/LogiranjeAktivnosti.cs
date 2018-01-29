using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using MostarConstruct.Data;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper
{
    public class LogiranjeAktivnosti
    {
        private DatabaseContext context;
        public LogiranjeAktivnosti(DatabaseContext context)
        {
            this.context = context;
        }       

        public void Logiraj(int KorisnikID,DateTime datum,string IPAdresa,string browser,string aktivnost,string tabela)
        {
            Log log = new Log
            {
                Aktivnost = aktivnost,
                Browser = browser,
                Datum = datum,
                IPAdresa = IPAdresa,
                KorisnikID = KorisnikID,
                Tabela = tabela
            };
            this.context.Logovi.Add(log);
            this.context.SaveChanges();
        }
       
    }
}
