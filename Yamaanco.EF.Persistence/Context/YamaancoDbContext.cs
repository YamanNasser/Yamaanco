using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.Common;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Entities.ProfileEntities;
using Yamaanco.Domain.Entities.UserLogsEntities;

namespace Yamaanco.Infrastructure.EF.Persistence.Context
{
    public class YamaancoDbContext : DbContext, IYamaancoDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public YamaancoDbContext(DbContextOptions options, ICurrentUserService currentUserService)
         : base(options)
        {
            _currentUserService = currentUserService;
        }

        public YamaancoDbContext(DbContextOptions options)
          : base(options)
        {
        }

        protected YamaancoDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(YamaancoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProfileCommentHashtag> ProfileCommentHashtag { get; set; }
        public DbSet<GroupCommentHashtag> GroupCommentHashtag { get; set; }
        public DbSet<ProfileMessageViewer> ProfileMessageViewer { get; set; }
        public DbSet<GroupMessageViewer> GroupMessageViewer { get; set; }
        public DbSet<GroupCommentViewer> GroupCommentViewer { get; set; }
        public DbSet<ProfileCommentViewer> ProfileCommentViewer { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<GroupComment> GroupComment { get; set; }
        public DbSet<GroupCommentPings> GroupCommentPings { get; set; }
        public DbSet<GroupCommentTransaction> GroupCommentTransaction { get; set; }
        public DbSet<GroupCommentUpvotedUser> GroupCommentUpvotedUser { get; set; }
        public DbSet<GroupMember> GroupMember { get; set; }
        public DbSet<GroupMemberRequest> GroupMemberRequest { get; set; }
        public DbSet<GroupMessage> GroupMessage { get; set; }
        public DbSet<GroupNotification> GroupNotification { get; set; }
        public DbSet<GroupType> GroupType { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ProfileBlockList> ProfileBlockList { get; set; }
        public DbSet<ProfileComment> ProfileComment { get; set; }
        public DbSet<ProfileCommentPings> ProfileCommentPings { get; set; }
        public DbSet<ProfileCommentTransaction> ProfileCommentTransaction { get; set; }
        public DbSet<ProfileCommentUpvotedUser> ProfileCommentUpvotedUser { get; set; }
        public DbSet<ProfileFollower> ProfileFollower { get; set; }
        public DbSet<ProfileFriend> ProfileFriend { get; set; }
        public DbSet<ProfileMessage> ProfileMessage { get; set; }
        public DbSet<ProfileNotification> ProfileNotification { get; set; }
        public DbSet<ProfileViewer> ProfileViewer { get; set; }
        public DbSet<UserLoginLogs> UserLoginLogs { get; set; }
        public DbSet<GroupViewer> GroupViewer { get; set; }
        public DbSet<GroupBlockList> GroupBlockList { get; set; }
        public DbSet<GroupCommentResources> GroupCommentResources { get; set; }
        public DbSet<GroupPhotoResources> GroupPhotoResources { get; set; }
        public DbSet<ProfilePhotoResources> ProfilePhotoResources { get; set; }
        public DbSet<ProfileCommentResources> ProfileCommentResources { get; set; }

        public DbSet<GroupMessageResources> GroupMessageResources { get; set; }
        public DbSet<ProfileMessageResources> ProfileMessageResources { get; set; }

        public DatabaseFacade YamaacoDatabase => Database;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}