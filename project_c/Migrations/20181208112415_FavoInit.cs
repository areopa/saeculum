using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace project_c.Migrations
{
    public partial class FavoInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorieten",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GameList = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorieten", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Favorieten_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorieten");
        }
    }
}
