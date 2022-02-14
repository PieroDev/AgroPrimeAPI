using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace AgroPrimeAPI.Migrations
{
    public partial class RunSqlScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(Environment.CurrentDirectory, @"Migrations", "storedProcedure.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
