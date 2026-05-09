using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotellerie_X.Migrations
{
    public partial class AjoutPaysHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pays",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Tunisie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Pays", table: "Hotels");
        }
    }
}
