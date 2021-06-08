namespace Yamaanco.Domain.Enums
{
    public enum NotificationType
    {
        NewMessage = 0,
        NewComment = 1,
        NewReply = 2,
        UpdateComment = 10,
        UpdateReply = 11,
        UpvotedComment = 20,
        UpvotedReply = 21,
        FollowUserProfile = 30,
        VideoOrAudioCall = 31,
        GroupRequest = 40,
        SystemMessage = 50
    }
}