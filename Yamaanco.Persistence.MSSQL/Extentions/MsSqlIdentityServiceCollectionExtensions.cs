using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Context;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Extentions
{
    public static class MsSqlIdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddMssqlIdentityDbContext(
               this IServiceCollection serviceCollection,
                string connectionStringIdentityDbContext,
               IConfiguration config = null)
        {
            serviceCollection.AddDbContext<YamaancoIdentityDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString(connectionStringIdentityDbContext),
                    b => b.MigrationsAssembly("Yamaanco.Infrastructure.EF.Persistence.MSSQL"));
            });
            return serviceCollection;
        }
    }
}