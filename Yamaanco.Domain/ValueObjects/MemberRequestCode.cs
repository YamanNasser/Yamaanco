using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Yamaanco.Domain.Common;

namespace Yamaanco.Domain.ValueObjects
{
    public class MemberRequestCode : ValueObject
    {
        private string Id { get; set; }

        private string GroupId { get; set; }

        private string InviterId { get; set; }

        private string InvitedEmail { get; set; }
        private DateTime RequestDate { get; set; }

        public MemberRequestCode(string id, string groupId, string inviterId, string invitedEmail, DateTime requestDate)
        {
            Id = id;
            GroupId = groupId;
            InviterId = inviterId;
            InvitedEmail = invitedEmail;
            RequestDate = requestDate;
        }

        private string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"{Id}-{GroupId}-{InviterId}-{InvitedEmail}-{RequestDate}";
        }

        public string ToHashString()
        {
            return GetHashString($"{Id}-{GroupId}-{InviterId}-{InvitedEmail}-{RequestDate}");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return GroupId;
            yield return InviterId;
            yield return InvitedEmail;
            yield return InvitedEmail;
        }
    }
}