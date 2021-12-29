using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrzavaVakcina_Vakcina_PodrzanoID",
                table: "DrzavaVakcina");

            migrationBuilder.RenameColumn(
                name: "PodrzanoID",
                table: "DrzavaVakcina",
                newName: "PodrzaneVakcineID");

            migrationBuilder.RenameIndex(
                name: "IX_DrzavaVakcina_PodrzanoID",
                table: "DrzavaVakcina",
                newName: "IX_DrzavaVakcina_PodrzaneVakcineID");

            migrationBuilder.AddForeignKey(
                name: "FK_DrzavaVakcina_Vakcina_PodrzaneVakcineID",
                table: "DrzavaVakcina",
                column: "PodrzaneVakcineID",
                principalTable: "Vakcina",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrzavaVakcina_Vakcina_PodrzaneVakcineID",
                table: "DrzavaVakcina");

            migrationBuilder.RenameColumn(
                name: "PodrzaneVakcineID",
                table: "DrzavaVakcina",
                newName: "PodrzanoID");

            migrationBuilder.RenameIndex(
                name: "IX_DrzavaVakcina_PodrzaneVakcineID",
                table: "DrzavaVakcina",
                newName: "IX_DrzavaVakcina_PodrzanoID");

            migrationBuilder.AddForeignKey(
                name: "FK_DrzavaVakcina_Vakcina_PodrzanoID",
                table: "DrzavaVakcina",
                column: "PodrzanoID",
                principalTable: "Vakcina",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
