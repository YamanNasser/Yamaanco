using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.Interfaces.Repositories.Comments
{
    public interface ICommentsRepository
    {
        Task<IList<CommentDto>> GetCommentIncludeReplies(string userId, string commentId);

        Task<IList<CommentDto>> FindComments(string userId, int pageIndex, int pageSize, string contains, DateTime? fromDate, DateTime? toDate, string creatorName, string categoryName);

        Task<CommentCategory> GetCommentCategory(string userId, string commentId);
        Task<IList<CommentDto>> GetComments(string userId, int pageIndex, int pageSize);
        Task<IList<CommentDto>> FindCommentsByHashtag(string userId, int pageIndex, int pageSize, string hashtag);
   
        Task<CommentCategory> GetCommentCategoryWithoutPermissionCheck(string commentId);
        Task<IList<CommentMentionsInfoDto>> GetMentionsList(string userId, int pageIndex, int pageSize, string userName, string commentId);
    }
}