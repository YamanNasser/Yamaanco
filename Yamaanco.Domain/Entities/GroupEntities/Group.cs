using System;
using System.Collections.Generic;
using Yamaanco.Domain.Common;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class Group : AuditableEntity
    {
        public Group() : base("")
        {
            PhotoResources = new HashSet<GroupPhotoResources>();
            Members = new HashSet<GroupMember>();
            Comments = new HashSet<GroupComment>();
            MemberRequests = new HashSet<GroupMemberRequest>();
            Notifications = new HashSet<GroupNotification>();
            CommentTransactions = new HashSet<GroupCommentTransaction>();
            Viewers = new HashSet<GroupViewer>();
            Messages = new HashSet<GroupMessage>();
            BlockList = new HashSet<GroupBlockList>();
        }

        public Group(string name, int groupTypeId, string createdById, string description) : base(createdById)
        {
            PhotoResources = new HashSet<GroupPhotoResources>();
            Members = new HashSet<GroupMember>();
            Comments = new HashSet<GroupComment>();
            MemberRequests = new HashSet<GroupMemberRequest>();
            Notifications = new HashSet<GroupNotification>();
            CommentTransactions = new HashSet<GroupCommentTransaction>();
            Viewers = new HashSet<GroupViewer>();
            Messages = new HashSet<GroupMessage>();
            BlockList = new HashSet<GroupBlockList>();

            Id = Guid.NewGuid().ToString();
            Name = name;
            GroupTypeId = groupTypeId;
            Description = description;
            NumberOfViewers = 0;
            NumberOfMembers = 1;
            AddDefaultPhoto();
            NewGroupAdmin(createdById);
        }

        public int NewGroupAdmin(string id)
        {
            Members.Add(new GroupMember(
                groupId: Id,
                memberId: id,
                isAdmin: true
            ));
            NumberOfMembers += 1;
            return NumberOfMembers;
        }

        public int NewGroupMember(string id)
        {
            Members.Add(new GroupMember(
                groupId: Id,
                memberId: id,
                isAdmin: false
            ));
            NumberOfMembers += 1;
            return NumberOfMembers;
        }

        public int NewViewer(string id)
        {
            Viewers.Add(new GroupViewer(
                groupId: Id,
                viewerProfileId: id
            ));
            NumberOfViewers += 1;
            return NumberOfViewers;
        }

        public void Update(string name, int groupTypeId, string description, string updatedById)
        {
            Name = name;
            GroupTypeId = groupTypeId;
            Description = description;
            LastModifiedById = updatedById;
        }

        private void AddDefaultPhoto()
        {
            foreach (var photoSize in
             new[] { PhotoSize.Default, PhotoSize.Large, PhotoSize.Medium, PhotoSize.Small })
            {
                PhotoResources.Add(
                 new GroupPhotoResources(
                     folderName: "",
                     photoSize: photoSize,
                     groupId: Id,
                     description: Description
                     ));
            }
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public int GroupTypeId { get; private set; }
        public GroupType GroupType { get; private set; }
        public string Description { get; private set; }
        public int NumberOfViewers { get; private set; }
        public int NumberOfMembers { get; private set; }

        public ICollection<GroupBlockList> BlockList { get; private set; }
        public ICollection<GroupMessage> Messages { get; private set; }
        public ICollection<GroupViewer> Viewers { get; private set; }
        public ICollection<GroupComment> Comments { get; private set; }
        public ICollection<GroupPhotoResources> PhotoResources { get; private set; }
        public ICollection<GroupMember> Members { get; private set; }
        public ICollection<GroupMemberRequest> MemberRequests { get; private set; }
        public ICollection<GroupNotification> Notifications { get; private set; }
        public ICollection<GroupCommentTransaction> CommentTransactions { get; private set; }
    }
}