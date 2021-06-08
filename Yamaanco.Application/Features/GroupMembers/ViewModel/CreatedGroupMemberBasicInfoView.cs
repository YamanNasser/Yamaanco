using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamaanco.Application.DTOs.Group;

namespace Yamaanco.Application.Features.GroupMembers.ViewModel
{
    public class CreatedGroupMemberBasicInfoView
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public int NumberOfGroupMembers { get; set; }
        public MemberBasicInfoDto MemberInfo { get; set; }
    }
}