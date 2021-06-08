using System.Collections.Generic;
using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupComment : Comment
    {
        public GroupComment()
        {
            Hashtags = new HashSet<GroupCommentHashtag>();
            Pings = new HashSet<GroupCommentPings>();
            UpvotedUsers = new HashSet<GroupCommentUpvotedUser>();
            CommentTransactions = new HashSet<GroupCommentTransaction>();
            CommentResources = new HashSet<GroupCommentResources>();
            CommentViewer = new HashSet<GroupCommentViewer>();
        }

        public GroupComment(string groupId, string parent, string root, string content, CommentCategory category, CommentType type, CommentClassification classification, string createdById, string[] pings) : base(parent, root, content, category, type, classification, createdById)
        {
            Hashtags = new HashSet<GroupCommentHashtag>();
            Pings = new HashSet<GroupCommentPings>();
            UpvotedUsers = new HashSet<GroupCommentUpvotedUser>();
            CommentTransactions = new HashSet<GroupCommentTransaction>();
            CommentResources = new HashSet<GroupCommentResources>();
            CommentViewer = new HashSet<GroupCommentViewer>();
            GroupId = groupId;

            UpdateHashtags();
            UpdatePings(pings);
            AddTransaction(CommentTransactionType.Add);
        }

        public int NewViewer(string viewerId)
        {
            CommentViewer.Add(new GroupCommentViewer(commentId: Id, groupId: GroupId, viewerProfileId: viewerId));
            CommentTransactions.Add(new GroupCommentTransaction(groupId: GroupId, commentId: Id, commentRoot: Root, commentParent: Parent, userId: viewerId, commentTransactionType: CommentTransactionType.View, data: Content));
            return NewViewer();
        }

        public override void Delete()
        {
            base.Delete();
            AddTransaction(CommentTransactionType.Delete);
        }

        public override int Upvote()
        {
            var val = base.Upvote();
            AddTransaction(CommentTransactionType.Upvote);
            return val;
        }

        public void Update(string content, CommentClassification classification, string lastModifiedById, string[] pings, List<GroupCommentResources> resources)
        {
            Content = content;
            Classification = classification;
            LastModifiedById = lastModifiedById;

            UpdateHashtags();
            UpdateResources(resources);
            UpdatePings(pings);
            AddTransaction(CommentTransactionType.Edit);
        }

        private void RemoveResources()
        {
            CommentResources.Clear();
        }

        private void RemoveHashtags()
        {
            Hashtags.Clear();
        }

        private void UpdateHashtags()
        {
            RemoveHashtags();

            var hashtags = GenerateHashtags();
            if (hashtags != null)
            {
                while (hashtags.MoveNext())
                {
                    Hashtags.Add(new GroupCommentHashtag(

                        commentId: Id,
                        groupId: GroupId,
                        hashtag: hashtags.Current
                    ));
                }
            }
        }

        private void RemovePings()
        {
            Pings.Clear();
        }

        private void UpdatePings(string[] pings)
        {
            RemovePings();
            if (pings != null)
            {
                foreach (var ping in pings)
                {
                    Pings.Add(
                      new GroupCommentPings(
                       groupId: GroupId,
                        commentId: Id,
                        mentionedUserId: ping
                    ));
                }
            }
        }

        private void UpdateResources(List<GroupCommentResources> commentResources)
        {
            RemoveResources();

            if (commentResources != null)
            {
                foreach (var resource in commentResources)
                {
                    CommentResources.Add(resource);
                }
            }
        }

        public void AddResources(List<GroupCommentResources> commentResources)
        {
            if (commentResources != null)
            {
                foreach (var resource in commentResources)
                {
                    CommentResources.Add(resource);
                }
            }
        }

        private void AddTransaction(CommentTransactionType type)
        {
            CommentTransactions.Add(
               new GroupCommentTransaction(

                   commentId: Id,
                   commentRoot: Root,
                   commentParent: Parent,
                   data: Content,
                   groupId: GroupId,
                   commentTransactionType: type,
                   userId: CreatedById
               ));
        }

        public string GroupId { get; private set; }
        public Group Group { get; private set; }
        public ICollection<GroupCommentHashtag> Hashtags { get; private set; }
        public ICollection<GroupCommentResources> CommentResources { get; private set; }
        public ICollection<GroupCommentPings> Pings { get; private set; }
        public ICollection<GroupCommentUpvotedUser> UpvotedUsers { get; private set; }
        public ICollection<GroupCommentViewer> CommentViewer { get; private set; }

        public ICollection<GroupCommentTransaction> CommentTransactions { get; private set; }
    }
}