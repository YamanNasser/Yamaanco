using System.Collections.Generic;
using System.Linq;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileCommentTransactionRepository : Repository<ProfileCommentTransaction>, IProfileCommentTransactionRepository
    {
        public ProfileCommentTransactionRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public IList<string> GetCommentParticipantsIdList(string commentId, string categoryId, string[] pings)
        {
            if (pings != null && pings.Any())
            {//TODO:Check this again.
                var participants = Context.ProfileCommentTransaction
                                   .Where(o => o.CommentRoot == commentId)
                                   .Where(o => o.CommentTransactionType
                                   == CommentTransactionType.Add)
                                   .Where(o => o.ProfileId != null)//ProfileId Is Not Null
                                   .Where(o => !pings.Contains(categoryId))//and not mentioned
                                   .Select(o => o.ProfileId)//not profile owner
                                   .Distinct();

                return participants.ToList();
            }
            return new List<string>();
        }
    }
}