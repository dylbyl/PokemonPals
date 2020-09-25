using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonPals.Data.Migrations
{
    public partial class UpdatingAvatars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Avatar",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Avatar");
        }
    }
}
