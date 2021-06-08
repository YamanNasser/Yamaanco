using MediatR;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.DTOs.Group;
using Yamaanco.Application.Features.GroupMembers.ViewModel;

namespace Yamaanco.Application.Features.GroupMembers.Commands
{
    public class AcceptJoinGroupCommand : IRequest<Response<CreatedGroupMemberBasicInfoView>>
    {
        public AcceptJoinGroupCommand(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}