using MediatR;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Features.GroupMembers.ViewModel;

namespace Yamaanco.Application.Features.GroupMembers.Commands
{
    public class AddNewMemberCommand : IRequest<Response<CreatedGroupMemberBasicInfoView>>
    {
        public string GroupId { get; set; }
        public string MemberId { get; set; }
    }
}