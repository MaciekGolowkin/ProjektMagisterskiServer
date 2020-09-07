using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektMagisterskiServer.Migrations
{
    public partial class DodanieImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationImages",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TypeOfProcessing = table.Column<string>(nullable: true),
                    Length = table.Column<long>(nullable: false),
                    Width = table.Column<long>(nullable: false),
                    ImgPath = table.Column<string>(nullable: true),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationImages", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_ApplicationImages_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationImages_ApplicationUserID",
                table: "ApplicationImages",
                column: "ApplicationUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationImages");
        }
    }
}
