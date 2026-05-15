using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncCurrentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FoedselsdagsrabatBrugt",
                table: "Kunder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Behandlingstyper",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0001-0000-0000-000000000000"),
                column: "Type",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0002-0000-0000-000000000000"),
                column: "Type",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0003-0000-0000-000000000000"),
                column: "Type",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0004-0000-0000-000000000000"),
                column: "Type",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0005-0000-0000-000000000000"),
                column: "Type",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0006-0000-0000-000000000000"),
                column: "Type",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0007-0000-0000-000000000000"),
                column: "Type",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0008-0000-0000-000000000000"),
                column: "Type",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Behandlingstyper",
                keyColumn: "BehandlingstypeId",
                keyValue: new Guid("aaaaaaaa-0009-0000-0000-000000000000"),
                column: "Type",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoedselsdagsrabatBrugt",
                table: "Kunder");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Behandlingstyper");
        }
    }
}
