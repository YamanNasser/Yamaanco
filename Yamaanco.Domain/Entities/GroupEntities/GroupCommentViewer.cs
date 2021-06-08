using System;
using Yamaanco.Domain.Entities.ProfileEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupCommentViewer
    {
        public GroupCommentViewer()
        {
        }

        public GroupCommentViewer(string commentId, string groupId, string viewerProfileId)
        {
            Id = Guid.NewGuid().ToString();
            CommentId = commentId;
            Date = DateTime.Now;
            GroupId = groupId;
            ViewerProfileId = viewerProfileId;
        }

        public string Id { get; private set; }
        public string CommentId { get; private set; }
        public DateTime Date { get; private set; }
        public string GroupId { get; private set; }
        public string ViewerProfileId { get; private set; }
        public Profile ViewerProfile { get; private set; }
        public GroupComment Comment { get; private set; }
        public Group Group { get; private set; }
    }
}