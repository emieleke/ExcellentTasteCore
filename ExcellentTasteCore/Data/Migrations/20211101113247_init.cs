using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExcellentTasteCore.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumptie",
                columns: table => new
                {
                    ConsumptieCode = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    ConsumptieNaam = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumptie", x => x.ConsumptieCode);
                });

            migrationBuilder.CreateTable(
                name: "Klant",
                columns: table => new
                {
                    KlantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantNaam = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Telefoon = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klant", x => x.KlantId);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptieGroep",
                columns: table => new
                {
                    ConsumptieGroepCode = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    ConsumptieCode = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    ConsumptieGroepNaam = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptieGroep", x => x.ConsumptieGroepCode);
                    table.ForeignKey(
                        name: "FK_SubConsumptie_Consumptie",
                        column: x => x.ConsumptieCode,
                        principalTable: "Consumptie",
                        principalColumn: "ConsumptieCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservering",
                columns: table => new
                {
                    ReserveringId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    klantId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime", nullable: false),
                    Tijd = table.Column<TimeSpan>(type: "time", nullable: false),
                    Tafel = table.Column<int>(type: "int", nullable: false),
                    AantalPersonen = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    DatumToegevoegd = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    BonDatum = table.Column<DateTime>(type: "datetime", nullable: false),
                    Betalingswijze = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    BonTotaal = table.Column<decimal>(type: "decimal(19,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservering", x => x.ReserveringId);
                    table.ForeignKey(
                        name: "FK_Reservering_Klant",
                        column: x => x.klantId,
                        principalTable: "Klant",
                        principalColumn: "KlantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptieItem",
                columns: table => new
                {
                    ConsumptieItemCode = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    ConsumptieGroepCode = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    ConsumptieItemNaam = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(19,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptieItem", x => x.ConsumptieItemCode);
                    table.ForeignKey(
                        name: "FK_ConsumptieItem_SubConsumptie",
                        column: x => x.ConsumptieGroepCode,
                        principalTable: "ConsumptieGroep",
                        principalColumn: "ConsumptieGroepCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bestelling",
                columns: table => new
                {
                    BestellingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReserveringId = table.Column<int>(type: "int", nullable: false),
                    ConsumptieItemCode = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    Aantal = table.Column<int>(type: "int", nullable: false),
                    DateTimeBereidingConsumptie = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Prijs = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    Totaal = table.Column<decimal>(type: "decimal(19,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestelling", x => x.BestellingId);
                    table.ForeignKey(
                        name: "FK_Bestelling_ConsumptieItem",
                        column: x => x.ConsumptieItemCode,
                        principalTable: "ConsumptieItem",
                        principalColumn: "ConsumptieItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bestelling_Reservering",
                        column: x => x.ReserveringId,
                        principalTable: "Reservering",
                        principalColumn: "ReserveringId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestelling_ConsumptieItemCode",
                table: "Bestelling",
                column: "ConsumptieItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_Bestelling_ReserveringId",
                table: "Bestelling",
                column: "ReserveringId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptieGroep_ConsumptieCode",
                table: "ConsumptieGroep",
                column: "ConsumptieCode");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptieItem_ConsumptieGroepCode",
                table: "ConsumptieItem",
                column: "ConsumptieGroepCode");

            migrationBuilder.CreateIndex(
                name: "IX_Reservering_klantId",
                table: "Reservering",
                column: "klantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bestelling");

            migrationBuilder.DropTable(
                name: "ConsumptieItem");

            migrationBuilder.DropTable(
                name: "Reservering");

            migrationBuilder.DropTable(
                name: "ConsumptieGroep");

            migrationBuilder.DropTable(
                name: "Klant");

            migrationBuilder.DropTable(
                name: "Consumptie");
        }
    }
}
