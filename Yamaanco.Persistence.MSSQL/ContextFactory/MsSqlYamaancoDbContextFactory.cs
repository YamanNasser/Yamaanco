using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Yamaanco.Infrastructure.EF.Persistence.Context;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Context;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.ContextFactory
{
    /*
     * https://codingblast.com/entityframework-core-idesigntimedbcontextfactory/
     * Problem:
     * Your DbContext in a separate project – class library project. You are trying to add new migration and update database.
     *
     * Solution:
     * So you need to implement this interface, and you are not sure how to do it.
     * You can add a class that implements IDesignTimeDbContextFactory inside of your Web project.
     */

    public class MsSqlYamaancoDbContextFactory : IDesignTimeDbContextFactory<MsSqlYamaancoDbContext>
    {
        public MsSqlYamaancoDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.local.json", true)
                .Build();

            var builder = new DbContextOptionsBuilder<YamaancoDbContext>();
            builder.UseSqlServer(
                config.GetConnectionString(nameof(YamaancoDbContext)),
                b => b.MigrationsAssembly("Yamaanco.Infrastructure.EF.Persistence.MSSQL")
            );
            return new MsSqlYamaancoDbContext(builder.Options);
        }
    }
}