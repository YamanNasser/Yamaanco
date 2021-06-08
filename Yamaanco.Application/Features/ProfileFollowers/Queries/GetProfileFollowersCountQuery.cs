using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.ProfileFollowers.Queries
{
    public class GetProfileFollowersCountQuery : IRequest<Response<int>>
    {
        public string ProfileId { get; set; }

        public GetProfileFollowersCountQuery(string profileId)
        {
            ProfileId = profileId;
        }
    }
}