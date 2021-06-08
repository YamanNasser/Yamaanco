using System.Collections.Generic;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Group
{
    public interface IGroupCommentTransactionRepository : IRepository<GroupCommentTransaction>
    {
        IList<string> GetCommentParticipantsIdListExceptCreator(string commentId, string[] pings, string creatorId);
    }
}