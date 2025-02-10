using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Comment.Migrations
{
    public partial class RenameFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "UserComments",
                newName: "NameSurname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "UserComments",
                newName: "FullName");
        }
    }
}
