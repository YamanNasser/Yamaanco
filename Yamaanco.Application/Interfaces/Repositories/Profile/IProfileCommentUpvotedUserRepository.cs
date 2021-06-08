using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Profile
{
    public interface IProfileCommentUpvotedUserRepository : IRepository<ProfileCommentUpvotedUser>
    {
        Task<CommentUpvotedDto> UpdateCommentUpvoteCommand(string commentId, string upvotedUserId);
    }
}