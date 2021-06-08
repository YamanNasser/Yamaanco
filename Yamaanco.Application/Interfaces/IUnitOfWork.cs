using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Application.Interfaces.Repositories.UserLogs;

namespace Yamaanco.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProfileCommentHashtagRepository ProfileCommentHashtagRepository { get; }

        IGroupCommentHashtagRepository GroupCommentHashtagRepository { get; }

        IProfileMessageViewerRepository ProfileMessageViewerRepository { get; }

        IGroupMessageViewerRepository GroupMessageViewerRepository { get; }

        IGroupCommentViewerRepository GroupCommentViewerRepository { get; }

        IProfileCommentViewerRepository ProfileCommentViewerRepository { get; }

        IGroupViewerRepository GroupViewerRepository { get; }
        IGroupBlockListRepository GroupBlockListRepository { get; }
        IGroupCommentResourcesRepository GroupCommentResourcesRepository { get; }
        IGroupPhotoResourcesRepository GroupPhotoResourcesRepository { get; }
        IProfilePhotoResourcesRepository ProfilePhotoResourcesRepository { get; }
        IProfileCommentResourcesRepository ProfileCommentResourcesRepository { get; }
        IGroupRepository GroupRepository { get; }
        IGroupCommentRepository GroupCommentRepository { get; }
        IGroupCommentPingsRepository GroupCommentPingsRepository { get; }
        IGroupCommentTransactionRepository GroupCommentTransactionRepository { get; }
        IGroupCommentUpvotedUserRepository GroupCommentUpvotedUserRepository { get; }
        IGroupMemberRepository GroupMemberRepository { get; }
        IGroupMemberRequestRepository GroupMemberRequestRepository { get; }
        IGroupMessageRepository GroupMessageRepository { get; }
        IGroupNotificationRepository GroupNotificationRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IProfileBlockListRepository ProfileBlockListRepository { get; }
        IProfileCommentRepository ProfileCommentRepository { get; }
        IProfileCommentPingsRepository ProfileCommentPingsRepository { get; }
        IProfileCommentTransactionRepository ProfileCommentTransactionRepository { get; }
        IProfileCommentUpvotedUserRepository ProfileCommentUpvotedUserRepository { get; }
        IProfileFollowerRepository ProfileFollowerRepository { get; }
        IProfileFriendRepository ProfileFriendRepository { get; }
        IProfileMessageRepository ProfileMessageRepository { get; }
        IProfileNotificationRepository ProfileNotificationRepository { get; }
        IProfileViewerRepository ProfileViewerRepository { get; }
        IUserLoginLogsRepository UserLoginLogsRepository { get; }

        IProfileMessageResourcesRepository ProfileMessageResourcesRepository { get; }
        IGroupMessageResourcesRepository GroupMessageResourcesRepository { get; }

        int Commit();

        Task<int> CommitAsync(CancellationToken cancellationToken);

        Task<int> CommitAsync();
    }
}