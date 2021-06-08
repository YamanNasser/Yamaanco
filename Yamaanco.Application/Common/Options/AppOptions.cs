namespace Yamaanco.Application.Common.Options
{
    public class AppOptions
    {
        public string GroupUploadFolderName { get; set; }
        public string GroupCommentUploadFolderName { get; set; }
        public string ProfileMessageUploadFolderName { get; set; }
        public string GroupMessageUploadFolderName { get; set; }
        public string DefaultUploadFolderName { get; set; }
        public string ProfileUploadFolderName { get; set; }
        public string ProfileCommentUploadFolderName { get; set; }
        public int SmallPageSize { get; set; }
        public int DefaultPageIndex { get; set; } = 0;
        public int MediumPageSize { get; set; }
        public int LargePageSize { get; set; }
        public int SmallImageSize { get; set; }
        public int MediumImageSize { get; set; }
        public int LargeImageSize { get; set; }
        public string LengthOfNotificationCharacter { get; set; }
    }
}