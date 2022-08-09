using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Migrations
{
    public partial class addedseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Address", "Age", "Name", "PasswordHash", "PhoneNumber", "Username" },
                values: new object[] { new Guid("d226b8fb-aead-4f2e-8cd7-b9536b550b79"), "Address", 22, "Muhammad", "473287F8298DBA7163A897908958F7C0EAE733E25D2E027992EA2EDC9BED2FA8", "2351234512", "admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "Age", "Name", "PasswordHash", "PhoneNumber", "Username" },
                values: new object[] { new Guid("1777d9e4-e78f-4ba4-b3cc-8df26f145d59"), "Address", 22, "Muhammad", "473287F8298DBA7163A897908958F7C0EAE733E25D2E027992EA2EDC9BED2FA8", "2351234512", "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("d226b8fb-aead-4f2e-8cd7-b9536b550b79"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1777d9e4-e78f-4ba4-b3cc-8df26f145d59"));
        }
    }
}
