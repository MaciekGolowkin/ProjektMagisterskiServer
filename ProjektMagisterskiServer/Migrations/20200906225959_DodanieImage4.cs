using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektMagisterskiServer.Migrations
{
    public partial class DodanieImage4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RodzajPrzetwarzania",
                table: "ApplicationImages",
                newName: "TypeOfProcessing");

            migrationBuilder.RenameColumn(
                name: "Opis",
                table: "ApplicationImages",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "NazwaObrazu",
                table: "ApplicationImages",
                newName: "Name");

            migrationBuilder.AddColumn<long>(
                name: "Length",
                table: "ApplicationImages",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Width",
                table: "ApplicationImages",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "ApplicationImages");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "ApplicationImages");

            migrationBuilder.RenameColumn(
                name: "TypeOfProcessing",
                table: "ApplicationImages",
                newName: "RodzajPrzetwarzania");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ApplicationImages",
                newName: "NazwaObrazu");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ApplicationImages",
                newName: "Opis");
        }
    }
}
