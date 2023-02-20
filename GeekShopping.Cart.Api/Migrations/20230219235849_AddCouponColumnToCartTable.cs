using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.Cart.Api.Migrations
{
    public partial class AddCouponColumnToCartTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "coupon",
                table: "cart",
                type: "varchar(6)",
                maxLength: 6,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coupon",
                table: "cart");
        }
    }
}
