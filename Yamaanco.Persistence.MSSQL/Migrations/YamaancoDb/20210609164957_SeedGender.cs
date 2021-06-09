using Microsoft.EntityFrameworkCore.Migrations;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Migrations.YamaancoDb
{
    public partial class SeedGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(" insert into  Gender (Name) values ('Male');");
            migrationBuilder.Sql(" insert into  Gender (Name) values ('Female');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
