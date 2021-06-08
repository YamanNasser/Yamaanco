﻿using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupMemberRequestRepository : Repository<GroupMemberRequest>, IGroupMemberRequestRepository
    {
        public GroupMemberRequestRepository(IYamaancoDbContext context)
            : base(context)
        {
        }
    }
}