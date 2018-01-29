using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MostarConstruct.Migrations
{
    public partial class fajlovi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fajlovi",
                columns: table => new
                {
                    FajlId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumDodavanja = table.Column<DateTime>(nullable: false),
                    Lokacija = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fajlovi", x => x.FajlId);
                });

            migrationBuilder.CreateTable(
                name: "ProjektiFajlovi",
                columns: table => new
                {
                    ProjektFajlID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FajlID = table.Column<int>(nullable: false),
                    ProjektID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjektiFajlovi", x => x.ProjektFajlID);
                    table.ForeignKey(
                        name: "FK_ProjektiFajlovi_Fajlovi_FajlID",
                        column: x => x.FajlID,
                        principalTable: "Fajlovi",
                        principalColumn: "FajlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjektiFajlovi_Projekti_ProjektID",
                        column: x => x.ProjektID,
                        principalTable: "Projekti",
                        principalColumn: "ProjektID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjektiFajlovi_FajlID",
                table: "ProjektiFajlovi",
                column: "FajlID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektiFajlovi_ProjektID",
                table: "ProjektiFajlovi",
                column: "ProjektID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjektiFajlovi");

            migrationBuilder.DropTable(
                name: "Fajlovi");
        }
    }
}
