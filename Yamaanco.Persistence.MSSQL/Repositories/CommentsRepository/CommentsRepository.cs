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
using Yamaanco.Application.Interfaces.Repositories.Comments;
using Yamaanco.Domain.Enums;
using Z.EntityFramework.Plus;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.CommentsRepository
{
    public class CommentsRepository : ICommentsRepository
    {
        protected readonly IYamaancoDbContext Context;

        public CommentsRepository(IYamaancoDbContext context)
        {
            Context = context;
        }


        public async Task<IList<CommentMentionsInfoDto>> GetMentionsList(string userId, int pageIndex, int pageSize, string userName, string commentId)
        {
            var commentType = await GetCommentCategoryWithoutPermissionCheck(commentId);
            var result = new List<CommentMentionsInfoDto>();

            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();

            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new
                {
                    UserId = userId,
                    CommentId = commentId,
                    UserName = $"%{userName}%",
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };

                switch (commentType)
                {
                    case CommentCategory.Profile:
                        {
                            //get all current logged-in user follower that he/she also followed.
                            result = (await db.QueryAsync<CommentMentionsInfoDto>(@"
                   select Id as UserId,UserName
                    from Profile where Id in(
                    SELECT [FollowerProfileId]
                    FROM [ProfileFollower] inner join GroupMember g on g.MemberId=[FollowerProfileId]
                    and g.GroupId in   (select gm.GroupId  from GroupMember gm where  gm.MemberId=@UserId)
                inner join Profile  on Profile.Id=[FollowerProfileId]  and UserName like @UserName
                    where [ProfileId]=@UserId
                    intersect
                    SELECT [ProfileId]
                    FROM [ProfileFollower] inner join GroupMember g on g.MemberId=[ProfileId]
                    and g.GroupId in   (select gm.GroupId  from GroupMember gm where  gm.MemberId=@UserId)
                inner join Profile  on Profile.Id=[FollowerProfileId]  and UserName like @UserName
                    where [FollowerProfileId]=@UserId ) 	
                     order by Created desc
                       OFFSET (@PageIndex-1)*@PageSize ROWS
                       FETCH NEXT @PageSize ROWS ONLY", @params)).ToList();
                        }
                        break;
                    case CommentCategory.Group:
                        {
                            //get all current logged-in user follower that belong to selected comment group.
                            result = (await db.QueryAsync<CommentMentionsInfoDto>(@"
                   select Id as UserId,UserName
                    from Profile where Id in(
                    SELECT [FollowerProfileId]
                    FROM [ProfileFollower] inner join GroupMember gm on gm.MemberId=[FollowerProfileId]
                    and gm.GroupId = (select top 1 CategoryId from vwComments where Id=@CommentId)
                inner join Profile  on Profile.Id=[FollowerProfileId]  and UserName like @UserName
                    where [ProfileId]=@UserId
                    intersect
                    SELECT [ProfileId]
                    FROM [ProfileFollower] inner join GroupMember gm on gm.MemberId=[ProfileId]
                    and gm.GroupId = (select top 1 CategoryId from vwComments where Id=@CommentId)
                inner join Profile  on Profile.Id=[FollowerProfileId]  and UserName like @UserName
                    where [FollowerProfileId]=@UserId )
                        order by Created desc
                       OFFSET (@PageIndex-1)*@PageSize ROWS
                       FETCH NEXT @PageSize ROWS ONLY", @params)).ToList();

                        }
                        break;
                }
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
            return result;

        }

        public async Task<IList<CommentDto>> FindCommentsByHashtag(string userId, int pageIndex, int pageSize, string hashtag)
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
                    UserId = userId,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    Hashtag = hashtag
                };

                var comments = await db.QueryAsync<CommentDto, CommentPingsDto, ResourcesBasicInfoDto, CommentDto>(@" exec [spFindCommentsByHashtag] @UserId,@Hashtag,@PageSize,@PageIndex", (comment, pings, resource) =>
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

        public async Task<IList<CommentDto>> FindComments(string userId, int pageIndex, int pageSize, string contains, DateTime? fromDate, DateTime? toDate, string creatorName, string categoryName)
        {
            //User Group comments
            //User profile comments

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
                    UserId = userId,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    Contains = contains != null ? $"%{contains}%" : null,
                    FromDate = fromDate,
                    ToDate = toDate,
                    CreatorName = creatorName != null ? $"%{creatorName}%" : null,
                    CategoryName = categoryName != null ? $"%{categoryName}%" : null,
                };

                var comments = await db.QueryAsync<CommentDto, CommentPingsDto, ResourcesBasicInfoDto, CommentDto>(@" exec [spFindComments] @UserId,@PageSize,@PageIndex ,@Contains, @FromDate,@ToDate,@CreatorName,@CategoryName", (comment, pings, resource) =>
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

        public async Task<IList<CommentDto>> GetComments(string userId, int pageIndex, int pageSize)
        {
            //User Group comments
            //User profile comments

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
                    UserId = userId,
                    PageSize = pageSize,
                    PageIndex = pageIndex
                };

                var comments = await db.QueryAsync<CommentDto, CommentPingsDto, ResourcesBasicInfoDto, CommentDto>(@" exec [spViewComments] @UserId,@PageSize,@PageIndex ", (comment, pings, resource) =>
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

        public async Task<IList<CommentDto>> GetCommentIncludeReplies(string userId, string commentId)
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
                    UserId = userId,
                    CommentId = commentId
                };

                var comments = await db.QueryAsync<CommentDto, CommentPingsDto, ResourcesBasicInfoDto, CommentDto>(@" exec [spViewSpesificCommentIncludeReplies] @UserId,@CommentId ", (comment, pings, resource) =>
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

        public async Task<CommentCategory> GetCommentCategory(string userId, string commentId)
        {
            CommentCategory commentCategory = CommentCategory.NotFound;
            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();

            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new
                {
                    UserId = userId,
                    CommentId = commentId
                };
                //here we get the comment type an ensure the user have permission to see it i.e. 
                commentCategory = await db.QuerySingleOrDefaultAsync<CommentCategory>(@"
                                    SELECT distinct Category
                                    FROM [vwComments]
                                    where Id=@CommentId and @CommentId in (
                                             select distinct id
				                                    from vwComments c
				                                    where c.CategoryId in
						                                    (select GroupId
							                                    from [GroupMember]
							                                    where MemberId=@UserId) or       c.CategoryId=@UserId) ", @params);
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

            return commentCategory;
        }


        public async Task<CommentCategory> GetCommentCategoryWithoutPermissionCheck(string commentId)
        {
            CommentCategory commentCategory = CommentCategory.NotFound;
            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();

            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new
                {
                    CommentId = commentId
                };
                //here we get the comment type an ensure the user have permission to see it i.e. 
                commentCategory = await db.QuerySingleOrDefaultAsync<CommentCategory>(@"
                                    SELECT [Type]
                                    FROM [vwCommentsType]
                                    where Id=@CommentId ", @params);
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

            return commentCategory;
        }


    }
}