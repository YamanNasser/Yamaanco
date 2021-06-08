using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;
using Z.EntityFramework.Plus;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private int[] availableForSearchTypes = new[]{
                GroupType.Public,
                GroupType.Private
            };

        private int[] allGroupTypes = new[]{
                GroupType.Public,
                GroupType.Private,
                 GroupType.Hidden
            };

        public GroupRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<GroupDto> GetGroupById(string id, string userId)
        {
            var group = await Context
                      .Group
                      .AsNoTracking()
                      .Where(o => o.Id == id)
                      .Include(o => o.PhotoResources)
                      .Include(o => o.Members)
                      .Select(group => new GroupDto
                      {
                          Description = group.Description,
                          GroupTypeId = group.GroupTypeId,
                          Id = group.Id,
                          Name = group.Name,
                          NumberOfMember = group.NumberOfMembers,
                          NumberOfViewer = group.NumberOfViewers,
                          CreatedDate = group.Created,
                          GroupPhotoResources = group.PhotoResources
                        .Select(f => new PhotoResourcesDto()
                        {
                            Path = f.FullPath,
                            PhotoSize = f.PhotoSize
                        }),
                          IsUserMemberOfGroup = group.Members
                         .Any(o => o.MemberId == userId)
                      }).SingleOrDefaultAsync();

            return group;
        }

        public async Task<IList<GroupDto>> GetGroupByNameAndIncludingResourcesOnly(string name, int skip, int take, string userId)
        {
            var groupList = await Context
                      .Group
                      .Where(o => o.Name.Contains(name))
                      .Where(o => availableForSearchTypes.Contains(o.GroupTypeId))
                      .OrderByDescending(o => o.Created)
                      .Skip(skip)
                      .Take(take)
                      .Include(o => o.PhotoResources)
                      .Include(o => o.Members)
                      .Select(group => new GroupDto
                      {
                          Description = group.Description,
                          GroupTypeId = group.GroupTypeId,
                          Id = group.Id,
                          Name = group.Name,
                          CreatedDate = group.Created,
                          NumberOfMember = group.NumberOfMembers,
                          NumberOfViewer = group.NumberOfViewers,
                          GroupPhotoResources = group.PhotoResources
                        .Select(f => new PhotoResourcesDto()
                        {
                            Path = f.FullPath,
                            PhotoSize = f.PhotoSize
                        }),
                          IsUserMemberOfGroup = group.Members
                         .Any(o => o.MemberId == userId)
                      }).AsNoTracking()
                        .FromCacheAsync();

            return groupList.ToList();
        }

        public async Task<IList<GroupDto>> GetGroupUserAndIncludingResourcesOnly(string userId, int skip, int take, params int[] groupType)
        {
            var selectedGroupType = groupType == null ? allGroupTypes : groupType;

            var groupList = await (from @group in Context.Group
                                   .Include(o => o.PhotoResources)
                                   join member in Context.GroupMember on @group.Id
                                   equals member.GroupId
                                   where member.MemberId == userId &&
                                   selectedGroupType.Contains(@group.GroupTypeId)
                                   orderby @group.Created descending
                                   select new GroupDto
                                   {
                                       Description = @group.Description,
                                       GroupTypeId = @group.GroupTypeId,
                                       Id = @group.Id,
                                       Name = @group.Name,
                                       CreatedDate = @group.Created,
                                       NumberOfMember = @group.NumberOfMembers,
                                       NumberOfViewer = @group.NumberOfViewers,
                                       GroupPhotoResources = @group.PhotoResources

                                   .Select(f => new PhotoResourcesDto()
                                   {
                                       Path = f.FullPath,
                                       PhotoSize = f.PhotoSize
                                   }),
                                       IsUserMemberOfGroup = true
                                   })
                         .Skip(skip)
                         .Take(take)
                         .AsNoTracking()
                        .FromCacheAsync();

            return groupList.ToList();
        }

        public async Task<IList<GroupFilterResultDto>> GetAllGroupIncludingResourcesOnly(string filter, string sortColumn, string sortColumnDirection, int skip, int take)
        {
            var groupData = Context
                      .Group
                      .Include(o => o.PhotoResources)
                      .Select(group => new GroupFilterResultDto
                      {
                          Description = group.Description,
                          GroupTypeId = group.GroupTypeId,
                          Id = group.Id,
                          Name = group.Name,
                          CreatedDate = group.Created,
                          NumberOfMember = group.NumberOfMembers,
                          NumberOfViewer = group.NumberOfViewers,
                          GroupPhotoResources = group.PhotoResources
                        .Select(f => new PhotoResourcesDto()
                        {
                            Path = f.FullPath,
                            PhotoSize = f.PhotoSize
                        })
                      });

            //Sort
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                groupData = groupData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            else
            {
                groupData = groupData.OrderByDescending(o => o.CreatedDate);
            }

            //Filter
            if (!string.IsNullOrEmpty(filter))
            {
                groupData = groupData.Where(m => m.Name.Contains(filter) ||
                                               m.Description.Contains(filter) ||
                                               m.CreatedDate.ToString().Contains(filter) ||
                                               m.NumberOfMember.ToString() == filter ||
                                               m.NumberOfViewer.ToString() == filter);
            }

            //Paging
            var data = await groupData
                .Skip(skip)
                .Take(take)
                .FromCacheAsync();

            return data.ToList();
        }
    }
}