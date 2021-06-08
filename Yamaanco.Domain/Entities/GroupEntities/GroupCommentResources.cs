using Yamaanco.Domain.Entities.BaseEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupCommentResources : Resources
    {
        public GroupCommentResources()
        {
        }

        public GroupCommentResources(string folderName, string groupId, string commentId, string description) : base
            ()
        {
            if (!string.IsNullOrEmpty(folderName))
                Path = $"{folderName}\\{groupId}\\{commentId}";
            CommentId = commentId;
            GroupId = groupId;
            Description = description;
        }

        public string FullPath
        {
            get => (new FullPath(Path, GroupId, Extension)).ToString();
            private set
            {
                _ = (new FullPath(Path, GroupId, Extension)).ToString();
            }
        }

        public string CommentId { get; private set; }
        public string GroupId { get; private set; }
        public GroupComment Comment { get; private set; }
        public Group Group { get; private set; }
    }
}