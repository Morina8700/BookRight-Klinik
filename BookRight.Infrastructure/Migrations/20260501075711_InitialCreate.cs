using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookRight.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Behandlere",
                columns: table => new
                {
                    BehandlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fornavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Efternavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorisationsNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorisationsType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Behandlere", x => x.BehandlerId);
                });

            migrationBuilder.CreateTable(
                name: "Behandlingstyper",
                columns: table => new
                {
                    BehandlingstypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VarighedMinutter = table.Column<int>(type: "int", nullable: false),
                    Pris = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    KrævetAutorisationsType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Behandlingstyper", x => x.BehandlingstypeId);
                });

            migrationBuilder.CreateTable(
                name: "Bookinger",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KundeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BehandlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KlinikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BehandlingstypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KampagneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlutTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OprettetDen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrisUdenRabat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrisMedRabat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AnvendtRabatType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookinger", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "Klinikker",
                columns: table => new
                {
                    KlinikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AntalRum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klinikker", x => x.KlinikId);
                });

            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    KundeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fornavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Efternavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fødselsdato = table.Column<DateOnly>(type: "date", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Helbredsnotater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForetrukkenBehandlerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    loyalitetsNiveau = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.KundeId);
                });

            migrationBuilder.CreateTable(
                name: "BehandlerBehandlingstyper",
                columns: table => new
                {
                    BehandlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BehandlingstyperBehandlingstypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehandlerBehandlingstyper", x => new { x.BehandlerId, x.BehandlingstyperBehandlingstypeId });
                    table.ForeignKey(
                        name: "FK_BehandlerBehandlingstyper_Behandlere_BehandlerId",
                        column: x => x.BehandlerId,
                        principalTable: "Behandlere",
                        principalColumn: "BehandlerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BehandlerBehandlingstyper_Behandlingstyper_BehandlingstyperBehandlingstypeId",
                        column: x => x.BehandlingstyperBehandlingstypeId,
                        principalTable: "Behandlingstyper",
                        principalColumn: "BehandlingstypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BehandlerKlinikker",
                columns: table => new
                {
                    BehandlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KlinikkerKlinikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehandlerKlinikker", x => new { x.BehandlerId, x.KlinikkerKlinikId });
                    table.ForeignKey(
                        name: "FK_BehandlerKlinikker_Behandlere_BehandlerId",
                        column: x => x.BehandlerId,
                        principalTable: "Behandlere",
                        principalColumn: "BehandlerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BehandlerKlinikker_Klinikker_KlinikkerKlinikId",
                        column: x => x.KlinikkerKlinikId,
                        principalTable: "Klinikker",
                        principalColumn: "KlinikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Behandlere",
                columns: new[] { "BehandlerId", "AutorisationsNummer", "AutorisationsType", "Efternavn", "Email", "Fornavn", "Telefon" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-0001-0000-0000-000000000000"), "FYS-001", 0, "Nielsen", "anders@bookright.dk", "Anders", "11111101" },
                    { new Guid("bbbbbbbb-0002-0000-0000-000000000000"), "FYS-002", 0, "Hansen", "birgitte@bookright.dk", "Birgitte", "11111102" },
                    { new Guid("bbbbbbbb-0003-0000-0000-000000000000"), "FYS-003", 0, "Madsen", "casper@bookright.dk", "Casper", "11111103" },
                    { new Guid("bbbbbbbb-0004-0000-0000-000000000000"), "FYS-004", 0, "Sørensen", "diana@bookright.dk", "Diana", "11111104" },
                    { new Guid("bbbbbbbb-0005-0000-0000-000000000000"), "MAS-001", 1, "Christensen", "erik@bookright.dk", "Erik", "11111105" },
                    { new Guid("bbbbbbbb-0006-0000-0000-000000000000"), "MAS-002", 1, "Pedersen", "freja@bookright.dk", "Freja", "11111106" },
                    { new Guid("bbbbbbbb-0007-0000-0000-000000000000"), "MAS-003", 1, "Jensen", "gunnar@bookright.dk", "Gunnar", "11111107" },
                    { new Guid("bbbbbbbb-0008-0000-0000-000000000000"), "AKU-001", 2, "Larsen", "hanne@bookright.dk", "Hanne", "11111108" },
                    { new Guid("bbbbbbbb-0009-0000-0000-000000000000"), "AKU-002", 2, "Olsen", "ivan@bookright.dk", "Ivan", "11111109" },
                    { new Guid("bbbbbbbb-0010-0000-0000-000000000000"), "AKU-003", 2, "Thomsen", "julie@bookright.dk", "Julie", "11111110" },
                    { new Guid("bbbbbbbb-0011-0000-0000-000000000000"), "KOS-001", 3, "Andersen", "klaus@bookright.dk", "Klaus", "11111111" },
                    { new Guid("bbbbbbbb-0012-0000-0000-000000000000"), "KOS-002", 3, "Møller", "laura@bookright.dk", "Laura", "11111112" }
                });

            migrationBuilder.InsertData(
                table: "Behandlingstyper",
                columns: new[] { "BehandlingstypeId", "KrævetAutorisationsType", "Navn", "Pris", "VarighedMinutter" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-0001-0000-0000-000000000000"), 0, "Fysioterapi 30 min", 395m, 30 },
                    { new Guid("aaaaaaaa-0002-0000-0000-000000000000"), 0, "Fysioterapi 45 min", 589m, 45 },
                    { new Guid("aaaaaaaa-0003-0000-0000-000000000000"), 0, "Fysioterapi 60 min", 745m, 60 },
                    { new Guid("aaaaaaaa-0004-0000-0000-000000000000"), 1, "Sportsmassage 30 min", 350m, 30 },
                    { new Guid("aaaaaaaa-0005-0000-0000-000000000000"), 1, "Sportsmassage 60 min", 699m, 60 },
                    { new Guid("aaaaaaaa-0006-0000-0000-000000000000"), 2, "Akupunktur 45 min", 550m, 45 },
                    { new Guid("aaaaaaaa-0007-0000-0000-000000000000"), 3, "Kostvejledning førstegangskons.", 799m, 60 },
                    { new Guid("aaaaaaaa-0008-0000-0000-000000000000"), 3, "Kostvejledning opfølgning", 450m, 30 },
                    { new Guid("aaaaaaaa-0009-0000-0000-000000000000"), 0, "Holdtræning/genoptræning", 150m, 60 }
                });

            migrationBuilder.InsertData(
                table: "Klinikker",
                columns: new[] { "KlinikId", "Adresse", "AntalRum", "Navn" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Vejle Centervej 1, 7100 Vejle", 4, "BookRight Vejle" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Egtvedvej 5, 6040 Egtved", 3, "BookRight Egtved" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Kolding Storcenter 2, 6000 Kolding", 3, "BookRight Kolding" }
                });

            migrationBuilder.InsertData(
                table: "BehandlerBehandlingstyper",
                columns: new[] { "BehandlerId", "BehandlingstyperBehandlingstypeId" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-0001-0000-0000-000000000000"), new Guid("aaaaaaaa-0001-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0001-0000-0000-000000000000"), new Guid("aaaaaaaa-0002-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0001-0000-0000-000000000000"), new Guid("aaaaaaaa-0003-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0001-0000-0000-000000000000"), new Guid("aaaaaaaa-0009-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0002-0000-0000-000000000000"), new Guid("aaaaaaaa-0001-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0002-0000-0000-000000000000"), new Guid("aaaaaaaa-0002-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0002-0000-0000-000000000000"), new Guid("aaaaaaaa-0003-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0002-0000-0000-000000000000"), new Guid("aaaaaaaa-0009-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0003-0000-0000-000000000000"), new Guid("aaaaaaaa-0001-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0003-0000-0000-000000000000"), new Guid("aaaaaaaa-0002-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0003-0000-0000-000000000000"), new Guid("aaaaaaaa-0003-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0003-0000-0000-000000000000"), new Guid("aaaaaaaa-0009-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0004-0000-0000-000000000000"), new Guid("aaaaaaaa-0001-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0004-0000-0000-000000000000"), new Guid("aaaaaaaa-0002-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0004-0000-0000-000000000000"), new Guid("aaaaaaaa-0003-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0004-0000-0000-000000000000"), new Guid("aaaaaaaa-0009-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0005-0000-0000-000000000000"), new Guid("aaaaaaaa-0004-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0005-0000-0000-000000000000"), new Guid("aaaaaaaa-0005-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0006-0000-0000-000000000000"), new Guid("aaaaaaaa-0004-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0006-0000-0000-000000000000"), new Guid("aaaaaaaa-0005-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0007-0000-0000-000000000000"), new Guid("aaaaaaaa-0004-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0007-0000-0000-000000000000"), new Guid("aaaaaaaa-0005-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0008-0000-0000-000000000000"), new Guid("aaaaaaaa-0006-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0009-0000-0000-000000000000"), new Guid("aaaaaaaa-0006-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0010-0000-0000-000000000000"), new Guid("aaaaaaaa-0006-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0011-0000-0000-000000000000"), new Guid("aaaaaaaa-0007-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0011-0000-0000-000000000000"), new Guid("aaaaaaaa-0008-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0012-0000-0000-000000000000"), new Guid("aaaaaaaa-0007-0000-0000-000000000000") },
                    { new Guid("bbbbbbbb-0012-0000-0000-000000000000"), new Guid("aaaaaaaa-0008-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "BehandlerKlinikker",
                columns: new[] { "BehandlerId", "KlinikkerKlinikId" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-0001-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-0001-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-0002-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-0002-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("bbbbbbbb-0003-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-0003-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("bbbbbbbb-0004-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-0004-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-0005-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-0005-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-0006-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-0006-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("bbbbbbbb-0007-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-0007-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("bbbbbbbb-0008-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-0008-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-0009-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-0009-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("bbbbbbbb-0010-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-0010-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("bbbbbbbb-0011-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-0012-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BehandlerBehandlingstyper_BehandlingstyperBehandlingstypeId",
                table: "BehandlerBehandlingstyper",
                column: "BehandlingstyperBehandlingstypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BehandlerKlinikker_KlinikkerKlinikId",
                table: "BehandlerKlinikker",
                column: "KlinikkerKlinikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BehandlerBehandlingstyper");

            migrationBuilder.DropTable(
                name: "BehandlerKlinikker");

            migrationBuilder.DropTable(
                name: "Bookinger");

            migrationBuilder.DropTable(
                name: "Kunder");

            migrationBuilder.DropTable(
                name: "Behandlingstyper");

            migrationBuilder.DropTable(
                name: "Behandlere");

            migrationBuilder.DropTable(
                name: "Klinikker");
        }
    }
}
