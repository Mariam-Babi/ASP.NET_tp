using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoManager_X.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "resto");

            migrationBuilder.CreateTable(
                name: "TProprietaire",
                schema: "resto",
                columns: table => new
                {
                    Numero = table.Column<int>(type: "int", nullable: false),
                    NomProp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailProp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GsmProp = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TProprietaire", x => x.Numero);
                });

            migrationBuilder.CreateTable(
                name: "TRestaurant",
                schema: "resto",
                columns: table => new
                {
                    CodeResto = table.Column<int>(type: "int", nullable: false),
                    NomResto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SpecResto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Tunisienne"),
                    VilleResto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TelResto = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    NumProp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRestaurant", x => x.CodeResto);
                    table.ForeignKey(
                        name: "Relation_Proprio_Restos",
                        column: x => x.NumProp,
                        principalSchema: "resto",
                        principalTable: "TProprietaire",
                        principalColumn: "Numero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRestaurant_NumProp",
                schema: "resto",
                table: "TRestaurant",
                column: "NumProp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TRestaurant", schema: "resto");
            migrationBuilder.DropTable(name: "TProprietaire", schema: "resto");
        }
    }
}
