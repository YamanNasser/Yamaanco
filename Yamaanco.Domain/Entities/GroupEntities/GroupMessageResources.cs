using Yamaanco.Domain.Entities.BaseEntities;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupMessageResources : Resources
    {
        public GroupMessageResources()
        {
        }

        public GroupMessageResources(string folderName, string groupId, string messageId, string description) : base
             ()
        {
            if (!string.IsNullOrEmpty(folderName))
                Path = $"{folderName}\\{groupId}\\{messageId}";
            MessageId = messageId;
            GroupId = groupId;
            Description = description;
        }

        public string FullPath
        {
            get => (new FullPath(Path, GroupId, Extension)).ToString();
            private set => _ = (new FullPath(Path, GroupId, Extension)).ToString();
        }

        public string MessageId { get; private set; }
        public string GroupId { get; private set; }

        public GroupMessage Message { get; private set; }
        public Group Group { get; private set; }
    }
}