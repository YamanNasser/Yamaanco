using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yamaanco.Infrastructure.EF.Persistence.Context;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Context;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Extentions
{
    public static class MsSqlServiceCollectionExtensions
    {
        public static IServiceCollection AddMssqlDbContext(
           this IServiceCollection serviceCollection,
           string connectionStringDbContext,
           IConfiguration config = null)
        {
            serviceCollection.AddDbContext<YamaancoDbContext, MsSqlYamaancoDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString(connectionStringDbContext),
                    b => b.MigrationsAssembly("Yamaanco.Infrastructure.EF.Persistence.MSSQL"));
            });

            return serviceCollection;
        }
    }
}