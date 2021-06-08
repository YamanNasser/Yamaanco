using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Comment;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;
using Z.EntityFramework.Plus;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupCommentRepository : Repository<GroupComment>, IGroupCommentRepository
    {
        public GroupCommentRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<GroupComment> GetGroupComment(string id)
        {
            var comments = await (from comment in Context.GroupComment
                         .Include(o => o.Pings)
                         .ThenInclude(o => o.MentionedUser)
                         .Include(o => o.CommentResources)
                         .Include(o => o.Hashtags)
                         .Include(o => o.Group)
                         .Include(o => o.UpvotedUsers)
                                  join creatorGroup in Context.Profile
                                  .Include(o => o.PhotoResources) on comment.CreatedById equals creatorGroup.Id
                                  where comment.Id == id
                                  select comment)
                                  .SingleOrDefaultAsync();

            return comments;
        }

        public async Task<CommentDto> GetCommentWithoutReplies(string commentId)
        {
            var comments = await (from comment in Context.GroupComment
                          .Where(o => o.Id == commentId)
                          .Include(o => o.Pings).ThenInclude(o => o.MentionedUser)
                          .Include(o => o.CommentResources)
                          .Include(o => o.Group)
                          .Include(o => o.UpvotedUsers)
                                  join creatorGroup in Context.Profile
                                  .Include(o => o.PhotoResources) on comment.CreatedById equals creatorGroup.Id
                                  select new CommentDto()
                                  {
                                      Category = CommentCategory.Group,
                                      CategoryId = comment.GroupId,
                                      CategoryName = comment.Group.Name,
                                      Content = comment.Content,
                                      Created = comment.Created,
                                      CreatedById = comment.CreatedById,
                                      CreatorName = creatorGroup.UserName,
                                      Id = comment.Id,
                                      Modified = comment.LastModified,
                                      ProfilePictureUrl = creatorGroup.PhotoResources
                                      .First(o => o.PhotoSize == PhotoSize.Small).Path,
                                      UpvoteCount = comment.UpvoteCount,
                                      ViewCount = comment.ViewCount,
                                      Parent = comment.Parent,
                                      Root = comment.Root,
                                      Pings = comment.Pings
                                      .Select(o => new CommentPingsDto()
                                      {
                                          UserName = o.MentionedUser.UserName,
                                          UserId = o.MentionedUserId
                                      }).ToList(),
                                      Attachments = comment.CommentResources
                                      .Select(o => new ResourcesBasicInfoDto()
                                      {
                                          Extension = o.Extension,
                                          FileId = o.Id,
                                          Path = o.Path
                                      }).ToList()
                                  }).AsNoTracking()
                                  .SingleOrDefaultAsync();
            return comments;
        }

        public async Task<IList<CommentDto>> GetComments(string groupId, string userId, int pageIndex, int pageSize)
        {
            var commentList = new List<CommentDto>();
            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();

            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new
                {
                    GroupId = groupId,
                    PageSize = pageSize,
                    UserId = userId,
                    PageIndex = pageIndex
                };

                var comments = await db.QueryAsync<CommentDto, CommentPingsDto, ResourcesBasicInfoDto, CommentDto>(@" exec [spViewGroupComments] @GroupId,@UserId,@PageSize,@PageIndex ", (comment, pings, resource) =>
                {
                    if (pings != null)
                    {
                        comment.Pings = comment.Pings.Concat(new[] { pings });
                    }

                    if (resource != null)
                    {
                        comment.Attachments = comment.Attachments.Concat(new[] { resource });
                    }
                    return comment;
                }, param: @params, splitOn: "UserId,FileId");

                var result = comments.GroupBy(p => p.Id).Select(c =>
                {
                    var comment = c.First();

                    var pings = c.Select(o => o.Pings.SingleOrDefault())
                    .ToList();

                    if (pings != null && pings[0] != null)
                    {
                        comment.Pings = pings
                    .GroupBy(o => o.UserId)
                    .Distinct()
                    .Select(o => o.First())
                    .ToList();
                    }

                    var attachments = c.Select(o => o.Attachments.SingleOrDefault())
                    .ToList();

                    if (attachments != null && attachments[0] != null)
                    {
                        comment.Attachments = attachments
                    .GroupBy(o => o.FileId)
                    .Distinct()
                    .Select(o => o.First())
                    .ToList();
                    }

                    return comment;
                });

                commentList = result?.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }

            return commentList;
        }
    }
}