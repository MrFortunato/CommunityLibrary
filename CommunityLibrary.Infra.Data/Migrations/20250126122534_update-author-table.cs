using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityLibrary.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateauthortable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_UserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRentals_Users_UserId",
                table: "BookRentals");

            migrationBuilder.DropIndex(
                name: "IX_Authors_UserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookRentals",
                newName: "RegisteredByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookRentals_UserId",
                table: "BookRentals",
                newName: "IX_BookRentals_RegisteredByUserId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RegisteredByUserId",
                table: "Authors",
                type: "BINARY(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_RegisteredByUserId",
                table: "Authors",
                column: "RegisteredByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_RegisteredByUserId",
                table: "Authors",
                column: "RegisteredByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRentals_Users_RegisteredByUserId",
                table: "BookRentals",
                column: "RegisteredByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_RegisteredByUserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRentals_Users_RegisteredByUserId",
                table: "BookRentals");

            migrationBuilder.DropIndex(
                name: "IX_Authors_RegisteredByUserId",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "RegisteredByUserId",
                table: "BookRentals",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookRentals_RegisteredByUserId",
                table: "BookRentals",
                newName: "IX_BookRentals_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegisteredByUserId",
                table: "Authors",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(byte[]),
                oldType: "BINARY(16)");

            migrationBuilder.AddColumn<byte[]>(
                name: "UserId",
                table: "Authors",
                type: "BINARY(16)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_UserId",
                table: "Authors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRentals_Users_UserId",
                table: "BookRentals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
