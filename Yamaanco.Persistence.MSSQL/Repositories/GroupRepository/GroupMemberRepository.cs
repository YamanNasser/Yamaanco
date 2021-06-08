using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupMemberRepository : Repository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<int> DeleteGroupMember(string groupId, string memberId)
        {
            var numberOfMembers = 0;

            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new { groupId, memberId };

                numberOfMembers = await db.QueryFirstAsync<int>(" [spDeleteGroupMember] @groupId,@memberId ", @params);
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

            return numberOfMembers;
        }

        public async Task<CreatedGroupMemberBasicInfoDto> CreateGroupMember(string groupId, string memberId)
        {
            CreatedGroupMemberBasicInfoDto groupMemberBasicInfoDto;
            IDbConnection db = Context.YamaacoDatabase
                  .GetDbConnection();

            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var @params = new { groupId, memberId };

                groupMemberBasicInfoDto = await db.QueryFirstAsync<CreatedGroupMemberBasicInfoDto>(" [spCreateGroupMember] @groupId,@memberId ", @params);
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

            return groupMemberBasicInfoDto;
        }

        //TODO: Check performance
        public async Task<IList<GroupMemberDto>> GetGroupMembers(string groupId, string memberNameContains, string currentUserId, int skip, int take)
        {
            var groupMembers = await Context
                            .GroupMember
                            .AsNoTracking()
                            .Include(o => o.Member)
                            .ThenInclude(o => o.Gender)
                            .Where(o => o.GroupId == groupId)
                            .Where(o => o.Member.UserName.Contains(memberNameContains))
                            .OrderByDescending(o => o.JoinDate)
                            .Skip(skip)
                            .Take(take)
                            .Include(o => o.Member)
                            .ThenInclude(o => o.PhotoResources)
                            .Include(o => o.Member)
                            .ThenInclude(o => o.Followers)
                            .Select(o =>
                           new GroupMemberDto
                           {
                               MemberName = o.Member.UserName,
                               Gender = o.Member.Gender,
                               MemberId = o.MemberId,
                               JoinDate = o.JoinDate,
                               BirthDate = o.Member.BirthDate,
                               City = o.Member.City,
                               Country = o.Member.Country,
                               Email = o.Member.Email,
                               PhoneNumber = o.Member.PhoneNumber,
                               IsAdmin = o.IsAdmin,
                               GroupId = o.GroupId,
                               IsCurrentUserFollowedTheMember =
                               o.Member.Followers
                               .Any(o => o.FollowerProfileId == currentUserId),
                               ProfilePhotoResources =
                               o.Member.PhotoResources
                                .Where(f => f.Length > 0)
                               .Select(f => new PhotoResourcesDto()
                               {
                                   Path = f.FullPath,
                                   PhotoSize = f.PhotoSize
                               })
                           }).ToListAsync();

            return groupMembers;
        }

        public async Task<IList<string>> GetUnMentiondMembersIdList(IList<string> userIdPings, string groupId, string creatorId)
        {
            if (userIdPings.Count() > 0 && !string.IsNullOrEmpty(groupId))
            {
                var members = await Context.GroupMember
                    .Where(o => o.GroupId == groupId)
                    .Where(o => o.MemberId == creatorId)
                    .Where(o => !userIdPings.Contains(o.MemberId))
                    .Select(o => o.MemberId)
                    .AsNoTracking()
                    .ToListAsync();

                return members;
            }
            return new List<string>();
        }
    }
}