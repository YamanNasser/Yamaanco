using System.Collections.Generic;
using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class ProfileComment : Comment
    {
        public ProfileComment()
        {
            Hashtags = new HashSet<ProfileCommentHashtag>();
            Pings = new HashSet<ProfileCommentPings>();
            UpvotedUsers = new HashSet<ProfileCommentUpvotedUser>();
            CommentViewer = new HashSet<ProfileCommentViewer>();
            CommentTransactions = new HashSet<ProfileCommentTransaction>();
            CommentResources = new HashSet<ProfileCommentResources>();
        }

        public ProfileComment(string profileId, string parent, string root, string content, CommentCategory category, CommentType type, CommentClassification classification, string createdById, string[] pings) : base(parent, root, content, category, type, classification, createdById)
        {
            Hashtags = new HashSet<ProfileCommentHashtag>();
            Pings = new HashSet<ProfileCommentPings>();
            UpvotedUsers = new HashSet<ProfileCommentUpvotedUser>();
            CommentViewer = new HashSet<ProfileCommentViewer>();
            CommentTransactions = new HashSet<ProfileCommentTransaction>();
            CommentResources = new HashSet<ProfileCommentResources>();

            ProfileId = profileId;

            UpdateHashtags();
            UpdatePings(pings);
            AddTransaction(CommentTransactionType.Add);
        }

        public override void Delete()
        {
            base.Delete();
            AddTransaction(CommentTransactionType.Delete);
        }

        public int NewViewer(string viewerId)
        {
            CommentViewer.Add(new ProfileCommentViewer(commentId: Id, viewerProfileId: viewerId, profileId: ProfileId));
            CommentTransactions.Add(new ProfileCommentTransaction(profileId: ProfileId, commentId: Id, commentRoot: Root, commentParent: Parent, userId: viewerId, commentTransactionType: CommentTransactionType.View, data: Content));

            return NewViewer();
        }

        public override int Upvote()
        {
            var val = base.Upvote();
            AddTransaction(CommentTransactionType.Upvote);
            return val;
        }

        public void Update(string content, CommentClassification classification, string lastModifiedById, string[] pings, List<ProfileCommentResources> resources)
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
                    Hashtags.Add(new ProfileCommentHashtag(

                        commentId: Id,
                        profileId: ProfileId,
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
                      new ProfileCommentPings(
                       profileId: ProfileId,
                        commentId: Id,
                        mentionedUserId: ping
                    ));
                }
            }
        }

        private void UpdateResources(List<ProfileCommentResources> profileCommentResources)
        {
            RemoveResources();

            if (profileCommentResources != null)
            {
                foreach (var resource in profileCommentResources)
                {
                    CommentResources.Add(resource);
                }
            }
        }

        public void AddResources(List<ProfileCommentResources> profileCommentResources)
        {
            if (profileCommentResources != null)
            {
                foreach (var resource in profileCommentResources)
                {
                    CommentResources.Add(resource);
                }
            }
        }

        private void AddTransaction(CommentTransactionType type)
        {
            CommentTransactions.Add(
               new ProfileCommentTransaction(

                   commentId: Id,
                   commentRoot: Root,
                   commentParent: Parent,
                   data: Content,
                   profileId: ProfileId,
                   commentTransactionType: type,
                   userId: CreatedById
               ));
        }

        public string ProfileId { get; private set; }
        public Profile Profile { get; private set; }
        public ICollection<ProfileCommentHashtag> Hashtags { get; private set; }
        public ICollection<ProfileCommentResources> CommentResources { get; private set; }
        public ICollection<ProfileCommentPings> Pings { get; private set; }
        public ICollection<ProfileCommentUpvotedUser> UpvotedUsers { get; private set; }
        public ICollection<ProfileCommentTransaction> CommentTransactions { get; private set; }
        public ICollection<ProfileCommentViewer> CommentViewer { get; private set; }
    }
}