using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Server.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
       
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "SupperAdmin", "SupperAdmin".ToUpper(), Guid.NewGuid().ToString() });
           
         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Role");
        }
    }
}
