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
    public class GroupViewerRepository : Repository<GroupViewer>, IGroupViewerRepository
    {
        public GroupViewerRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<int> CreateGroupViewer(string groupId, string viewerId)
        {
            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();

            var numberOfViewer = 0;

            try
            {
                if (db.State == ConnectionState.Closed) db.Open();

                var @params = new { groupId, viewerId };
                numberOfViewer = await db.QueryFirstAsync<int>(" [spCreateGroupViewer] @groupId,@viewerId ", @params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
            return numberOfViewer;
        }

        public async Task<IList<GroupViewersInfoDto>> GetGroupViewers(string id, int skip, int take)
        {
            var profileViewers = await Context
                            .GroupViewer
                            .AsNoTracking()
                            .Where(o => o.GroupId == id)
                            .OrderByDescending(o => o.ViewerDate)
                            .Skip(skip)
                            .Take(take)
                            .Include(o => o.ViewerProfile)
                            .ThenInclude(o => o.PhotoResources)
                            .Include(o => o.ViewerProfile)
                            .ThenInclude(o => o.Gender)
                            .Include(o => o.ViewerProfile)
                            .ThenInclude(o => o.Viewers)
                            .Select(o =>
                                   new GroupViewersInfoDto
                                   {
                                       ViewerId = o.ViewerProfileId,
                                       UserName = o.ViewerProfile.UserName,
                                       Gender = o.ViewerProfile.Gender,
                                       ProfilePhotoResources = o.ViewerProfile
                                       .PhotoResources.Select(f =>
                                       new PhotoResourcesDto()
                                       {
                                           Path = f.FullPath,
                                           PhotoSize = f.PhotoSize
                                       }),
                                       GroupId = o.GroupId,
                                       ViewedDate = o.ViewerDate,
                                       BirthDate = o.ViewerProfile.BirthDate,
                                       City = o.ViewerProfile.City,
                                       Country = o.ViewerProfile.Country,
                                       Email = o.ViewerProfile.Email,
                                       PhoneNumber = o.ViewerProfile.PhoneNumber
                                   }).ToListAsync();

            return profileViewers;
        }
    }
}