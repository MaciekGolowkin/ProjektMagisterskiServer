using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektMagisterskiServer.Migrations
{
    public partial class DodanieImage3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationImages",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NazwaObrazu = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    RodzajPrzetwarzania = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true),
                    MessageBoardId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApplicationUserID = table.Column<string>(nullable: true)
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
