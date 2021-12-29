using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pasos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Drzavljanstvo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vakcina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Proizvodjac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Doza = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vakcina", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DrzavaPasos",
                columns: table => new
                {
                    PodrzaneDrzaveID = table.Column<int>(type: "int", nullable: false),
                    PodrzaniPasosiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrzavaPasos", x => new { x.PodrzaneDrzaveID, x.PodrzaniPasosiID });
                    table.ForeignKey(
                        name: "FK_DrzavaPasos_Drzava_PodrzaneDrzaveID",
                        column: x => x.PodrzaneDrzaveID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrzavaPasos_Pasos_PodrzaniPasosiID",
                        column: x => x.PodrzaniPasosiID,
                        principalTable: "Pasos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrzavaTest",
                columns: table => new
                {
                    PodrzaneDrzaveID = table.Column<int>(type: "int", nullable: false),
                    PodrzaniTestoviID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrzavaTest", x => new { x.PodrzaneDrzaveID, x.PodrzaniTestoviID });
                    table.ForeignKey(
                        name: "FK_DrzavaTest_Drzava_PodrzaneDrzaveID",
                        column: x => x.PodrzaneDrzaveID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrzavaTest_Test_PodrzaniTestoviID",
                        column: x => x.PodrzaniTestoviID,
                        principalTable: "Test",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrzavaVakcina",
                columns: table => new
                {
                    PodrzaneDrzaveID = table.Column<int>(type: "int", nullable: false),
                    PodrzanoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrzavaVakcina", x => new { x.PodrzaneDrzaveID, x.PodrzanoID });
                    table.ForeignKey(
                        name: "FK_DrzavaVakcina_Drzava_PodrzaneDrzaveID",
                        column: x => x.PodrzaneDrzaveID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrzavaVakcina_Vakcina_PodrzanoID",
                        column: x => x.PodrzanoID,
                        principalTable: "Vakcina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrzavaPasos_PodrzaniPasosiID",
                table: "DrzavaPasos",
                column: "PodrzaniPasosiID");

            migrationBuilder.CreateIndex(
                name: "IX_DrzavaTest_PodrzaniTestoviID",
                table: "DrzavaTest",
                column: "PodrzaniTestoviID");

            migrationBuilder.CreateIndex(
                name: "IX_DrzavaVakcina_PodrzanoID",
                table: "DrzavaVakcina",
                column: "PodrzanoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrzavaPasos");

            migrationBuilder.DropTable(
                name: "DrzavaTest");

            migrationBuilder.DropTable(
                name: "DrzavaVakcina");

            migrationBuilder.DropTable(
                name: "Pasos");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Drzava");

            migrationBuilder.DropTable(
                name: "Vakcina");
        }
    }
}
