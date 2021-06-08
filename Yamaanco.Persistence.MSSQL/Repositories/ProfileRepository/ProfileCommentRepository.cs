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
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;
using Z.EntityFramework.Plus;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileCommentRepository : Repository<ProfileComment>, IProfileCommentRepository
    {
        public ProfileCommentRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<ProfileComment> GetProfileCommentIncludePhotoResources(string id)
        {
            var comments = await (from comment in Context.ProfileComment
                         .Include(o => o.Pings)
                         .ThenInclude(o => o.MentionedUser)
                         .Include(o => o.CommentResources)
                         .Include(o => o.Profile)
                         .Include(o => o.UpvotedUsers)
                                  join creatorProfile in Context.Profile
                                  .Include(o => o.PhotoResources) on comment.CreatedById equals creatorProfile.Id
                                  where comment.Id == id
                                  select comment)
                                  .SingleOrDefaultAsync();

            return comments;
        }

        public async Task<CommentDto> GetCommentWithoutReplies(string id)
        {
            var comments = await (from comment in Context.ProfileComment
                          .Where(o => o.Id == id)
                          .Include(o => o.Pings).ThenInclude(o => o.MentionedUser)
                          .Include(o => o.CommentResources)
                          .Include(o => o.Profile)
                          .Include(o => o.UpvotedUsers)
                                  join creatorProfile in Context.Profile
                                  .Include(o => o.PhotoResources) on comment.CreatedById equals creatorProfile.Id
                                  select new CommentDto()
                                  {
                                      Category = CommentCategory.Profile,
                                      CategoryId = comment.ProfileId,
                                      CategoryName = comment.Profile.UserName,
                                      Content = comment.Content,
                                      Created = comment.Created,
                                      CreatedById = comment.CreatedById,
                                      CreatorName = creatorProfile.UserName,
                                      Id = comment.Id,
                                      Modified = comment.LastModified,
                                      ProfilePictureUrl = creatorProfile.PhotoResources
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
                                      }),
                                      Attachments = comment.CommentResources
                                      .Select(o => new ResourcesBasicInfoDto()
                                      {
                                          Extension = o.Extension,
                                          FileId = o.Id,
                                          Path = o.Path
                                      })
                                  }).AsNoTracking()
                                  .SingleOrDefaultAsync();
            return comments;
        }

        public async Task<IList<CommentDto>> GetComments(string profileId, string userId, int pageIndex, int pageSize)
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
                    ProfileId = profileId,
                    UserId = userId,
                    PageSize = pageSize,
                    PageIndex = pageIndex
                };

                var comments = await db.QueryAsync<CommentDto, CommentPingsDto, ResourcesBasicInfoDto, CommentDto>(@" exec [spViewProfileComments] @ProfileId,@UserId,@PageSize,@PageIndex ", (comment, pings, resource) =>
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