using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class addedcontroller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d2d8ac9d-c8df-48d0-b12b-77db87e4133d", "AQAAAAEAACcQAAAAEMVTbm3LiIcxxHfkeAoMwvgVhC8Y8a0kZ2Y7sJsk9E+O5oSy8anyI1fTCaLD7Yt3kA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dc3c7ff1-ce43-48c5-807c-e0957ccbe24f", "AQAAAAEAACcQAAAAEPUoBH8VZImIVkd+NzW8YV/VaYN3CD0OvmLisCuwWqRkfB2wwiJh0Xq755o+/Xhbyw==" });
        }
    }
}
