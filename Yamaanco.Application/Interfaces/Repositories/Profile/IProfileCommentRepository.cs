using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Profile
{
    public interface IProfileCommentRepository : IRepository<ProfileComment>
    {
        Task<ProfileComment> GetProfileCommentIncludePhotoResources(string id);

        Task<CommentDto> GetCommentWithoutReplies(string commentId);

        Task<IList<CommentDto>> GetComments(string profileId, string userId, int pageIndex, int pageSize);
    }
}