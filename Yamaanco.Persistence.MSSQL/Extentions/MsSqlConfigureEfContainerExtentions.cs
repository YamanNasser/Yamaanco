using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yamaanco.Application.Interfaces;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Context;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Extensions;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Services;
using Yamaanco.Infrastructure.EF.Persistence.Context;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Context;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Extentions
{
    public static class MsSqlConfigureEfContainerExtentions
    {
        public static void AddMsSqlIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var builder = new DbContextOptionsBuilder<YamaancoIdentityDbContext>();
            builder.UseSqlServer(
                config.GetConnectionString(nameof(YamaancoIdentityDbContext)),
                b => b.MigrationsAssembly("Yamaanco.Infrastructure.EF.Persistence.MSSQL"));

            services.AddScoped(db => new YamaancoIdentityDbContext(builder.Options));

            services.AddIdentityInfrastructure(config);
        }

        public static void AddMsSqlInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var builder = new DbContextOptionsBuilder<YamaancoDbContext>();
            builder.UseSqlServer(
                config.GetConnectionString(nameof(YamaancoDbContext)),
                b => b.MigrationsAssembly("Yamaanco.Infrastructure.EF.Persistence.MSSQL"));

            services.AddScoped<IYamaancoDbContext>(db => new MsSqlYamaancoDbContext(builder.Options, new CurrentUserService(new HttpContextAccessor())));

            services.AddInfrastructure();
        }
    }
}