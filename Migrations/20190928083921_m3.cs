using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EconomyProject.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpierDate",
                table: "Carts",
                newName: "CreateDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "PayDate",
                table: "Carts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayDate",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Carts",
                newName: "ExpierDate");
        }
    }
}
