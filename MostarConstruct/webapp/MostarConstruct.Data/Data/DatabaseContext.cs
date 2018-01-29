using Microsoft.EntityFrameworkCore;
using MostarConstruct.Data.Models;
using MostarConstruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MostarConstruct.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Regija> Regije { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Inventar> Inventar { get; set; }
        public DbSet<InventarRadiliste> InventarRadiliste { get; set; }
        public DbSet<Izvjestaj> Izvjestaji { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Log> Logovi { get; set; }
        public DbSet<Osoba> Osobe { get; set; }
        public DbSet<Plata> Plate { get; set; }
        public DbSet<PlatnaLista> PlatneListe { get; set; }
        public DbSet<Ponuda> Ponude { get; set; }
        public DbSet<Pozicija> Pozicije { get; set; }
        public DbSet<Projekt> Projekti { get; set; }
        public DbSet<ProjektDokumenti> ProjektDokumenti { get; set; }
        public DbSet<PutniNalog> PutniNalozi { get; set; }
        public DbSet<Radiliste> Radilista { get; set; }
        public DbSet<RadniNalog> RadniNalozi { get; set; }
        public DbSet<Radnik> Radnici { get; set; }
        public DbSet<TipKlijenta> TipoviKlijenata { get; set; }
        public DbSet<Uplata> Uplate { get; set; }
        public DbSet<Uvjerenje> Uvjerenja { get; set; }
        public DbSet<VozaciKategorije> VozaciKategorije { get; set; }
        public DbSet<VozackaKategorija> VozackeKategorije { get; set; }
        public DbSet<Vozilo> Vozila { get; set; }
        public DbSet<VrstaVozila> VrsteVozila { get; set; }
        public DbSet<Fajl> Fajlovi { get; set; }
        public DbSet<ProjektiFajlovi> ProjektiFajlovi { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VozaciKategorije>().HasKey(k => new { k.VozacID, k.KategorijaID });
        }
    }
}
