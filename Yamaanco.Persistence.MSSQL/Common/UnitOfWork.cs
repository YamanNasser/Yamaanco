using System;
using System.Threading;
using System.Threading.Tasks;
using Yamaanco.Application.Interfaces;
using Yamaanco.Application.Interfaces.Repositories.Group;
using Yamaanco.Application.Interfaces.Repositories.Profile;
using Yamaanco.Application.Interfaces.Repositories.UserLogs;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.GroupRepository;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.ProfileRepository;
using Yamaanco.Infrastructure.EF.Persistence.MSSQL.Repositories.UserLogsRepository;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IYamaancoDbContext _context;

        public UnitOfWork(IYamaancoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //ProfileCommentHashtagRepository

        private IProfileCommentHashtagRepository _profileCommentHashtagRepository { get; set; }

        public IProfileCommentHashtagRepository ProfileCommentHashtagRepository
        {
            get
            {
                if (_profileCommentHashtagRepository == null)
                {
                    _profileCommentHashtagRepository = new ProfileCommentHashtagRepository(_context);
                }
                return _profileCommentHashtagRepository;
            }
        }

        private IGroupCommentHashtagRepository _groupCommentHashtagRepository { get; set; }

        public IGroupCommentHashtagRepository GroupCommentHashtagRepository
        {
            get
            {
                if (_groupCommentHashtagRepository == null)
                {
                    _groupCommentHashtagRepository = new GroupCommentHashtagRepository(_context);
                }
                return _groupCommentHashtagRepository;
            }
        }

        private IGroupMessageViewerRepository _groupMessageViewerRepository { get; set; }

        public IGroupMessageViewerRepository GroupMessageViewerRepository
        {
            get
            {
                if (_groupMessageViewerRepository == null)
                {
                    _groupMessageViewerRepository = new GroupMessageViewerRepository(_context);
                }
                return _groupMessageViewerRepository;
            }
        }

        private IProfileMessageViewerRepository _profileMessageViewerRepository { get; set; }

        public IProfileMessageViewerRepository ProfileMessageViewerRepository
        {
            get
            {
                if (_profileMessageViewerRepository == null)
                {
                    _profileMessageViewerRepository = new ProfileMessageViewerRepository(_context);
                }
                return _profileMessageViewerRepository;
            }
        }

        private IGroupMessageResourcesRepository _groupMessageResourcesRepository { get; set; }

        public IGroupMessageResourcesRepository GroupMessageResourcesRepository
        {
            get
            {
                if (_groupMessageResourcesRepository == null)
                {
                    _groupMessageResourcesRepository = new GroupMessageResourcesRepository(_context);
                }
                return _groupMessageResourcesRepository;
            }
        }

        private IProfileMessageResourcesRepository _profileMessageResourcesRepository { get; set; }

        public IProfileMessageResourcesRepository ProfileMessageResourcesRepository
        {
            get
            {
                if (_profileMessageResourcesRepository == null)
                {
                    _profileMessageResourcesRepository = new ProfileMessageResourcesRepository(_context);
                }
                return _profileMessageResourcesRepository;
            }
        }

        private IGroupCommentViewerRepository _groupCommentViewerRepository { get; set; }

        public IGroupCommentViewerRepository GroupCommentViewerRepository
        {
            get
            {
                if (_groupCommentViewerRepository == null)
                {
                    _groupCommentViewerRepository = new GroupCommentViewerRepository(_context);
                }
                return _groupCommentViewerRepository;
            }
        }

        private IProfileCommentViewerRepository _profileCommentViewerRepository { get; set; }

        public IProfileCommentViewerRepository ProfileCommentViewerRepository
        {
            get
            {
                if (_profileCommentViewerRepository == null)
                {
                    _profileCommentViewerRepository = new ProfileCommentViewerRepository(_context);
                }
                return _profileCommentViewerRepository;
            }
        }

        private IGroupRepository _groupRepository { get; set; }

        public IGroupRepository GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new Repositories.GroupRepository.GroupRepository(_context);
                }
                return _groupRepository;
            }
        }

        private IGroupViewerRepository _groupViewerRepository { get; set; }

        public IGroupViewerRepository GroupViewerRepository
        {
            get
            {
                if (_groupViewerRepository == null)
                {
                    _groupViewerRepository = new GroupViewerRepository(_context);
                }
                return _groupViewerRepository;
            }
        }

        private IGroupBlockListRepository _groupBlockListRepository { get; set; }

        public IGroupBlockListRepository GroupBlockListRepository
        {
            get
            {
                if (_groupBlockListRepository == null)
                {
                    _groupBlockListRepository = new GroupBlockListRepository(_context);
                }
                return _groupBlockListRepository;
            }
        }

        private IGroupCommentResourcesRepository _groupCommentResourcesRepository { get; set; }

        public IGroupCommentResourcesRepository GroupCommentResourcesRepository
        {
            get
            {
                if (_groupCommentResourcesRepository == null)
                {
                    _groupCommentResourcesRepository = new GroupCommentResourcesRepository(_context);
                }
                return _groupCommentResourcesRepository;
            }
        }

        private IGroupPhotoResourcesRepository _groupPhotoResourcesRepository { get; set; }

        public IGroupPhotoResourcesRepository GroupPhotoResourcesRepository
        {
            get
            {
                if (_groupPhotoResourcesRepository == null)
                {
                    _groupPhotoResourcesRepository = new GroupPhotoResourcesRepository(_context);
                }
                return _groupPhotoResourcesRepository;
            }
        }

        private IProfilePhotoResourcesRepository _profilePhotoResourcesRepository { get; set; }

        public IProfilePhotoResourcesRepository ProfilePhotoResourcesRepository
        {
            get
            {
                if (_profilePhotoResourcesRepository == null)
                {
                    _profilePhotoResourcesRepository = new ProfilePhotoResourcesRepository(_context);
                }
                return _profilePhotoResourcesRepository;
            }
        }

        private IProfileCommentResourcesRepository _profileCommentResourcesRepository { get; set; }

        public IProfileCommentResourcesRepository ProfileCommentResourcesRepository
        {
            get
            {
                if (_profileCommentResourcesRepository == null)
                {
                    _profileCommentResourcesRepository = new ProfileCommentResourcesRepository(_context);
                }
                return _profileCommentResourcesRepository;
            }
        }

        private IGroupCommentRepository _groupCommentRepository { get; set; }

        public IGroupCommentRepository GroupCommentRepository
        {
            get
            {
                if (_groupCommentRepository == null)
                {
                    _groupCommentRepository = new GroupCommentRepository(_context);
                }
                return _groupCommentRepository;
            }
        }

        private IGroupCommentPingsRepository _groupCommentPingsRepository { get; set; }

        public IGroupCommentPingsRepository GroupCommentPingsRepository
        {
            get
            {
                if (_groupCommentPingsRepository == null)
                {
                    _groupCommentPingsRepository = new GroupCommentPingsRepository(_context);
                }
                return _groupCommentPingsRepository;
            }
        }

        private IGroupCommentTransactionRepository _groupCommentTransactionRepository { get; set; }

        public IGroupCommentTransactionRepository GroupCommentTransactionRepository
        {
            get
            {
                if (_groupCommentTransactionRepository == null)
                {
                    _groupCommentTransactionRepository = new GroupCommentTransactionRepository(_context);
                }
                return _groupCommentTransactionRepository;
            }
        }

        private IGroupCommentUpvotedUserRepository _groupCommentUpvotedUserRepository { get; set; }

        public IGroupCommentUpvotedUserRepository GroupCommentUpvotedUserRepository
        {
            get
            {
                if (_groupCommentUpvotedUserRepository == null)
                {
                    _groupCommentUpvotedUserRepository = new GroupCommentUpvotedUserRepository(_context);
                }
                return _groupCommentUpvotedUserRepository;
            }
        }

        private IGroupMemberRepository _groupMemberRepository { get; set; }

        public IGroupMemberRepository GroupMemberRepository
        {
            get
            {
                if (_groupMemberRepository == null)
                {
                    _groupMemberRepository = new GroupMemberRepository(_context);
                }
                return _groupMemberRepository;
            }
        }

        private IGroupMemberRequestRepository _groupMemberRequestRepository { get; set; }

        public IGroupMemberRequestRepository GroupMemberRequestRepository
        {
            get
            {
                if (_groupMemberRequestRepository == null)
                {
                    _groupMemberRequestRepository = new GroupMemberRequestRepository(_context);
                }
                return _groupMemberRequestRepository;
            }
        }

        private IGroupMessageRepository _groupMessageRepository { get; set; }

        public IGroupMessageRepository GroupMessageRepository
        {
            get
            {
                if (_groupMessageRepository == null)
                {
                    _groupMessageRepository = new GroupMessageRepository(_context);
                }
                return _groupMessageRepository;
            }
        }

        private IGroupNotificationRepository _groupNotificationRepository { get; set; }

        public IGroupNotificationRepository GroupNotificationRepository
        {
            get
            {
                if (_groupNotificationRepository == null)
                {
                    _groupNotificationRepository = new GroupNotificationRepository(_context);
                }
                return _groupNotificationRepository;
            }
        }

        private IProfileRepository _profileRepository { get; set; }

        public IProfileRepository ProfileRepository
        {
            get
            {
                if (_profileRepository == null)
                {
                    _profileRepository = new ProfileRepository(_context);
                }

                return _profileRepository;
            }
        }

        private IProfileBlockListRepository _profileBlockListRepository { get; set; }

        public IProfileBlockListRepository ProfileBlockListRepository
        {
            get
            {
                if (_profileBlockListRepository == null)
                {
                    _profileBlockListRepository = new ProfileBlockListRepository(_context);
                }

                return _profileBlockListRepository;
            }
        }

        private IProfileCommentRepository _profileCommentRepository { get; set; }

        public IProfileCommentRepository ProfileCommentRepository
        {
            get
            {
                if (_profileCommentRepository == null)
                {
                    _profileCommentRepository = new ProfileCommentRepository(_context);
                }

                return _profileCommentRepository;
            }
        }

        private IProfileCommentPingsRepository _profileCommentPingsRepository { get; set; }

        public IProfileCommentPingsRepository ProfileCommentPingsRepository
        {
            get
            {
                if (_profileCommentPingsRepository == null)
                {
                    _profileCommentPingsRepository = new ProfileCommentPingsRepository(_context);
                }

                return _profileCommentPingsRepository;
            }
        }

        private IProfileCommentTransactionRepository _profileCommentTransactionRepository { get; set; }

        public IProfileCommentTransactionRepository ProfileCommentTransactionRepository
        {
            get
            {
                if (_profileCommentTransactionRepository == null)
                {
                    _profileCommentTransactionRepository = new ProfileCommentTransactionRepository(_context);
                }

                return _profileCommentTransactionRepository;
            }
        }

        private IProfileCommentUpvotedUserRepository _profileCommentUpvotedUserRepository { get; set; }

        public IProfileCommentUpvotedUserRepository ProfileCommentUpvotedUserRepository
        {
            get
            {
                if (_profileCommentUpvotedUserRepository == null)
                {
                    _profileCommentUpvotedUserRepository = new ProfileCommentUpvotedUserRepository(_context);
                }

                return _profileCommentUpvotedUserRepository;
            }
        }

        private IProfileFollowerRepository _profileFollowerRepository { get; set; }

        public IProfileFollowerRepository ProfileFollowerRepository
        {
            get
            {
                if (_profileFollowerRepository == null)
                {
                    _profileFollowerRepository = new ProfileFollowerRepository(_context);
                }

                return _profileFollowerRepository;
            }
        }

        private IProfileFriendRepository _profileFriendRepository { get; set; }

        public IProfileFriendRepository ProfileFriendRepository
        {
            get
            {
                if (_profileFriendRepository == null)
                {
                    _profileFriendRepository = new ProfileFriendRepository(_context);
                }

                return _profileFriendRepository;
            }
        }

        private IProfileMessageRepository _profileMessageRepository { get; set; }

        public IProfileMessageRepository ProfileMessageRepository
        {
            get
            {
                if (_profileMessageRepository == null)
                {
                    _profileMessageRepository = new ProfileMessageRepository(_context);
                }

                return _profileMessageRepository;
            }
        }

        private IProfileNotificationRepository _profileNotificationRepository { get; set; }

        public IProfileNotificationRepository ProfileNotificationRepository
        {
            get
            {
                if (_profileNotificationRepository == null)
                {
                    _profileNotificationRepository = new ProfileNotificationRepository(_context);
                }

                return _profileNotificationRepository;
            }
        }

        private IProfileViewerRepository _profileViewerRepository { get; set; }

        public IProfileViewerRepository ProfileViewerRepository
        {
            get
            {
                if (_profileViewerRepository == null)
                {
                    _profileViewerRepository = new ProfileViewerRepository(_context);
                }

                return _profileViewerRepository;
            }
        }

        private IUserLoginLogsRepository _userLoginLogsRepository { get; set; }

        public IUserLoginLogsRepository UserLoginLogsRepository
        {
            get
            {
                if (_userLoginLogsRepository == null)
                {
                    _userLoginLogsRepository = new UserLoginLogsRepository(_context);
                }

                return _userLoginLogsRepository;
            }
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync(default);
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }
}