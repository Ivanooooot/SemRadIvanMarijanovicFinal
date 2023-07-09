using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemRadIvanMarijanovic.Migrations
{
    public partial class adduserfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e71d461-63e3-4aa5-be93-d708888888888",
                column: "ConcurrencyStamp",
                value: "9bc734ce-4e9f-4b2b-b4a2-04bc87f98067");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66412151-dd0c-4b69-82c8-0f51551515",
                column: "ConcurrencyStamp",
                value: "30913271-75d0-443a-9c46-a7bdaae74c44");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6642155-dj2c-4819-82c8-0f4151555555551",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2d23baa-4bf2-493d-bae3-a52ca081d801", "AQAAAAEAACcQAAAAEABO+fVutj39qLw7TQhXv5QZIzOEgv1ggMnOzPRW5lzIK6JNh01Kj7Ize/i8LvYk/A==", "5d860ff2-ae7a-49c5-bbd1-65b71a106b8a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e71d461-63e3-4aa5-be93-d708888888888",
                column: "ConcurrencyStamp",
                value: "83240ce8-b132-4b76-8cf2-8acf6c83f259");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66412151-dd0c-4b69-82c8-0f51551515",
                column: "ConcurrencyStamp",
                value: "46561d85-360d-4ac0-96a3-5f18db2333ae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6642155-dj2c-4819-82c8-0f4151555555551",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84fad205-924d-44ad-8cc5-b57c696d4ad4", "AQAAAAEAACcQAAAAEHt778f0DIi9F7Kr6kKVXqcb3Tz8MOzIBUK7eH1lqNR+CrYH4kjhZNmbt8kGCjzZoQ==", "c91af212-537e-493d-8e76-0c7e51856b01" });
        }
    }
}
