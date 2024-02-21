using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGarage.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Services",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsDeleted", "IsEmployee", "JoinDate", "PasswordHash", "PhoneNumber", "Username" },
                values: new object[] { 1, "user1@example.com", false, false, new DateTime(2024, 2, 21, 4, 8, 57, 718, DateTimeKind.Local).AddTicks(413), "Test123!", "1234567890", "exampleuser1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsDeleted", "IsEmployee", "JoinDate", "PasswordHash", "PhoneNumber", "Username" },
                values: new object[] { 2, "user2@example.com", false, false, new DateTime(2024, 2, 21, 4, 8, 57, 718, DateTimeKind.Local).AddTicks(447), "Test123!", "9876543210", "exampleuser2" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Name", "Price", "UserId", "isDeleted" },
                values: new object[,]
                {
                    { 1, "Oil Change", 50.0, 1, false },
                    { 2, "Tire Rotation", 30.0, 2, false }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CarLicencePlate", "CarMake", "CarModel", "CarVin", "CarYear", "IsDeleted", "userId" },
                values: new object[,]
                {
                    { 1, "ABC123", "Toyota", "Camry", "ABC123456DEF78901", 2018, false, 1 },
                    { 2, "XYZ789", "Honda", "Accord", "DEF987654ABC12345", 2019, false, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Services",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
