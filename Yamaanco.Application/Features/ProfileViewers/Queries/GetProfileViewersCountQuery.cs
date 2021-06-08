using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.ProfileViewers.Queries
{
   public  class GetProfileViewersCountQuery : IRequest<Response<int>>
    {
        public string ProfileId { get; set; }

        public GetProfileViewersCountQuery(string profileId)
        {
            ProfileId = profileId;
        }
    }
}