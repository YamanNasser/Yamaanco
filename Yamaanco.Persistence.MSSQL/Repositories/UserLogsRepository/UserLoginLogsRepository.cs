using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.UserLogs;
using Yamaanco.Domain.Entities.UserLogsEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.UserLogsRepository
{
    public class UserLoginLogsRepository : Repository<UserLoginLogs>, IUserLoginLogsRepository
    {
        public UserLoginLogsRepository(IYamaancoDbContext context)
            : base(context)
        {
        }
    }
}