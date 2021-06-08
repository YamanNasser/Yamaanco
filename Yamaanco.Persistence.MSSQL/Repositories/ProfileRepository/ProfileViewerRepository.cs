using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileViewerRepository : Repository<ProfileViewer>, IProfileViewerRepository
    {
        public ProfileViewerRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<int> CreateProfileViewer(string profileId, string viewerId)
        {
            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();

            var numberOfViewer = 0;

            try
            {
                if (db.State == ConnectionState.Closed) db.Open();

                var @params = new { profileId, viewerId };
                numberOfViewer = await db.QueryFirstAsync<int>(" [spCreateProfileViewer] @profileId,@viewerId ", @params);
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

        public async Task<int> GetProfileViewersCount(string profileId)
        {
            return await Context.ProfileViewer.AsNoTracking()
             .CountAsync(o => o.ProfileId == profileId);
        }

        public async Task<bool> IsUserViewdProfileBefore(string userId, string profileId)
        {
            return await Context.ProfileViewer.AsNoTracking()
             .AnyAsync(o => o.ViewerProfileId == userId && o.ProfileId == profileId);
        }

        public async Task<IList<ProfileViewersInfoDto>> GetProfileViewers(string id, int skip, int take)
        {
            var profileViewers = await Context
                            .ProfileViewer
                            .AsNoTracking()
                            .Where(o => o.ProfileId == id)
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
                                   new ProfileViewersInfoDto
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
                                       ProfileId = o.ProfileId,
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