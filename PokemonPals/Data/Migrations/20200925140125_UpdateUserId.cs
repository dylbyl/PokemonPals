using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonPals.Data.Migrations
{
    public partial class UpdateUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaughtPokemon_AspNetUsers_OwnerId",
                table: "CaughtPokemon");

            migrationBuilder.DropIndex(
                name: "IX_CaughtPokemon_OwnerId",
                table: "CaughtPokemon");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "CaughtPokemon");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CaughtPokemon",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaughtPokemon_UserId",
                table: "CaughtPokemon",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaughtPokemon_AspNetUsers_UserId",
                table: "CaughtPokemon",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaughtPokemon_AspNetUsers_UserId",
                table: "CaughtPokemon");

            migrationBuilder.DropIndex(
                name: "IX_CaughtPokemon_UserId",
                table: "CaughtPokemon");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CaughtPokemon",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "CaughtPokemon",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaughtPokemon_OwnerId",
                table: "CaughtPokemon",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaughtPokemon_AspNetUsers_OwnerId",
                table: "CaughtPokemon",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
