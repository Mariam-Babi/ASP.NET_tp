using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotellerie_X.Migrations
{
    public partial class AjoutNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Note",
                table: "Appreciations",
                type: "int",
                nullable: false,
                defaultValue: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Note", table: "Appreciations");
        }
    }
}
