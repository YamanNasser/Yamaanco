using Microsoft.EntityFrameworkCore.Migrations;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Migrations.YamaancoDb
{
    public partial class SeedGroupType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(" insert into GroupType  (Name) values ('Public');");
            migrationBuilder.Sql(" insert into GroupType  (Name) values ('Private');");
            migrationBuilder.Sql(" insert into GroupType  (Name) values ('Hidden');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
