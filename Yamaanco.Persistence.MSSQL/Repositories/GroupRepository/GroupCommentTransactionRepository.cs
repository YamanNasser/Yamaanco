using System.Collections.Generic;
using System.Linq;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupCommentTransactionRepository : Repository<GroupCommentTransaction>, IGroupCommentTransactionRepository
    {
        public GroupCommentTransactionRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        //TODO:Remove this method
        public IList<string> GetCommentParticipantsIdListExceptCreator(string commentId, string[] pings, string creatorId)
        {
            if (pings != null && pings.Any())
            {
                var participants = Context.GroupCommentTransaction
                                .Where(o => o.CommentRoot == commentId)
                                .Where(o => o.CommentTransactionType
                                == CommentTransactionType.Add)
                               .Where(o => !pings.Contains(o.UserId))
                               .Where(o => o.UserId != creatorId)
                               .Select(o => o.UserId)
                               .Distinct();
                return participants.ToList();
            }
            return new List<string>();
        }
    }
}