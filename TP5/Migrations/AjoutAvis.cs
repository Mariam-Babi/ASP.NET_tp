using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoManager_X.Migrations
{
    public partial class AjoutAvis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "admin");

            migrationBuilder.CreateTable(
                name: "TAvis",
                schema: "admin",
                columns: table => new
                {
                    CodeAvis = table.Column<int>(type: "int", nullable: false),
                    NomPersonne = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Note = table.Column<int>(type: "int", nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NumResto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAvis", x => x.CodeAvis);
                    table.ForeignKey(
                        name: "Relation_Resto_Avis",
                        column: x => x.NumResto,
                        principalSchema: "resto",
                        principalTable: "TRestaurant",
                        principalColumn: "CodeResto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAvis_NumResto",
                schema: "admin",
                table: "TAvis",
                column: "NumResto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TAvis", schema: "admin");
        }
    }
}
