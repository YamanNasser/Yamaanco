using System;
using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupMessage : Message
    {
        public GroupMessage()
        {
        }

        public GroupMessage(string groupId, string content, MessageCategory messageType, string createdById) : base(content, messageType, createdById)
        {
            GroupId = groupId;
        }

        public string Target
        {
            get
            {
                return GroupId;
            }
            private set
            {
                _ = GroupId;
            }
        }

        public Profile CreatedBy { get; private set; }

        public string GroupId { get; private set; }
        public Group Group { get; private set; }
        public GroupMessageResources File { get; private set; }

        public void AttachFile(GroupMessageResources file)
        {
            FileId = file.Id;
            File = file;
        }
    }
}