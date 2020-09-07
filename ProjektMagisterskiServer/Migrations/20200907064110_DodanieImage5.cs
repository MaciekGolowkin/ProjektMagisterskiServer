using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektMagisterskiServer.Migrations
{
    public partial class DodanieImage5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationImages_AspNetUsers_ApplicationUserID",
                table: "ApplicationImages");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationImages_ApplicationUserID",
                table: "ApplicationImages");

            migrationBuilder.DropColumn(
                name: "MessageBoardId",
                table: "ApplicationImages");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "ApplicationImages",
                newName: "ApplicationUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageID",
                table: "ApplicationImages",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "ApplicationImages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationImages_ApplicationUserID",
                table: "ApplicationImages",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationImages_AspNetUsers_ApplicationUserID",
                table: "ApplicationImages",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationImages_AspNetUsers_ApplicationUserID",
                table: "ApplicationImages");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationImages_ApplicationUserID",
                table: "ApplicationImages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "ApplicationImages");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ApplicationImages",
                newName: "ApplicationUserID");

            migrationBuilder.AlterColumn<int>(
                name: "ImageID",
                table: "ApplicationImages",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "MessageBoardId",
                table: "ApplicationImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationImages_ApplicationUserID",
                table: "ApplicationImages",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationImages_AspNetUsers_ApplicationUserID",
                table: "ApplicationImages",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
