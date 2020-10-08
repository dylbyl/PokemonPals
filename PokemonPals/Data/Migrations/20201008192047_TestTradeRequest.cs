using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonPals.Data.Migrations
{
    public partial class TestTradeRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "CaughtPokemon",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TradeRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesiredPokemonId = table.Column<int>(nullable: false),
                    OfferedPokemonId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeRequest_CaughtPokemon_DesiredPokemonId",
                        column: x => x.DesiredPokemonId,
                        principalTable: "CaughtPokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeRequest_CaughtPokemon_OfferedPokemonId",
                        column: x => x.OfferedPokemonId,
                        principalTable: "CaughtPokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeRequest_DesiredPokemonId",
                table: "TradeRequest",
                column: "DesiredPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeRequest_OfferedPokemonId",
                table: "TradeRequest",
                column: "OfferedPokemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeRequest");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "CaughtPokemon",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);
        }
    }
}
