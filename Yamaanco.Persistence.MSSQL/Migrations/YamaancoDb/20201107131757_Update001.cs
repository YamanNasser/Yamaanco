using Microsoft.EntityFrameworkCore.Migrations;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Migrations.YamaancoDb
{
    public partial class Update001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfViewer",
                table: "Group",
                newName: "NumberOfViewers");

            migrationBuilder.RenameColumn(
                name: "NumberOfMember",
                table: "Group",
                newName: "NumberOfMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfViewers",
                table: "Group",
                newName: "NumberOfViewer");

            migrationBuilder.RenameColumn(
                name: "NumberOfMembers",
                table: "Group",
                newName: "NumberOfMember");
        }
    }
}
