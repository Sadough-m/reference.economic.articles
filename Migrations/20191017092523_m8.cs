using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EconomyProject.Migrations
{
    public partial class m8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_AspNetUsers_ApplicationUserId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_Products_ProductId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_ApplicationUserId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLikes_AspNetUsers_ApplicationUserId",
                table: "ProductLikes");

            migrationBuilder.DropIndex(
                name: "IX_ProductLikes_ApplicationUserId",
                table: "ProductLikes");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_ApplicationUserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_CommentLikes_ApplicationUserId",
                table: "CommentLikes");

            migrationBuilder.DropIndex(
                name: "IX_CommentLikes_ProductId",
                table: "CommentLikes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ProductLikes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CommentLikes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CommentLikes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProductLikes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProductComments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ProductComments",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CommentLikes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_UserId",
                table: "ProductLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_UserId",
                table: "CommentLikes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserId",
                table: "CommentLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLikes_AspNetUsers_UserId",
                table: "ProductLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserId",
                table: "CommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLikes_AspNetUsers_UserId",
                table: "ProductLikes");

            migrationBuilder.DropIndex(
                name: "IX_ProductLikes_UserId",
                table: "ProductLikes");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_CommentLikes_UserId",
                table: "CommentLikes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProductLikes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ProductLikes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProductComments",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ProductComments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ProductComments",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CommentLikes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CommentLikes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CommentLikes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductLikes_ApplicationUserId",
                table: "ProductLikes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ApplicationUserId",
                table: "ProductComments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_ApplicationUserId",
                table: "CommentLikes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_ProductId",
                table: "CommentLikes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_AspNetUsers_ApplicationUserId",
                table: "CommentLikes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_Products_ProductId",
                table: "CommentLikes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_ApplicationUserId",
                table: "ProductComments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLikes_AspNetUsers_ApplicationUserId",
                table: "ProductLikes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
