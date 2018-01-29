using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MostarConstruct.Migrations
{
    public partial class Inicijalna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    DrzavaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    Oznaka = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.DrzavaID);
                });

            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.KategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "Pozicije",
                columns: table => new
                {
                    PozicijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 60, nullable: false),
                    Satnica = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pozicije", x => x.PozicijaID);
                });

            migrationBuilder.CreateTable(
                name: "TipoviKlijenata",
                columns: table => new
                {
                    TipKlijentaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviKlijenata", x => x.TipKlijentaID);
                });

            migrationBuilder.CreateTable(
                name: "VozackeKategorije",
                columns: table => new
                {
                    VozackaKategorijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VozackeKategorije", x => x.VozackaKategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "VrsteVozila",
                columns: table => new
                {
                    VrstaVozilaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteVozila", x => x.VrstaVozilaID);
                });

            migrationBuilder.CreateTable(
                name: "Regije",
                columns: table => new
                {
                    RegijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DrzavaID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    Oznaka = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regije", x => x.RegijaID);
                    table.ForeignKey(
                        name: "FK_Regije_Drzave_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzave",
                        principalColumn: "DrzavaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventar",
                columns: table => new
                {
                    InventarID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKupovine = table.Column<DateTime>(nullable: true),
                    Ispravno = table.Column<bool>(nullable: false),
                    KategorijaID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    Pogon = table.Column<string>(maxLength: 30, nullable: true),
                    SerijskiBroj = table.Column<string>(maxLength: 30, nullable: false),
                    Tezina = table.Column<string>(maxLength: 30, nullable: true),
                    Zauzeto = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventar", x => x.InventarID);
                    table.ForeignKey(
                        name: "FK_Inventar_Kategorije_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    KlijentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adresa = table.Column<string>(maxLength: 100, nullable: true),
                    BrojNarucenihProjekata = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Fax = table.Column<string>(maxLength: 30, nullable: true),
                    Kompanija = table.Column<string>(maxLength: 100, nullable: false),
                    KontaktOsoba = table.Column<string>(maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(maxLength: 30, nullable: true),
                    TipKlijentaID = table.Column<int>(nullable: false),
                    Ziroracun = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijenti", x => x.KlijentID);
                    table.ForeignKey(
                        name: "FK_Klijenti_TipoviKlijenata_TipKlijentaID",
                        column: x => x.TipKlijentaID,
                        principalTable: "TipoviKlijenata",
                        principalColumn: "TipKlijentaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vozila",
                columns: table => new
                {
                    VoziloID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojSasije = table.Column<string>(maxLength: 40, nullable: false),
                    BrojSjedista = table.Column<int>(nullable: false),
                    CijenaPoSatu = table.Column<decimal>(nullable: true),
                    DatumKupovine = table.Column<DateTime>(nullable: false),
                    DatumRegistracije = table.Column<DateTime>(nullable: false),
                    DatumZadnjegServisa = table.Column<DateTime>(nullable: true),
                    GodinaProizvodnje = table.Column<int>(nullable: false),
                    Kubikaza = table.Column<string>(maxLength: 30, nullable: false),
                    Nosivost = table.Column<string>(maxLength: 20, nullable: true),
                    Proizvodjac = table.Column<string>(maxLength: 100, nullable: false),
                    RegistarskaOznaka = table.Column<string>(maxLength: 20, nullable: false),
                    VozackaKategorijaID = table.Column<int>(nullable: false),
                    VrstaVozilaID = table.Column<int>(nullable: false),
                    Zauzeto = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozila", x => x.VoziloID);
                    table.ForeignKey(
                        name: "FK_Vozila_VozackeKategorije_VozackaKategorijaID",
                        column: x => x.VozackaKategorijaID,
                        principalTable: "VozackeKategorije",
                        principalColumn: "VozackaKategorijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vozila_VrsteVozila_VrstaVozilaID",
                        column: x => x.VrstaVozilaID,
                        principalTable: "VrsteVozila",
                        principalColumn: "VrstaVozilaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    GradID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    PostanskiBroj = table.Column<string>(maxLength: 12, nullable: true),
                    RegijaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.GradID);
                    table.ForeignKey(
                        name: "FK_Gradovi_Regije_RegijaID",
                        column: x => x.RegijaID,
                        principalTable: "Regije",
                        principalColumn: "RegijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Osobe",
                columns: table => new
                {
                    OsobaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adresa = table.Column<string>(maxLength: 100, nullable: true),
                    BracniStatus = table.Column<string>(maxLength: 30, nullable: true),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    GradID = table.Column<int>(nullable: false),
                    Ime = table.Column<string>(maxLength: 100, nullable: false),
                    JMBG = table.Column<string>(maxLength: 13, nullable: false),
                    Prezime = table.Column<string>(maxLength: 100, nullable: false),
                    Slika = table.Column<byte[]>(nullable: true),
                    Spol = table.Column<string>(maxLength: 15, nullable: false),
                    Telefon = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osobe", x => x.OsobaID);
                    table.ForeignKey(
                        name: "FK_Osobe_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(nullable: false),
                    Aktivan = table.Column<bool>(nullable: false),
                    DatumRegistracije = table.Column<DateTime>(nullable: false),
                    DatumZadnjePrijave = table.Column<DateTime>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsClanUprave = table.Column<bool>(nullable: false),
                    IsPoslovodja = table.Column<bool>(nullable: false),
                    KorisnickoIme = table.Column<string>(maxLength: 30, nullable: false),
                    LozinkaHash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikID);
                    table.ForeignKey(
                        name: "FK_Korisnici_Osobe_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Osobe",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Radnici",
                columns: table => new
                {
                    RadnikID = table.Column<int>(nullable: false),
                    Aktivan = table.Column<bool>(nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(nullable: false),
                    DodatakNaSatnicu = table.Column<decimal>(nullable: false),
                    PozicijaID = table.Column<int>(nullable: false),
                    Staz = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnici", x => x.RadnikID);
                    table.ForeignKey(
                        name: "FK_Radnici_Pozicije_PozicijaID",
                        column: x => x.PozicijaID,
                        principalTable: "Pozicije",
                        principalColumn: "PozicijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Radnici_Osobe_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Osobe",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logovi",
                columns: table => new
                {
                    LogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aktivnost = table.Column<string>(maxLength: 100, nullable: false),
                    Browser = table.Column<string>(maxLength: 100, nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    IPAdresa = table.Column<string>(maxLength: 100, nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    Tabela = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logovi", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_Logovi_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ponude",
                columns: table => new
                {
                    PonudaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClanUpraveID = table.Column<int>(nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    PotrebnoVrijeme = table.Column<decimal>(nullable: false),
                    PredlozenaCijena = table.Column<decimal>(nullable: false),
                    Sadrzaj = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ponude", x => x.PonudaID);
                    table.ForeignKey(
                        name: "FK_Ponude_Korisnici_ClanUpraveID",
                        column: x => x.ClanUpraveID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekti",
                columns: table => new
                {
                    ProjektID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojRata = table.Column<int>(nullable: false),
                    Cijena = table.Column<decimal>(nullable: false),
                    ClanUpraveID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: false),
                    PredlozeniPocetak = table.Column<DateTime>(nullable: false),
                    PredlozeniZavrsetak = table.Column<DateTime>(nullable: false),
                    StvarniPocetak = table.Column<DateTime>(nullable: true),
                    StvarniZavrsetak = table.Column<DateTime>(nullable: true),
                    Zavrsen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekti", x => x.ProjektID);
                    table.ForeignKey(
                        name: "FK_Projekti_Korisnici_ClanUpraveID",
                        column: x => x.ClanUpraveID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plate",
                columns: table => new
                {
                    PlataID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BonusIznos = table.Column<decimal>(nullable: false),
                    BrutoIznos = table.Column<decimal>(nullable: false),
                    DatumKnjizenja = table.Column<DateTime>(nullable: false),
                    Godina = table.Column<int>(nullable: false),
                    Mjesec = table.Column<int>(nullable: false),
                    NetoIznos = table.Column<decimal>(nullable: false),
                    RadnikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plate", x => x.PlataID);
                    table.ForeignKey(
                        name: "FK_Plate_Radnici_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlatneListe",
                columns: table => new
                {
                    PlatnaListaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojMjeseci = table.Column<string>(maxLength: 5, nullable: false),
                    BrojProtokola = table.Column<string>(maxLength: 30, nullable: false),
                    ClanUpraveID = table.Column<int>(nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    RadnikID = table.Column<int>(nullable: false),
                    Svrha = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatneListe", x => x.PlatnaListaID);
                    table.ForeignKey(
                        name: "FK_PlatneListe_Korisnici_ClanUpraveID",
                        column: x => x.ClanUpraveID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatneListe_Radnici_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uvjerenja",
                columns: table => new
                {
                    UvjerenjeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojProtokola = table.Column<string>(maxLength: 30, nullable: false),
                    ClanUpraveID = table.Column<int>(nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    RadnikID = table.Column<int>(nullable: false),
                    Svrha = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uvjerenja", x => x.UvjerenjeID);
                    table.ForeignKey(
                        name: "FK_Uvjerenja_Korisnici_ClanUpraveID",
                        column: x => x.ClanUpraveID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uvjerenja_Radnici_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VozaciKategorije",
                columns: table => new
                {
                    VozacID = table.Column<int>(nullable: false),
                    KategorijaID = table.Column<int>(nullable: false),
                    DatumPolaganja = table.Column<DateTime>(nullable: false),
                    VaziDo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VozaciKategorije", x => new { x.VozacID, x.KategorijaID });
                    table.UniqueConstraint("AK_VozaciKategorije_KategorijaID_VozacID", x => new { x.KategorijaID, x.VozacID });
                    table.ForeignKey(
                        name: "FK_VozaciKategorije_VozackeKategorije_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "VozackeKategorije",
                        principalColumn: "VozackaKategorijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VozaciKategorije_Radnici_VozacID",
                        column: x => x.VozacID,
                        principalTable: "Radnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Izvjestaji",
                columns: table => new
                {
                    IzvjestajID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojProtokola = table.Column<string>(nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    ProjektID = table.Column<int>(nullable: false),
                    Svrha = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvjestaji", x => x.IzvjestajID);
                    table.ForeignKey(
                        name: "FK_Izvjestaji_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Izvjestaji_Projekti_ProjektID",
                        column: x => x.ProjektID,
                        principalTable: "Projekti",
                        principalColumn: "ProjektID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjektDokumenti",
                columns: table => new
                {
                    ProjektDokumentiID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojPreuzimanja = table.Column<int>(nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    Dokument = table.Column<byte[]>(nullable: true),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    PoslovodjaID = table.Column<int>(nullable: false),
                    ProjektID = table.Column<int>(nullable: false),
                    Velicina = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjektDokumenti", x => x.ProjektDokumentiID);
                    table.ForeignKey(
                        name: "FK_ProjektDokumenti_Korisnici_PoslovodjaID",
                        column: x => x.PoslovodjaID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjektDokumenti_Projekti_ProjektID",
                        column: x => x.ProjektID,
                        principalTable: "Projekti",
                        principalColumn: "ProjektID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Radilista",
                columns: table => new
                {
                    RadilisteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adresa = table.Column<string>(maxLength: 100, nullable: false),
                    GradID = table.Column<int>(nullable: false),
                    NadzorniOrgan = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    PocetakRadova = table.Column<DateTime>(nullable: false),
                    ProjektID = table.Column<int>(nullable: false),
                    ZavrsetakRadova = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radilista", x => x.RadilisteID);
                    table.ForeignKey(
                        name: "FK_Radilista_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Radilista_Projekti_ProjektID",
                        column: x => x.ProjektID,
                        principalTable: "Projekti",
                        principalColumn: "ProjektID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    UplataID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojUplate = table.Column<int>(nullable: false),
                    ClanUpraveID = table.Column<int>(nullable: false),
                    DatumUplate = table.Column<DateTime>(nullable: false),
                    Iznos = table.Column<decimal>(nullable: false),
                    KlijentID = table.Column<int>(nullable: false),
                    ProjektID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: false),
                    Svrha = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.UplataID);
                    table.ForeignKey(
                        name: "FK_Uplate_Korisnici_ClanUpraveID",
                        column: x => x.ClanUpraveID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uplate_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uplate_Projekti_ProjektID",
                        column: x => x.ProjektID,
                        principalTable: "Projekti",
                        principalColumn: "ProjektID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventarRadiliste",
                columns: table => new
                {
                    InventarRadilisteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumZauzimanja = table.Column<DateTime>(nullable: false),
                    InventarID = table.Column<int>(nullable: false),
                    PoslovodjaID = table.Column<int>(nullable: false),
                    RadilisteID = table.Column<int>(nullable: false),
                    RadnikID = table.Column<int>(nullable: false),
                    Vraceno = table.Column<bool>(nullable: false),
                    ZauzetoDo = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarRadiliste", x => x.InventarRadilisteID);
                    table.ForeignKey(
                        name: "FK_InventarRadiliste_Inventar_InventarID",
                        column: x => x.InventarID,
                        principalTable: "Inventar",
                        principalColumn: "InventarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventarRadiliste_Korisnici_PoslovodjaID",
                        column: x => x.PoslovodjaID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventarRadiliste_Radilista_RadilisteID",
                        column: x => x.RadilisteID,
                        principalTable: "Radilista",
                        principalColumn: "RadilisteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarRadiliste_Radnici_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PutniNalozi",
                columns: table => new
                {
                    PutniNalogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumIzdavanja = table.Column<DateTime>(nullable: false),
                    GradID = table.Column<int>(nullable: false),
                    KorisnikID = table.Column<int>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    RadilisteID = table.Column<int>(nullable: false),
                    RadnikID = table.Column<int>(nullable: false),
                    Svrha = table.Column<string>(maxLength: 100, nullable: false),
                    TroskoviPutovanja = table.Column<decimal>(nullable: false),
                    UkupnoRadnihSati = table.Column<decimal>(nullable: false),
                    VoziloID = table.Column<int>(nullable: false),
                    VrijediDo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PutniNalozi", x => x.PutniNalogID);
                    table.ForeignKey(
                        name: "FK_PutniNalozi_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PutniNalozi_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PutniNalozi_Radilista_RadilisteID",
                        column: x => x.RadilisteID,
                        principalTable: "Radilista",
                        principalColumn: "RadilisteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PutniNalozi_Radnici_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PutniNalozi_Vozila_VoziloID",
                        column: x => x.VoziloID,
                        principalTable: "Vozila",
                        principalColumn: "VoziloID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RadniNalozi",
                columns: table => new
                {
                    RadniNalogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumDo = table.Column<DateTime>(nullable: false),
                    DatumDodjele = table.Column<DateTime>(nullable: false),
                    DatumOd = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    RadilisteID = table.Column<int>(nullable: false),
                    RadnikID = table.Column<int>(nullable: false),
                    Zaduzenje = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadniNalozi", x => x.RadniNalogID);
                    table.ForeignKey(
                        name: "FK_RadniNalozi_Radilista_RadilisteID",
                        column: x => x.RadilisteID,
                        principalTable: "Radilista",
                        principalColumn: "RadilisteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RadniNalozi_Radnici_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gradovi_RegijaID",
                table: "Gradovi",
                column: "RegijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventar_KategorijaID",
                table: "Inventar",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_InventarRadiliste_InventarID",
                table: "InventarRadiliste",
                column: "InventarID");

            migrationBuilder.CreateIndex(
                name: "IX_InventarRadiliste_PoslovodjaID",
                table: "InventarRadiliste",
                column: "PoslovodjaID");

            migrationBuilder.CreateIndex(
                name: "IX_InventarRadiliste_RadilisteID",
                table: "InventarRadiliste",
                column: "RadilisteID");

            migrationBuilder.CreateIndex(
                name: "IX_InventarRadiliste_RadnikID",
                table: "InventarRadiliste",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Izvjestaji_KorisnikID",
                table: "Izvjestaji",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Izvjestaji_ProjektID",
                table: "Izvjestaji",
                column: "ProjektID");

            migrationBuilder.CreateIndex(
                name: "IX_Klijenti_TipKlijentaID",
                table: "Klijenti",
                column: "TipKlijentaID");

            migrationBuilder.CreateIndex(
                name: "IX_Logovi_KorisnikID",
                table: "Logovi",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Osobe_GradID",
                table: "Osobe",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Plate_RadnikID",
                table: "Plate",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_PlatneListe_ClanUpraveID",
                table: "PlatneListe",
                column: "ClanUpraveID");

            migrationBuilder.CreateIndex(
                name: "IX_PlatneListe_RadnikID",
                table: "PlatneListe",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Ponude_ClanUpraveID",
                table: "Ponude",
                column: "ClanUpraveID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektDokumenti_PoslovodjaID",
                table: "ProjektDokumenti",
                column: "PoslovodjaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektDokumenti_ProjektID",
                table: "ProjektDokumenti",
                column: "ProjektID");

            migrationBuilder.CreateIndex(
                name: "IX_Projekti_ClanUpraveID",
                table: "Projekti",
                column: "ClanUpraveID");

            migrationBuilder.CreateIndex(
                name: "IX_PutniNalozi_GradID",
                table: "PutniNalozi",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_PutniNalozi_KorisnikID",
                table: "PutniNalozi",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_PutniNalozi_RadilisteID",
                table: "PutniNalozi",
                column: "RadilisteID");

            migrationBuilder.CreateIndex(
                name: "IX_PutniNalozi_RadnikID",
                table: "PutniNalozi",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_PutniNalozi_VoziloID",
                table: "PutniNalozi",
                column: "VoziloID");

            migrationBuilder.CreateIndex(
                name: "IX_Radilista_GradID",
                table: "Radilista",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Radilista_ProjektID",
                table: "Radilista",
                column: "ProjektID");

            migrationBuilder.CreateIndex(
                name: "IX_Radnici_PozicijaID",
                table: "Radnici",
                column: "PozicijaID");

            migrationBuilder.CreateIndex(
                name: "IX_RadniNalozi_RadilisteID",
                table: "RadniNalozi",
                column: "RadilisteID");

            migrationBuilder.CreateIndex(
                name: "IX_RadniNalozi_RadnikID",
                table: "RadniNalozi",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Regije_DrzavaID",
                table: "Regije",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_ClanUpraveID",
                table: "Uplate",
                column: "ClanUpraveID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_KlijentID",
                table: "Uplate",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_ProjektID",
                table: "Uplate",
                column: "ProjektID");

            migrationBuilder.CreateIndex(
                name: "IX_Uvjerenja_ClanUpraveID",
                table: "Uvjerenja",
                column: "ClanUpraveID");

            migrationBuilder.CreateIndex(
                name: "IX_Uvjerenja_RadnikID",
                table: "Uvjerenja",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_VozackaKategorijaID",
                table: "Vozila",
                column: "VozackaKategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_VrstaVozilaID",
                table: "Vozila",
                column: "VrstaVozilaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventarRadiliste");

            migrationBuilder.DropTable(
                name: "Izvjestaji");

            migrationBuilder.DropTable(
                name: "Logovi");

            migrationBuilder.DropTable(
                name: "Plate");

            migrationBuilder.DropTable(
                name: "PlatneListe");

            migrationBuilder.DropTable(
                name: "Ponude");

            migrationBuilder.DropTable(
                name: "ProjektDokumenti");

            migrationBuilder.DropTable(
                name: "PutniNalozi");

            migrationBuilder.DropTable(
                name: "RadniNalozi");

            migrationBuilder.DropTable(
                name: "Uplate");

            migrationBuilder.DropTable(
                name: "Uvjerenja");

            migrationBuilder.DropTable(
                name: "VozaciKategorije");

            migrationBuilder.DropTable(
                name: "Inventar");

            migrationBuilder.DropTable(
                name: "Vozila");

            migrationBuilder.DropTable(
                name: "Radilista");

            migrationBuilder.DropTable(
                name: "Klijenti");

            migrationBuilder.DropTable(
                name: "Radnici");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropTable(
                name: "VozackeKategorije");

            migrationBuilder.DropTable(
                name: "VrsteVozila");

            migrationBuilder.DropTable(
                name: "Projekti");

            migrationBuilder.DropTable(
                name: "TipoviKlijenata");

            migrationBuilder.DropTable(
                name: "Pozicije");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Osobe");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Regije");

            migrationBuilder.DropTable(
                name: "Drzave");
        }
    }
}
