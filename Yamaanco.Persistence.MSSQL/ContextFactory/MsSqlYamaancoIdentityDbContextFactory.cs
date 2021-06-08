using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Context;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.ContextFactory
{
    public class MsSqlYamaancoIdentityDbContextFactory : IDesignTimeDbContextFactory<YamaancoIdentityDbContext>
    {
        public YamaancoIdentityDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.local.json", true)
                .Build();

            var builder = new DbContextOptionsBuilder<YamaancoIdentityDbContext>();
            builder.UseSqlServer(
                config.GetConnectionString(nameof(YamaancoIdentityDbContext)),
                b => b.MigrationsAssembly("Yamaanco.Infrastructure.EF.Persistence.MSSQL")
            );
            return new YamaancoIdentityDbContext(builder.Options);
        }
    }
}