using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class bla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9b89766c-f007-420d-b843-2d15f92cb250"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Cpf", "Email", "Name", "Phone" },
                values: new object[] { new Guid("af615154-40e1-4c19-9bb7-993e9772c02b"), "111.111.111-11", "luizhlelis@gmail.com", "Luiz Lelis", "(31)99999-9999" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("af615154-40e1-4c19-9bb7-993e9772c02b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Cpf", "Email", "Name", "Phone" },
                values: new object[] { new Guid("9b89766c-f007-420d-b843-2d15f92cb250"), "111.111.111-11", "luizhlelis@gmail.com", "Luiz Lelis", "(31)99999-9999" });
        }
    }
}
