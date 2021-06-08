using System.Collections.Generic;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Profile
{
    public interface IProfileCommentTransactionRepository : IRepository<ProfileCommentTransaction>
    {
        IList<string> GetCommentParticipantsIdList(string commentId, string categoryId, string[] pings);
    }
}