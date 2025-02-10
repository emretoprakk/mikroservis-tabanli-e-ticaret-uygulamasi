using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Order.Persistence.Migrations
{
    public partial class RemoveAddressIdFromOrderings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderings_Addresses_AddressId",
                table: "Orderings");

            migrationBuilder.DropIndex(
                name: "IX_Orderings_AddressId",
                table: "Orderings");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orderings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Orderings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orderings_AddressId",
                table: "Orderings",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderings_Addresses_AddressId",
                table: "Orderings",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
