using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupCommentUpvotedUserRepository : Repository<GroupCommentUpvotedUser>, IGroupCommentUpvotedUserRepository
    {
        public GroupCommentUpvotedUserRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<CommentUpvotedDto> UpdateCommentUpvoteCommand(string commentId, string upvotedUserId)
        {
            CommentUpvotedDto commentUpvoted;
            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed) db.Open();

                var @params = new { commentId, upvotedUserId };

                commentUpvoted = await db.QueryFirstAsync<CommentUpvotedDto>(@" [spUpdateGroupCommentUpvoteCommand] @CommentId,@UpvotedUserId", @params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
            return commentUpvoted;
        }
    }
}