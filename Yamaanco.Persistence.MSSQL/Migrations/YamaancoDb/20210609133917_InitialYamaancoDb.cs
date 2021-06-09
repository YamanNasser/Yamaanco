using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Migrations.YamaancoDb
{
    public partial class InitialYamaancoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserActionId = table.Column<int>(type: "int", nullable: false),
                    UserAction = table.Column<int>(type: "int", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumberOfFollowers = table.Column<int>(type: "int", nullable: false),
                    NumberOfViewers = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactivated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfViewers = table.Column<int>(type: "int", nullable: false),
                    NumberOfMembers = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_GroupType_GroupTypeId",
                        column: x => x.GroupTypeId,
                        principalTable: "GroupType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileBlockList",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlockProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileBlockList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileBlockList_Profile_BlockProfileId",
                        column: x => x.BlockProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileBlockList_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileComment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Parent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Root = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpvoteCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Classification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileComment_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileFollower",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FollowerProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FollowedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    SeenDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileFollower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileFollower_Profile_FollowerProfileId",
                        column: x => x.FollowerProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileFollower_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileFriend",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileFriend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileFriend_Profile_FriendProfileId",
                        column: x => x.FriendProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileFriend_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileMessage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileMessage_Profile_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileMessage_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePhotoResources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoSize = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePhotoResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePhotoResources_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileViewer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ViewerProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ViewerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileViewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileViewer_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileViewer_Profile_ViewerProfileId",
                        column: x => x.ViewerProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupBlockList",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlockProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupBlockList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupBlockList_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupBlockList_Profile_BlockProfileId",
                        column: x => x.BlockProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupComment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Parent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Root = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpvoteCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Classification = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupComment_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MemberId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMember_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMember_Profile_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMemberRequest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InviterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InvitedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMemberRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMemberRequest_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMemberRequest_Profile_InviterId",
                        column: x => x.InviterId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMessage_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMessage_Profile_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupPhotoResources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoSize = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPhotoResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPhotoResources_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupViewer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ViewerProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ViewerDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupViewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupViewer_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupViewer_Profile_ViewerProfileId",
                        column: x => x.ViewerProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCommentHashtag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Hashtag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCommentHashtag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileCommentHashtag_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCommentHashtag_ProfileComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "ProfileComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCommentPings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MentionedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCommentPings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileCommentPings_Profile_MentionedUserId",
                        column: x => x.MentionedUserId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCommentPings_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCommentPings_ProfileComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "ProfileComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCommentResources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCommentResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileCommentResources_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCommentResources_ProfileComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "ProfileComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCommentTransaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileCommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentRoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentParent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentTransactionType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCommentTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileCommentTransaction_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCommentTransaction_ProfileComment_ProfileCommentId",
                        column: x => x.ProfileCommentId,
                        principalTable: "ProfileComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCommentUpvotedUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCommentUpvotedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileCommentUpvotedUser_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCommentUpvotedUser_ProfileComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "ProfileComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCommentViewer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ViewerProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCommentViewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileCommentViewer_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCommentViewer_Profile_ViewerProfileId",
                        column: x => x.ViewerProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCommentViewer_ProfileComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "ProfileComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileNotification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    SourceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    NotificationCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileNotification_Profile_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileNotification_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileNotification_ProfileComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "ProfileComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileMessageResources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileMessageResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileMessageResources_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileMessageResources_ProfileMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ProfileMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileMessageViewer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileMessageViewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileMessageViewer_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileMessageViewer_ProfileMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ProfileMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupCommentHashtag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Hashtag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCommentHashtag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupCommentHashtag_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentHashtag_GroupComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "GroupComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupCommentPings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MentionedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCommentPings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupCommentPings_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentPings_GroupComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "GroupComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentPings_Profile_MentionedUserId",
                        column: x => x.MentionedUserId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupCommentResources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCommentResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupCommentResources_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentResources_GroupComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "GroupComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupCommentTransaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentRoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentParent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentTransactionType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCommentTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupCommentTransaction_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentTransaction_GroupComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "GroupComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupCommentUpvotedUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCommentUpvotedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupCommentUpvotedUser_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentUpvotedUser_GroupComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "GroupComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentUpvotedUser_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupCommentViewer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ViewerProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCommentViewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupCommentViewer_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentViewer_GroupComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "GroupComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupCommentViewer_Profile_ViewerProfileId",
                        column: x => x.ViewerProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupNotification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    SourceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    NotificationCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupNotification_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupNotification_GroupComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "GroupComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupNotification_Profile_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupNotification_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessageResources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessageResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMessageResources_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMessageResources_GroupMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "GroupMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessageViewer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessageViewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMessageViewer_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMessageViewer_GroupMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "GroupMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMessageViewer_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_GroupTypeId",
                table: "Group",
                column: "GroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ProfileId",
                table: "Group",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupBlockList_BlockProfileId",
                table: "GroupBlockList",
                column: "BlockProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupBlockList_GroupId",
                table: "GroupBlockList",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupComment_GroupId",
                table: "GroupComment",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentHashtag_CommentId",
                table: "GroupCommentHashtag",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentHashtag_GroupId",
                table: "GroupCommentHashtag",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentPings_CommentId",
                table: "GroupCommentPings",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentPings_GroupId",
                table: "GroupCommentPings",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentPings_MentionedUserId",
                table: "GroupCommentPings",
                column: "MentionedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentResources_CommentId",
                table: "GroupCommentResources",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentResources_GroupId",
                table: "GroupCommentResources",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentTransaction_CommentId",
                table: "GroupCommentTransaction",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentTransaction_GroupId",
                table: "GroupCommentTransaction",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentUpvotedUser_CommentId",
                table: "GroupCommentUpvotedUser",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentUpvotedUser_GroupId",
                table: "GroupCommentUpvotedUser",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentUpvotedUser_ProfileId",
                table: "GroupCommentUpvotedUser",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentViewer_CommentId",
                table: "GroupCommentViewer",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentViewer_GroupId",
                table: "GroupCommentViewer",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentViewer_ViewerProfileId",
                table: "GroupCommentViewer",
                column: "ViewerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_GroupId",
                table: "GroupMember",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_MemberId",
                table: "GroupMember",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberRequest_GroupId",
                table: "GroupMemberRequest",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberRequest_InviterId",
                table: "GroupMemberRequest",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessage_CreatedById",
                table: "GroupMessage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessage_GroupId",
                table: "GroupMessage",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessageResources_GroupId",
                table: "GroupMessageResources",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessageResources_MessageId",
                table: "GroupMessageResources",
                column: "MessageId",
                unique: true,
                filter: "[MessageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessageViewer_GroupId",
                table: "GroupMessageViewer",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessageViewer_MessageId",
                table: "GroupMessageViewer",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessageViewer_ProfileId",
                table: "GroupMessageViewer",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotification_CommentId",
                table: "GroupNotification",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotification_GroupId",
                table: "GroupNotification",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotification_ParticipantId",
                table: "GroupNotification",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupNotification_ProfileId",
                table: "GroupNotification",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPhotoResources_GroupId",
                table: "GroupPhotoResources",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupViewer_GroupId",
                table: "GroupViewer",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupViewer_ViewerProfileId",
                table: "GroupViewer",
                column: "ViewerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Email",
                table: "Profile",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_GenderId",
                table: "Profile",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_PhoneNumber",
                table: "Profile",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_UserName",
                table: "Profile",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileBlockList_BlockProfileId",
                table: "ProfileBlockList",
                column: "BlockProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileBlockList_ProfileId",
                table: "ProfileBlockList",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComment_ProfileId",
                table: "ProfileComment",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentHashtag_CommentId",
                table: "ProfileCommentHashtag",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentHashtag_ProfileId",
                table: "ProfileCommentHashtag",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentPings_CommentId",
                table: "ProfileCommentPings",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentPings_MentionedUserId",
                table: "ProfileCommentPings",
                column: "MentionedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentPings_ProfileId",
                table: "ProfileCommentPings",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentResources_CommentId",
                table: "ProfileCommentResources",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentResources_ProfileId",
                table: "ProfileCommentResources",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentTransaction_ProfileCommentId",
                table: "ProfileCommentTransaction",
                column: "ProfileCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentTransaction_ProfileId",
                table: "ProfileCommentTransaction",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentUpvotedUser_CommentId",
                table: "ProfileCommentUpvotedUser",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentUpvotedUser_ProfileId",
                table: "ProfileCommentUpvotedUser",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentViewer_CommentId",
                table: "ProfileCommentViewer",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentViewer_ProfileId",
                table: "ProfileCommentViewer",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCommentViewer_ViewerProfileId",
                table: "ProfileCommentViewer",
                column: "ViewerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFollower_FollowerProfileId",
                table: "ProfileFollower",
                column: "FollowerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFollower_ProfileId",
                table: "ProfileFollower",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFriend_FriendProfileId",
                table: "ProfileFriend",
                column: "FriendProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFriend_ProfileId",
                table: "ProfileFriend",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMessage_CreatedById",
                table: "ProfileMessage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMessage_ProfileId",
                table: "ProfileMessage",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMessageResources_MessageId",
                table: "ProfileMessageResources",
                column: "MessageId",
                unique: true,
                filter: "[MessageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMessageResources_ProfileId",
                table: "ProfileMessageResources",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMessageViewer_MessageId",
                table: "ProfileMessageViewer",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMessageViewer_ProfileId",
                table: "ProfileMessageViewer",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileNotification_CommentId",
                table: "ProfileNotification",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileNotification_ParticipantId",
                table: "ProfileNotification",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileNotification_ProfileId",
                table: "ProfileNotification",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhotoResources_ProfileId",
                table: "ProfilePhotoResources",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileViewer_ProfileId",
                table: "ProfileViewer",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileViewer_ViewerProfileId",
                table: "ProfileViewer",
                column: "ViewerProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupBlockList");

            migrationBuilder.DropTable(
                name: "GroupCommentHashtag");

            migrationBuilder.DropTable(
                name: "GroupCommentPings");

            migrationBuilder.DropTable(
                name: "GroupCommentResources");

            migrationBuilder.DropTable(
                name: "GroupCommentTransaction");

            migrationBuilder.DropTable(
                name: "GroupCommentUpvotedUser");

            migrationBuilder.DropTable(
                name: "GroupCommentViewer");

            migrationBuilder.DropTable(
                name: "GroupMember");

            migrationBuilder.DropTable(
                name: "GroupMemberRequest");

            migrationBuilder.DropTable(
                name: "GroupMessageResources");

            migrationBuilder.DropTable(
                name: "GroupMessageViewer");

            migrationBuilder.DropTable(
                name: "GroupNotification");

            migrationBuilder.DropTable(
                name: "GroupPhotoResources");

            migrationBuilder.DropTable(
                name: "GroupViewer");

            migrationBuilder.DropTable(
                name: "ProfileBlockList");

            migrationBuilder.DropTable(
                name: "ProfileCommentHashtag");

            migrationBuilder.DropTable(
                name: "ProfileCommentPings");

            migrationBuilder.DropTable(
                name: "ProfileCommentResources");

            migrationBuilder.DropTable(
                name: "ProfileCommentTransaction");

            migrationBuilder.DropTable(
                name: "ProfileCommentUpvotedUser");

            migrationBuilder.DropTable(
                name: "ProfileCommentViewer");

            migrationBuilder.DropTable(
                name: "ProfileFollower");

            migrationBuilder.DropTable(
                name: "ProfileFriend");

            migrationBuilder.DropTable(
                name: "ProfileMessageResources");

            migrationBuilder.DropTable(
                name: "ProfileMessageViewer");

            migrationBuilder.DropTable(
                name: "ProfileNotification");

            migrationBuilder.DropTable(
                name: "ProfilePhotoResources");

            migrationBuilder.DropTable(
                name: "ProfileViewer");

            migrationBuilder.DropTable(
                name: "UserLoginLogs");

            migrationBuilder.DropTable(
                name: "GroupMessage");

            migrationBuilder.DropTable(
                name: "GroupComment");

            migrationBuilder.DropTable(
                name: "ProfileMessage");

            migrationBuilder.DropTable(
                name: "ProfileComment");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "GroupType");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
