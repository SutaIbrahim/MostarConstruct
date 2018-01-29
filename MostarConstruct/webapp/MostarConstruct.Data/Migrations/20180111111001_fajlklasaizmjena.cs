using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MostarConstruct.Migrations
{
    public partial class fajlklasaizmjena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lokacija",
                table: "Fajlovi",
                newName: "Tip");

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Fajlovi",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<byte[]>(
                name: "Podaci",
                table: "Fajlovi",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Podaci",
                table: "Fajlovi");

            migrationBuilder.RenameColumn(
                name: "Tip",
                table: "Fajlovi",
                newName: "Lokacija");

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Fajlovi",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
