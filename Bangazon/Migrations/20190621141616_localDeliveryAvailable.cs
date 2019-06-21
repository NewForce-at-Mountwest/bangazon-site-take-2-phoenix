using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class localDeliveryAvailable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "localDeliveryAvailable",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3a5b6d64-aa91-489e-9168-184fdea5c706", "AQAAAAEAACcQAAAAEBXP6HEtxTfPqTOZmPGoWfIX6MJy6p2Ww0WCdrrJmMPXz7hXBwXomm0PUf62xpdvwQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "localDeliveryAvailable",
                table: "Product");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d2d8ac9d-c8df-48d0-b12b-77db87e4133d", "AQAAAAEAACcQAAAAEMVTbm3LiIcxxHfkeAoMwvgVhC8Y8a0kZ2Y7sJsk9E+O5oSy8anyI1fTCaLD7Yt3kA==" });
        }
    }
}
