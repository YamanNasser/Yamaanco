using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamaanco.Application.DTOs.Group
{
    public class MemberBasicInfoDto
    {
        public string MemberId { get; set; }
        public string MemberProfileMediumPhotoPath { get; set; }
        public string MemberName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime JoinDate { get; set; }
    }
}