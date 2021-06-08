using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Group
{
    public interface IGroupCommentUpvotedUserRepository : IRepository<GroupCommentUpvotedUser>
    {
        Task<CommentUpvotedDto> UpdateCommentUpvoteCommand(string commentId, string upvotedUserId);
    }
}