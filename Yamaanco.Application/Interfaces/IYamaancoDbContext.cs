using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Entities.UserLogsEntities;

namespace Yamaanco.Application.Interfaces
{
    public interface IYamaancoDbContext
    {
        DbSet<T> Set<T>() where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public DatabaseFacade YamaacoDatabase { get; }

        void Dispose();

        DbSet<ProfileMessageViewer> ProfileMessageViewer { get; set; }
        DbSet<GroupMessageViewer> GroupMessageViewer { get; set; }

        DbSet<GroupCommentViewer> GroupCommentViewer { get; set; }
        DbSet<ProfileCommentViewer> ProfileCommentViewer { get; set; }

        DbSet<Group> Group { get; set; }
        DbSet<GroupComment> GroupComment { get; set; }
        DbSet<GroupCommentPings> GroupCommentPings { get; set; }
        DbSet<GroupCommentTransaction> GroupCommentTransaction { get; set; }
        DbSet<GroupCommentUpvotedUser> GroupCommentUpvotedUser { get; set; }
        DbSet<GroupMember> GroupMember { get; set; }
        DbSet<GroupMemberRequest> GroupMemberRequest { get; set; }
        DbSet<GroupMessage> GroupMessage { get; set; }
        DbSet<GroupNotification> GroupNotification { get; set; }
        DbSet<GroupType> GroupType { get; set; }
        DbSet<Gender> Gender { get; set; }
        DbSet<Profile> Profile { get; set; }
        DbSet<ProfileBlockList> ProfileBlockList { get; set; }
        DbSet<ProfileComment> ProfileComment { get; set; }
        DbSet<ProfileCommentPings> ProfileCommentPings { get; set; }
        DbSet<ProfileCommentTransaction> ProfileCommentTransaction { get; set; }
        DbSet<ProfileCommentUpvotedUser> ProfileCommentUpvotedUser { get; set; }
        DbSet<ProfileFollower> ProfileFollower { get; set; }
        DbSet<ProfileFriend> ProfileFriend { get; set; }
        DbSet<ProfileMessage> ProfileMessage { get; set; }
        DbSet<ProfileNotification> ProfileNotification { get; set; }
        DbSet<ProfileViewer> ProfileViewer { get; set; }
        DbSet<UserLoginLogs> UserLoginLogs { get; set; }
        DbSet<GroupViewer> GroupViewer { get; set; }
        DbSet<GroupBlockList> GroupBlockList { get; set; }
        DbSet<GroupCommentResources> GroupCommentResources { get; set; }
        DbSet<GroupPhotoResources> GroupPhotoResources { get; set; }
        DbSet<ProfilePhotoResources> ProfilePhotoResources { get; set; }
        DbSet<ProfileCommentResources> ProfileCommentResources { get; set; }
    }
}