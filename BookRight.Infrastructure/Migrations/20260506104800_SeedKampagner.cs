using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedKampagner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kampagner",
                columns: new[] { "KampagneId", "Aktiv", "GaeldendeBehandlingstyper", "Navn", "SlutDato", "StartDato", "RabatProcent" },
                values: new object[] { new Guid("cccccccc-0001-0000-0000-000000000000"), true, "Fysioterapi", "Sommerkampagne fysioterapi", new DateOnly(2026, 6, 30), new DateOnly(2026, 6, 1), 20m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kampagner",
                keyColumn: "KampagneId",
                keyValue: new Guid("cccccccc-0001-0000-0000-000000000000"));
        }
    }
}
