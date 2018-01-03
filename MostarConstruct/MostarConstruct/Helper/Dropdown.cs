using Microsoft.AspNetCore.Mvc.Rendering;
using MostarConstruct.Data;
using MostarConstruct.Web.Helper.IHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper
{
    public class Dropdown : IDropdown
    {
        private DatabaseContext db;

        public Dropdown(DatabaseContext db)
        {
            this.db = db;
        }

        public IEnumerable<SelectListItem> Drzave(bool praznaLista = true)
        {
            var drzave = db.Drzave;
            List<SelectListItem> lista = new List<SelectListItem>();

            if (praznaLista)
                lista.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite drzavu" });

            lista.AddRange(drzave.Select(x => new SelectListItem() { Value = x.DrzavaID.ToString(), Text = x.Naziv }));

            return lista;
        }

        public IEnumerable<SelectListItem> Regije(bool praznaLista = true)
        {
            var regije = db.Regije;
            List<SelectListItem> lista = new List<SelectListItem>();

            if (praznaLista)
                lista.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite regiju" });

            lista.AddRange(regije.Select(x => new SelectListItem() { Value = x.RegijaID.ToString(), Text = x.Naziv }));

            return lista;
        }

        public IEnumerable<SelectListItem> Gradovi(bool praznaLista = true)
        {
            var gradovi = db.Gradovi;
            List<SelectListItem> lista = new List<SelectListItem>();

            if (praznaLista)
                lista.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite grad" });

            lista.AddRange(gradovi.Select(x => new SelectListItem() { Value = x.GradID.ToString(), Text = x.Naziv }));

            return lista;
        }

        public IEnumerable<SelectListItem> Uloge(bool praznaLista = true)
        {
            List<SelectListItem> uloge = new List<SelectListItem>();

            if (praznaLista)
                uloge.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite ulogu" });

            uloge.Add(new SelectListItem() { Value = TipKorisnika.Administrator.ToString(), Text = Konfiguracija.Admin });
            uloge.Add(new SelectListItem() { Value = TipKorisnika.Poslovodja.ToString(), Text = Konfiguracija.Poslovodja });
            uloge.Add(new SelectListItem() { Value = TipKorisnika.ClanUprave.ToString(), Text = Konfiguracija.ClanUprave });

            return uloge;
        }


        public IEnumerable<SelectListItem> Pozicije(bool praznaLista = true)
        {
            var pozicije = db.Pozicije;
            List<SelectListItem> lista = new List<SelectListItem>();

            if (praznaLista)
                lista.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite poziciju" });

            lista.AddRange(pozicije.Select(x => new SelectListItem() { Value = x.PozicijaID.ToString(), Text = x.Naziv }));

            return lista;
        }

        public IEnumerable<SelectListItem> Kategorije(bool praznaLista = true)
        {
            var kategorije = db.Kategorije;
            List<SelectListItem> lista = new List<SelectListItem>();

            if (praznaLista)
                lista.Add(new SelectListItem() { Value = string.Empty, Text = "Odaberite kategoriju" });

            lista.AddRange(kategorije.Select(x => new SelectListItem() { Value = x.KategorijaID.ToString(), Text = x.Naziv }));

            return lista;
        }
    }
}
