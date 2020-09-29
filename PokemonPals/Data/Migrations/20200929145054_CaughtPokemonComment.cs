using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonPals.Data.Migrations
{
    public partial class CaughtPokemonComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CaughtPokemon",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CaughtPokemon");
        }
    }
}
