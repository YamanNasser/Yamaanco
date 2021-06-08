using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Common;
using Yamaanco.Application.DTOs.Profile;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common;
using Z.EntityFramework.Plus;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(IYamaancoDbContext context)
            : base(context)
        {
        }

        public async Task<string> GetProfileIdByEmail(string email)
        {
            var profileId = await Context
                      .Profile
                      .AsNoTracking()
                       .Where(o => o.Email == email)
                       .Select(o => o.Id)
                       .SingleOrDefaultAsync();

            return profileId;
        }

        //TODO: check if current logedIn user follow the returned profile list. Also, delete the follower id list.
        public async Task<ProfileDto> GetProfileById(string id)
        {
            var profile = await Context
                      .Profile
                      .AsNoTracking()
                      .Where(o => o.Id == id)
                      .IncludeOptimized(o => o.Followers)
                      .IncludeOptimized(o => o.PhotoResources)
                      .Select(profile => new ProfileDto
                      {
                          UserName = profile.UserName,
                          Id = profile.Id,
                          NumberOfFollowers = profile.NumberOfFollowers,
                          NumberOfViewers = profile.NumberOfViewers,
                          PhoneNumber = profile.PhoneNumber,
                          Email = profile.Email,
                          Gender = profile.Gender,
                          CreatedDate = profile.Created,
                          BirthDate = profile.BirthDate,
                          AboutMe = profile.AboutMe,
                          Address = profile.Address,
                          City = profile.City,
                          Country = profile.Country,
                          FirstName = profile.FirstName,
                          LastName = profile.LastName,
                          ProfilePhotoResources = profile.PhotoResources
                        .Select(f => new PhotoResourcesDto()
                        {
                            Path = f.FullPath,
                            PhotoSize = f.PhotoSize
                        }),
                          FollowersId = profile.Followers
                         .Select(o => o.Id)
                      }).SingleOrDefaultAsync();

            return profile;
        }

        //TODO: check if current logedIn user follow the returned profile list.
        public async Task<IList<ProfileDto>> GetProfileByName(string name, int skip, int take)
        {
            var response = await Context
                      .Profile
                      .IncludeOptimized(o => o.Gender)
                      .IncludeOptimized(o => o.PhotoResources)
                      .Where(o => o.UserName.Contains(name))
                      .OrderByDescending(o => o.Created)
                      .Skip(skip)
                      .Take(take)
                      .Select(o =>
                        new ProfileDto
                        {
                            UserName = o.UserName,
                            Id = o.Id,
                            NumberOfFollowers = o.NumberOfFollowers,
                            NumberOfViewers = o.NumberOfViewers,
                            PhoneNumber = o.PhoneNumber,
                            Email = o.Email,
                            Gender = o.Gender,
                            CreatedDate = o.Created,
                            BirthDate = o.BirthDate,
                            AboutMe = o.AboutMe,
                            Address = o.Address,
                            City = o.City,
                            Country = o.Country,
                            FirstName = o.FirstName,
                            LastName = o.LastName,
                            ProfilePhotoResources = o.PhotoResources
                            .Select(f => new PhotoResourcesDto()
                            {
                                Path = f.FullPath,
                                PhotoSize = f.PhotoSize
                            }),
                            FollowersId = o.Followers
                             .Select(o => o.Id)
                        })
                      .AsNoTracking()
                        .FromCacheAsync();
            return response.ToList();
        }

        public async Task<IList<ProfileDto>> GetProfileList(string filter, string sortColumn, string sortColumnDirection, int skip, int take)
        {
            var userData = Context
                .Profile
                .AsNoTracking()
                .IncludeOptimized(o => o.Gender)
                .IncludeOptimized(o => o.PhotoResources)
                .Select(o =>
            new ProfileDto
            {
                UserName = o.UserName,
                Id = o.Id,
                NumberOfFollowers = o.NumberOfFollowers,
                NumberOfViewers = o.NumberOfViewers,
                PhoneNumber = o.PhoneNumber ?? "",
                Email = o.Email,
                Gender = o.Gender,
                CreatedDate = o.Created,
                BirthDate = o.BirthDate,
                AboutMe = o.AboutMe,
                Address = o.Address,
                City = o.City,
                Country = o.Country,
                FirstName = o.FirstName,
                LastName = o.LastName,
                ProfilePhotoResources = o.PhotoResources
                            .Select(f => new PhotoResourcesDto()
                            {
                                Path = f.FullPath,
                                PhotoSize = f.PhotoSize
                            })
            });

            //Sort
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                userData = userData.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            else
            {
                userData = userData.OrderByDescending(o => o.CreatedDate);
            }

            //Filter
            if (!string.IsNullOrEmpty(filter))
            {
                userData = userData.Where(m => m.UserName.Contains(filter) ||
                                               m.Email.Contains(filter) ||
                                               m.Gender.Name.Contains(filter) ||
                                               m.City.Contains(filter) ||
                                               m.Country.Contains(filter) ||
                                               m.AboutMe.Contains(filter) ||
                                               m.Address.Contains(filter) ||
                                               m.PhoneNumber.Contains(filter) ||
                                               m.NumberOfFollowers.ToString() == filter ||
                                               m.NumberOfViewers.ToString() == filter);
            }

            //Paging
            var data = await userData
                .Skip(skip)
                .Take(take)
                .FromCacheAsync();

            return data.ToList();
        }

        public bool HaveProfile(string id)
        {
            var response = Context
                      .Profile
                      .AsNoTracking()
                      .Any(o => o.Id == id);
            return response;
        }

        public async Task<string> GetProfileUserName(string id)
        {
            var profile = await Context
                .Profile
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == id);
            return profile.UserName;
        }
    }
}