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
    public class ProfileFollowerRepository : Repository<ProfileFollower>, IProfileFollowerRepository
    {
        public ProfileFollowerRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<IList<string>> GetUnMentiondFollowersIdList(IList<string> userIdPings, string profileId)
        {
            if (userIdPings.Count() > 0 && !string.IsNullOrEmpty(profileId))
            {
                var followers = await Context.ProfileFollower
                    .Where(o => o.ProfileId == profileId)
                    .Where(o => !userIdPings.Contains(o.FollowerProfileId))
                    .Select(o => o.FollowerProfileId)
                    .AsNoTracking()
                    .ToListAsync();

                return followers;
            }
            return new List<string>();
        }

        public async Task<bool> IsUserFollowedProfile(string userId, string profileId)
        {
            return await Context.ProfileFollower
                .AsNoTracking()
                .AnyAsync(o => o.FollowerProfileId == userId && o.ProfileId == profileId);
        }

        public async Task<IList<ProfileFollowersDto>> GetProfileFollowers(string id, int skip, int take, bool? withIsSeen = null)
        {
            var profileFollowers = await Context
                            .ProfileFollower
                            .AsNoTracking()
                            .Where(o => o.ProfileId == id)
                            .Where(o => withIsSeen == null ? 1 == 1 : o.IsSeen == withIsSeen)
                            .OrderByDescending(o => o.FollowedDate)
                            .Skip(skip)
                            .Take(take)
                            .Include(o => o.FollowerProfile)
                            .ThenInclude(o => o.PhotoResources)
                            .Include(o => o.FollowerProfile)
                            .ThenInclude(o => o.Gender)
                            .Include(o => o.FollowerProfile)
                            .ThenInclude(o => o.Followers)
                            .Select(o =>
                           new ProfileFollowersDto
                           {
                               FollowerId = o.FollowerProfileId,
                               UserName = o.FollowerProfile.UserName,
                               Gender = o.FollowerProfile.Gender,
                               ProfileId = o.ProfileId,
                               FollowedDate = o.FollowedDate,
                               BirthDate = o.FollowerProfile.BirthDate,
                               City = o.FollowerProfile.City,
                               Country = o.FollowerProfile.Country,
                               Email = o.FollowerProfile.Email,
                               PhoneNumber = o.FollowerProfile.PhoneNumber,
                               IsSeen = o.IsSeen,
                               IsProfileOwnerFollowedTheFollower =
                               o.FollowerProfile
                               .Followers
                               .Any(o => o.FollowerProfileId == id),
                               ProfilePhotoResources =
                               o.FollowerProfile
                               .PhotoResources
                                .Where(f => f.Length > 0)
                               .Select(f => new PhotoResourcesDto()
                               {
                                   Path = f.FullPath,
                                   PhotoSize = f.PhotoSize
                               })
                           }).ToListAsync();

            return profileFollowers;
        }

        public async Task<int> DeleteProfileFollower(string profileId, string followerId)
        {
            var numberOfFollowers = 0;

            IDbConnection db = Context.YamaacoDatabase.GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed) db.Open();
                var @params = new { followerId, profileId };

                numberOfFollowers = await db.QueryFirstAsync<int>(" [spDeleteProfileFollower] @profileId,@followerId ", @params);
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

            return numberOfFollowers;
        }

        public async Task<CreatedProfileFollowerBasicInfoDto> CreateProfileFollower(string profileId, string followerId)
        {
            CreatedProfileFollowerBasicInfoDto profileFollowerCreated;
            IDbConnection db = Context.YamaacoDatabase
                  .GetDbConnection();

            try
            {
                if (db.State == ConnectionState.Closed) db.Open();

                var @params = new { followerId, profileId };

                profileFollowerCreated = await db.QueryFirstAsync<CreatedProfileFollowerBasicInfoDto>(" [spCreateProfileFollower] @profileId,@followerId ", @params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }

            return profileFollowerCreated;
        }

        public async Task<IList<ProfileFollowerBasicInfoDto>> GetAllProfileFollowersBasicList(string id)
        {
            var query = Context
                .ProfileFollower
                 .Where(o => o.ProfileId == id)
                .AsNoTracking()
                .Include(o => o.FollowerProfile)
                .ThenInclude(o => o.Gender)
                .Select(o => new ProfileFollowerBasicInfoDto()
                {
                    Email = o.FollowerProfile.Email,
                    FollowedDate = o.FollowedDate,
                    FollowerId = o.FollowerProfileId,
                    Gender = o.FollowerProfile.Gender,
                    UserName = o.FollowerProfile.UserName,
                    PhoneNumber = o.FollowerProfile.PhoneNumber
                });

            return await query.ToListAsync();
        }
    }
}