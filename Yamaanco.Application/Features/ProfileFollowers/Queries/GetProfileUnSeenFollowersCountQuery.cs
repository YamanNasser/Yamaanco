using MediatR;
using Yamaanco.Application.ApiResponses;

namespace Yamaanco.Application.Features.GroupFollowers.Queries
{
    public class GetUnSeenFollowersCountQuery : IRequest<Response<int>>
    {
       
    }
}