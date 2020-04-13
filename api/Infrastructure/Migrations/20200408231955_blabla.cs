using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class blabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("af615154-40e1-4c19-9bb7-993e9772c02b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Cpf", "Email", "Name", "Phone" },
                values: new object[] { new Guid("5e2f7696-06b0-4d6e-a67a-f7da1c088e0f"), "111.111.111-11", "luizhlelis@gmail.com", "Luiz Lelis", "(31)99999-9999" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5e2f7696-06b0-4d6e-a67a-f7da1c088e0f"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Cpf", "Email", "Name", "Phone" },
                values: new object[] { new Guid("af615154-40e1-4c19-9bb7-993e9772c02b"), "111.111.111-11", "luizhlelis@gmail.com", "Luiz Lelis", "(31)99999-9999" });
        }
    }
}
