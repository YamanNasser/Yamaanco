using System;
using System.Collections.Generic;
using Yamaanco.Domain.Common;
using Yamaanco.Domain.Entities.GroupEntities;
using Yamaanco.Domain.Enums;
using Yamaanco.Domain.ValueObjects;

namespace Yamaanco.Domain.Entities.ProfileEntities
{
    public class Profile : AuditableEntity
    {
        public Profile()
        {
        }

        public Profile(string id, string firstName, string lastName, int genderId, DateTime? birthDate, string phoneNumber, string email, string country, string city, string address, string aboutMe) : base(id)
        {
            PhotoResources = new HashSet<ProfilePhotoResources>();
            Messages = new HashSet<ProfileMessage>();
            Comments = new HashSet<ProfileComment>();
            Viewers = new HashSet<ProfileViewer>();
            Followers = new HashSet<ProfileFollower>();
            Friends = new HashSet<ProfileFriend>();
            BlockList = new HashSet<ProfileBlockList>();
            Notifications = new HashSet<ProfileNotification>();
            Groups = new HashSet<Group>();
            CommentTransactions = new HashSet<ProfileCommentTransaction>();

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            AboutMe = aboutMe;
            GenderId = genderId;
            BirthDate = birthDate;
            NumberOfFollowers = 0;
            NumberOfViewers = 0;
            Address = address;
            City = city;
            Country = country;
            LastModifiedById = id;
            AddDefaultPhoto();
        }

        public void Update(string firstName, string lastName, int genderId, DateTime? birthDate, string phoneNumber, string email, string country, string city, string address, string aboutMe)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            AboutMe = aboutMe;
            GenderId = genderId;
            BirthDate = birthDate;
            Address = address;
            City = city;
            Country = country;
            LastModifiedById = Id;
        }

        public int NewViewer(string id)
        {
            Viewers.Add(new ProfileViewer(
                profileId: Id,
                viewerProfileId: id
            ));
            NumberOfViewers += 1;
            return NumberOfViewers;
        }

        public int NewFollower(string id)
        {
            Followers.Add(new ProfileFollower(
                profileId: Id,
                followerProfileId: id
            ));
            NumberOfFollowers += 1;
            return NumberOfFollowers;
        }

        private void AddDefaultPhoto()
        {
            foreach (var photoSize in
             new[] { PhotoSize.Default, PhotoSize.Large, PhotoSize.Medium, PhotoSize.Small })
            {
                PhotoResources.Add(
                 new ProfilePhotoResources(
                     folderName: "",
                     photoSize: photoSize,
                     profileId: Id,
                     description: AboutMe
                     ));
            }
        }

        public ICollection<ProfilePhotoResources> PhotoResources { get; private set; }
        public ICollection<ProfileMessage> Messages { get; private set; }
        public ICollection<ProfileComment> Comments { get; private set; }
        public ICollection<ProfileViewer> Viewers { get; private set; }
        public ICollection<ProfileFollower> Followers { get; private set; }
        public ICollection<ProfileFriend> Friends { get; private set; }
        public ICollection<ProfileBlockList> BlockList { get; private set; }
        public ICollection<ProfileNotification> Notifications { get; private set; }
        public ICollection<Group> Groups { get; private set; }
        public ICollection<ProfileCommentTransaction> CommentTransactions { get; private set; }

        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public string UserName
        {
            get => (new UserName(FirstName, LastName)).ToString();
            private set
            {
                _ = (new UserName(FirstName, LastName)).ToString();
            }
        }

        public string PhoneNumber { get; private set; }
        public string AboutMe { get; private set; }
        public int GenderId { get; private set; }
        public Gender Gender { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public int NumberOfFollowers { get; private set; }
        public int NumberOfViewers { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public bool IsDeactivated { get; private set; } //TODO: it will be available in future work.
    }
}