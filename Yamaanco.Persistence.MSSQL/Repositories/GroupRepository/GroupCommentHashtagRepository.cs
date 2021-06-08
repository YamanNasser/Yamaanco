using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupCommentHashtagRepository : Repository<GroupCommentHashtag>, IGroupCommentHashtagRepository
    {
        public GroupCommentHashtagRepository(IYamaancoDbContext context)
            : base(context)
        {
        }
    }
}