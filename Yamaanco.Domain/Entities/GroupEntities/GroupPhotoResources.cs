using Yamaanco.Domain.Entities.BaseEntities;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Domain.Entities.GroupEntities
{
    public class GroupPhotoResources : Resources
    {
        public GroupPhotoResources()
        {
        }

        public GroupPhotoResources(string folderName, PhotoSize photoSize, string groupId, string description)
        {
            if (!string.IsNullOrEmpty(folderName))
                Path = $"{folderName}\\{photoSize}\\{groupId}";
            GroupId = groupId;
            Description = description;
        }

        public void SetPath(string folderName)
        {
            if (!string.IsNullOrEmpty(folderName))
                Path = $"{folderName}\\{PhotoSize}\\{GroupId}";
        }

        public string FullPath
        {
            get => (new FullPath(Path, GroupId, Extension)).ToString();
            private set
            {
                _ = (new FullPath(Path, GroupId, Extension)).ToString();
            }
        }

        public PhotoSize PhotoSize { get; private set; }
        public string GroupId { get; private set; }
        public Group Group { get; private set; }
    }
}