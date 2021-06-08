using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Domain.Entities.GroupEntities;

namespace Yamaanco.Application.Interfaces.Repositories.Group
{
    public interface IGroupCommentRepository : IRepository<GroupComment>
    {
        Task<CommentDto> GetCommentWithoutReplies(string commentId);

        Task<IList<CommentDto>> GetComments(string groupId, string userId, int pageIndex, int pageSize);

        Task<GroupComment> GetGroupComment(string id);
    }
}