using Microsoft.EntityFrameworkCore;
using Yamaanco.Application.Interfaces;
using Yamaanco.Infrastructure.EF.Persistence.Context;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Context
{
    public class MsSqlYamaancoDbContext : YamaancoDbContext
    {
        public MsSqlYamaancoDbContext(DbContextOptions options, ICurrentUserService currentUserService)
         : base(options, currentUserService)
        {
        }

        public MsSqlYamaancoDbContext(DbContextOptions options)
          : base(options)
        {
        }
    }
}