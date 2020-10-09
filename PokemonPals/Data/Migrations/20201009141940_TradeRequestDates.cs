using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokemonPals.Data.Migrations
{
    public partial class TradeRequestDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "TradeRequest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSent",
                table: "TradeRequest",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isOpen",
                table: "TradeRequest",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "TradeRequest");

            migrationBuilder.DropColumn(
                name: "DateSent",
                table: "TradeRequest");

            migrationBuilder.DropColumn(
                name: "isOpen",
                table: "TradeRequest");
        }
    }
}
