using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektMagisterskiServer.Migrations
{
    public partial class DodanieImage12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApplicationUserID = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true),
                    Length = table.Column<long>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TypeOfProcessing = table.Column<string>(nullable: true),
                    Width = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationImages", x => x.Id);
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
    }
}
