using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API3.Migrations
{
    public partial class thsmigratin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "Achternaam", "ConcurrencyStamp", "Email", "EmailConfirmed", "Geboortedatum", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Voornaam" },
                values: new object[] { 1, 0, "Jaqueline", "fc4a7524-df94-4fb8-8995-b698690c3008", "Maria@gmail.com", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Maria@gmail.com", "Maria", null, null, false, "3fbdfcb0-f490-4682-a565-90ee21db7c70", false, "Maria", "Maria" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
