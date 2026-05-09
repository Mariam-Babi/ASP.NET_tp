using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotellerie_X.Migrations
{
    public partial class AjoutTel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Tel", table: "Hotels");
        }
    }
}
