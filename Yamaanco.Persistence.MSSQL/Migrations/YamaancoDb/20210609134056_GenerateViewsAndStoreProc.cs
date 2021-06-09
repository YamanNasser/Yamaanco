using Microsoft.EntityFrameworkCore.Migrations;

namespace Yamaanco.Infrastructure.EF.Persistence.MSSQL.Migrations.YamaancoDb
{
    public partial class GenerateViewsAndStoreProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
CREATE VIEW [dbo].[vwProfileComments]
AS
SELECT        dbo.ProfileComment.Id, dbo.ProfileComment.CreatedById, dbo.ProfileComment.Created, dbo.ProfileComment.LastModified AS Modified, dbo.ProfileComment.Parent, dbo.ProfileComment.Root, dbo.ProfileComment.[Content], 
                         dbo.ProfileComment.UpvoteCount, dbo.ProfileComment.ViewCount, dbo.ProfileComment.Category, dbo.ProfileComment.Type, dbo.ProfileComment.Classification, dbo.ProfileComment.ProfileId AS CategoryId, 
                         dbo.Profile.UserName AS CategoryName, Profile_1.UserName AS CreatorName, dbo.ProfileCommentPings.MentionedUserId AS UserId, Profile_2.UserName, dbo.ProfileCommentResources.FullPath AS Path, 
                         dbo.ProfileCommentResources.Extension, dbo.ProfileCommentResources.Id AS FileId, dbo.ProfilePhotoResources.FullPath AS ProfilePictureUrl
FROM            dbo.ProfileCommentResources RIGHT OUTER JOIN
                         dbo.ProfileComment INNER JOIN
                         dbo.Profile ON dbo.ProfileComment.ProfileId = dbo.Profile.Id AND dbo.ProfileComment.IsDeleted = 0 INNER JOIN
                         dbo.Profile AS Profile_1 ON dbo.ProfileComment.CreatedById = Profile_1.Id ON dbo.ProfileCommentResources.CommentId = dbo.ProfileComment.Id LEFT OUTER JOIN
                         dbo.Profile AS Profile_2 INNER JOIN
                         dbo.ProfileCommentPings ON Profile_2.Id = dbo.ProfileCommentPings.MentionedUserId ON dbo.ProfileComment.Id = dbo.ProfileCommentPings.CommentId LEFT OUTER JOIN
                         dbo.ProfilePhotoResources ON dbo.Profile.Id = dbo.ProfilePhotoResources.ProfileId AND dbo.ProfilePhotoResources.PhotoSize = 45

                                  ");

            migrationBuilder.Sql($@"
CREATE VIEW [dbo].[vwGroupComments]
AS
SELECT        dbo.GroupComment.Id, dbo.GroupComment.CreatedById, dbo.GroupComment.Created, dbo.GroupComment.LastModified AS Modified, dbo.GroupComment.Parent, dbo.GroupComment.Root, dbo.GroupComment.[Content], 
                         dbo.GroupComment.UpvoteCount, dbo.GroupComment.ViewCount, dbo.GroupComment.Category, dbo.GroupComment.Type, dbo.GroupComment.Classification, dbo.GroupComment.GroupId AS CategoryId, 
                         dbo.[Group].Name AS CategoryName, Profile_1.UserName AS CreatorName, dbo.GroupCommentPings.MentionedUserId AS UserId, Profile_2.UserName, dbo.ProfileCommentResources.FullPath AS Path, 
                         dbo.ProfileCommentResources.Extension, dbo.ProfileCommentResources.Id AS FileId, dbo.ProfilePhotoResources.FullPath AS ProfilePictureUrl
FROM            dbo.ProfileCommentResources LEFT OUTER JOIN
                         dbo.ProfilePhotoResources ON dbo.ProfileCommentResources.ProfileId = dbo.ProfilePhotoResources.ProfileId AND dbo.ProfilePhotoResources.PhotoSize = 45 RIGHT OUTER JOIN
                         dbo.GroupComment INNER JOIN
                         dbo.[Group] ON dbo.GroupComment.GroupId = dbo.[Group].Id AND dbo.GroupComment.IsDeleted = 0 INNER JOIN
                         dbo.Profile AS Profile_1 ON dbo.GroupComment.CreatedById = Profile_1.Id ON dbo.ProfileCommentResources.CommentId = dbo.GroupComment.Id LEFT OUTER JOIN
                         dbo.Profile AS Profile_2 INNER JOIN
                         dbo.GroupCommentPings ON Profile_2.Id = dbo.GroupCommentPings.MentionedUserId ON dbo.GroupComment.Id = dbo.GroupCommentPings.CommentId

                                  ");

            migrationBuilder.Sql($@"
CREATE VIEW [dbo].[vwComments]
AS
select * from vwProfileComments
UNION ALL
select * from vwGroupComments

                                  ");

            migrationBuilder.Sql($@"
CREATE VIEW [dbo].[vwCommentsType]
AS
SELECT        Id, 0 AS Type
FROM            dbo.ProfileComment
union all
SELECT        Id, 10 AS Type
FROM            dbo.GroupComment

                                  ");

            migrationBuilder.Sql($@"
CREATE  VIEW [dbo].[vwCreateProfileFollowerBasicInfo]
AS
SELECT        dbo.ProfileFollower.ProfileId, dbo.ProfileFollower.FollowedDate, dbo.Profile.Email AS ProfileEmail, dbo.Profile.PhoneNumber AS ProfilePhone, dbo.Profile.UserName AS ProfileUserName, 
                         dbo.ProfileFollower.FollowerProfileId AS FollowerId, dbo.Profile.NumberOfFollowers AS NumberOfProfileFollowers, Profile_1.UserName AS FollowerName, 
                         dbo.ProfilePhotoResources.FullPath AS FollowerProfileMediumPhotoPath
FROM            dbo.Profile AS Profile_1 INNER JOIN
                         dbo.Profile INNER JOIN
                         dbo.ProfileFollower ON dbo.Profile.Id = dbo.ProfileFollower.ProfileId ON Profile_1.Id = dbo.ProfileFollower.FollowerProfileId INNER JOIN
                         dbo.ProfilePhotoResources ON Profile_1.Id = dbo.ProfilePhotoResources.ProfileId AND dbo.ProfilePhotoResources.PhotoSize = 45
GO

                                  ");

            migrationBuilder.Sql($@"
CREATE VIEW [dbo].[vwCreatedGroupMemberBasicInfo]
AS
SELECT        dbo.Profile.UserName AS MemberName, dbo.Profile.PhoneNumber, dbo.Profile.GenderId, dbo.Profile.BirthDate, dbo.Profile.City, dbo.Profile.Country, dbo.GroupMember.IsAdmin, dbo.GroupMember.JoinDate, 
                         dbo.GroupMember.MemberId, dbo.GroupMember.GroupId, dbo.Profile.NumberOfViewers, dbo.Profile.NumberOfFollowers, dbo.Profile.Address, dbo.Profile.Email, 
                         dbo.ProfilePhotoResources.FullPath AS MemberProfileMediumPhotoPath, dbo.[Group].Name AS GroupName
FROM            dbo.Profile INNER JOIN
                         dbo.GroupMember ON dbo.Profile.Id = dbo.GroupMember.MemberId INNER JOIN
                         dbo.[Group] ON dbo.GroupMember.GroupId = dbo.[Group].Id LEFT OUTER JOIN
                         dbo.ProfilePhotoResources ON dbo.Profile.Id = dbo.ProfilePhotoResources.ProfileId AND dbo.ProfilePhotoResources.PhotoSize = 45
WHERE        (dbo.Profile.IsDeleted = 0)
GO



                                  ");
            migrationBuilder.Sql($@"
CREATE VIEW [dbo].[vwNotifications]
AS
SELECT      dbo.ProfileNotification.id,  dbo.ProfileNotification.ProfileId, dbo.Profile.UserName AS ParticipantName, dbo.ProfileNotification.NotificationType, dbo.ProfileNotification.ParticipantId, dbo.ProfileNotification.IsSeen, 
                         dbo.ProfileNotification.NotificationCategory AS CategoryType, dbo.Profile.GenderId, dbo.ProfileNotification.[Content], dbo.ProfilePhotoResources.FullPath AS ParticipantPhotoPath, dbo.ProfileNotification.CreatedDate, 
                         dbo.ProfileNotification.Target, dbo.ProfileNotification.ProfileId AS CategoryId, Profile_1.UserName AS CategoryName
FROM            dbo.ProfileNotification INNER JOIN
                         dbo.Profile ON dbo.ProfileNotification.ParticipantId = dbo.Profile.Id INNER JOIN
                         dbo.ProfilePhotoResources ON dbo.Profile.Id = dbo.ProfilePhotoResources.ProfileId AND dbo.ProfilePhotoResources.PhotoSize = 45 INNER JOIN
                         dbo.Profile AS Profile_1 ON dbo.ProfileNotification.ProfileId = Profile_1.Id
UNION ALL
SELECT     dbo.GroupNotification.id,    dbo.GroupNotification.ProfileId, dbo.Profile.UserName AS ParticipantName, dbo.GroupNotification.NotificationType, dbo.GroupNotification.ParticipantId, dbo.GroupNotification.IsSeen, 
                         dbo.GroupNotification.NotificationCategory AS CategoryType, dbo.Profile.GenderId, dbo.GroupNotification.[Content] AS Message, dbo.ProfilePhotoResources.FullPath AS ParticipantPhotoPath, dbo.GroupNotification.CreatedDate, 
                         dbo.GroupNotification.Target, dbo.GroupNotification.GroupId AS CategoryId, dbo.[Group].Name AS CategoryName
FROM            dbo.GroupNotification INNER JOIN
                         dbo.Profile ON dbo.GroupNotification.ParticipantId = dbo.Profile.Id INNER JOIN
                         dbo.ProfilePhotoResources ON dbo.Profile.Id = dbo.ProfilePhotoResources.ProfileId AND dbo.ProfilePhotoResources.PhotoSize = 45 INNER JOIN
                         dbo.[Group] ON dbo.GroupNotification.GroupId = dbo.[Group].Id

                                  ");

            migrationBuilder.Sql($@"
CREATE PROCEDURE [dbo].[spCreateGroupMember]
(
     @groupId varchar(100)
    , @memberId varchar(100)
)
AS
BEGIN

DECLARE @Num as int= (select count(*) from  GroupMember where GroupId=@groupId and MemberId=@memberId);
 if @Num=0
	  begin

    BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;

    BEGIN TRY

	   insert into GroupMember([Id],[GroupId],[MemberId],[JoinDate],[IsAdmin]) 
	  values(CONVERT(varchar(100), newid()),@groupId,@memberId,GETDATE(),0);

	  update [Group] set NumberOfMembers=NumberOfMembers+1 where id=@groupId;
	 
        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
	 end 

	 select GroupId,GroupName ,MemberId,MemberProfileMediumPhotoPath,MemberName,PhoneNumber,Email,City,Country,IsAdmin,JoinDate
	        ,(select count(*) from GroupMember where GroupId=@groupId)  as NumberOfGroupMembers
			from [dbo].[vwCreatedGroupMemberBasicInfo]
			where GroupId=@groupId and MemberId=@memberId
END;

                                  ");

            migrationBuilder.Sql($@"

Create PROCEDURE [dbo].[spCreateGroupViewer]
(
     @groupId varchar(100)
    , @viewerId varchar(100)
)
AS
BEGIN
DECLARE @groupTypeId as int=(select GroupTypeId from  [Group] where  Id=@groupId);

DECLARE @Num as int= (select count(*) from  GroupViewer where ViewerProfileId=@viewerId and GroupId=@groupId);
 if @Num=0 and @GroupTypeId in (1,2)
	  begin

    BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;

    BEGIN TRY

	   insert into GroupViewer([Id],[GroupId],[ViewerProfileId],[ViewerDate]) 
	  values(CONVERT(varchar(100), newid()),@groupId,@viewerId,GETDATE());

	  update [Group] set NumberOfViewers=NumberOfViewers+1 where id=@groupId;
	 
        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
	 end 
   select top (1) NumberOfViewers from [Group] where id=@groupId;
END;
                                  ");

            migrationBuilder.Sql($@"
CREATE PROCEDURE [dbo].[spCreateProfileFollower]
(
     @profileId varchar(100)
    , @followerId varchar(100)
)
AS
BEGIN

DECLARE @Num as int= (select count(*) from  ProfileFollower where FollowerProfileId=@followerId and ProfileId=@profileId);
 if @Num=0
	  begin

    BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;

    BEGIN TRY

	   insert into ProfileFollower ([Id],[ProfileId],[FollowerProfileId],[FollowedDate],SeenDate,IsSeen) 
	  values(CONVERT(varchar(100), newid()),@profileId,@followerId,GETDATE(),'0001-01-01 00:00:00.0000000',0);

	  update profile set NumberOfFollowers=NumberOfFollowers+1 where id=@profileId;
	 
        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
	 end 

	 	   select  ProfileEmail,[NumberOfProfileFollowers],ProfileId,ProfileUserName,ProfilePhone,FollowerId,FollowerName,FollowedDate,FollowerProfileMediumPhotoPath,
		   (select count(*) from ProfileNotification where ProfileId=@profileId and (IsSeen is null or IsSeen=0) and NotificationType =30 ) as NumberOfUnSeenProfileFollowers
		   from vwCreateProfileFollowerBasicInfo
		   where FollowerId=@followerId and  ProfileId=@profileId;
END;

                                  ");

            migrationBuilder.Sql($@"

Create PROCEDURE [dbo].[spCreateProfileViewer]
(
     @profileId varchar(100)
    , @viewerId varchar(100)
)
AS
BEGIN

DECLARE @Num as int= (select count(*) from  ProfileViewer where ViewerProfileId=@viewerId and ProfileId=@profileId);
 if @Num=0
	  begin

    BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;

    BEGIN TRY

	   insert into ProfileViewer([Id],[ProfileId],[ViewerProfileId],[ViewerDate]) 
	  values(CONVERT(varchar(100), newid()),@profileId,@viewerId,GETDATE());

	  update profile set NumberOfViewers=NumberOfViewers+1 where id=@profileId;
	 
        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
	 end 
   select top (1) NumberOfViewers from Profile where id=@profileId;
END;

                                  ");

            migrationBuilder.Sql($@"


CREATE PROCEDURE [dbo].[spDeleteGroupMember]
(
     @groupId varchar(100)
    , @memberId varchar(100)
)
AS
BEGIN
	DECLARE @Num as int= (select count(*) from  GroupMember where GroupId=@groupId and MemberId=@memberId);
	 if @Num>0
	  begin
    BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;

    BEGIN TRY

	  delete from GroupMember where  GroupId=@groupId and MemberId=@memberId;
	  update [Group] set NumberOfMembers=NumberOfMembers-1 where id=@groupId;
	  
      COMMIT TRANSACTION 
		
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
	 end 
	 select count(*) from GroupMember where GroupId=@groupId;
END;
                                  ");
            migrationBuilder.Sql($@"
CREATE PROCEDURE [dbo].[spDeleteProfileFollower]
(
     @profileId varchar(100)
    , @followerId varchar(100)
)
AS
BEGIN
	DECLARE @Num as int= (select count(*) from  ProfileFollower where FollowerProfileId=@followerId and ProfileId=@profileId);
	 if @Num>0
	  begin
    BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;

    BEGIN TRY

	  delete from ProfileFollower where FollowerProfileId=@followerId and ProfileId=@profileId;
	  update profile set NumberOfFollowers=NumberOfFollowers-1 where id=@profileId;
	  
      COMMIT TRANSACTION 
		
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
	 end 
	 select count(*) from ProfileFollower where ProfileId=@profileId;
END;

                                  ");

            migrationBuilder.Sql($@"

Create  PROCEDURE [dbo].[spUpdateGroupCommentUpvoteCommand]
(
     @CommentId varchar(100)
    , @UpvotedUserId varchar(100)
)
AS
BEGIN
DECLARE @Num as int= (select count(*) from  GroupCommentUpvotedUser where  GroupId=@UpvotedUserId and CommentId=@CommentId);

BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;
    BEGIN TRY
if (@Num=0)
		  begin

	  insert into GroupCommentUpvotedUser ([Id],CommentId,GroupId,[Date]) 
	  values(CONVERT(varchar(100), newid()),@CommentId,@UpvotedUserId,GETDATE());

	  update GroupComment set UpvoteCount=UpvoteCount+1 where id=@CommentId;
	 
     --create transaction
		 insert into GroupCommentTransaction (CommentId,CommentRoot,CommentParent,[Data],GroupId,CommentTransactionType,CreatedDate,UserId,Id)
	 select Id,[Root],Parent,Content,GroupId,30,GETDATE(),CreatedById,newid()  as id
	 from GroupComment where Id=@commentId

	 end 
else
	 begin

 	   update GroupComment set UpvoteCount=UpvoteCount-1 where id=@CommentId;

	   delete from GroupCommentUpvotedUser   where CommentId=@CommentId and GroupId=@UpvotedUserId;
	--delete transaction
	 delete from GroupCommentTransaction where CommentId=@CommentId and GroupId=@upvotedUserId
	 end

	  COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
	 select Id,[Root],Parent,Content,GroupId as CategoryId,UpvoteCount,@UpvotedUserId as UpvoteById,
	(select top 1 UserName from profile where id =@UpvotedUserId) as UpvoteByName,
	(select top 1 UserName from profile where id =GroupId) as CategoryName,CreatedById
	from GroupComment  where id=@CommentId; 
END;
                                  ");

            migrationBuilder.Sql($@"
Create  PROCEDURE [dbo].[spUpdateProfileCommentUpvoteCommand]
(
     @CommentId varchar(100)
    , @UpvotedUserId varchar(100)
)
AS
BEGIN
DECLARE @Num as int= (select count(*) from  ProfileCommentUpvotedUser where ProfileId=@UpvotedUserId and CommentId=@CommentId);

BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;
    BEGIN TRY
if (@Num=0)
		  begin

	  insert into ProfileCommentUpvotedUser ([Id],CommentId,ProfileId,[Date]) 
	  values(CONVERT(varchar(100), newid()),@CommentId,@UpvotedUserId,GETDATE());

	  update ProfileComment set UpvoteCount=UpvoteCount+1 where id=@CommentId;
	 
     --create transaction
		 insert into ProfileCommentTransaction (CommentId,CommentRoot,CommentParent,[Data],ProfileId,CommentTransactionType,CreatedDate,UserId,Id)
	 select Id,[Root],Parent,Content,ProfileId,30,GETDATE(),CreatedById,newid()  as id
	 from ProfileComment where Id=@commentId

	 end 
else
	 begin

 	   update ProfileComment set UpvoteCount=UpvoteCount-1 where id=@CommentId;

	   delete from ProfileCommentUpvotedUser   where CommentId=@CommentId and ProfileId=@UpvotedUserId;
	--delete transaction
	 delete from ProfileCommentTransaction where CommentId=@CommentId and ProfileId=@upvotedUserId
	 end

	  COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH
	 select Id,[Root],Parent,Content,ProfileId as CategoryId,UpvoteCount,@UpvotedUserId as UpvoteById,
	(select top 1 UserName from profile where id =@UpvotedUserId) as UpvoteByName,
	(select top 1 UserName from profile where id =ProfileId) as CategoryName,CreatedById
	from ProfileComment  where id=@CommentId; 
END;

                                  ");

            migrationBuilder.Sql($@"

CREATE PROCEDURE [dbo].[spViewComments]
(
     @UserId varchar(100)
    , @PageSize int=15
	,@PageIndex int=1
)
AS
BEGIN

declare @tempTable as table (Id  nvarchar(450),
							[CreatedById]  nvarchar(450),
							Created datetime2(7),
							Modified datetime2(7),
							Parent nvarchar(max),
							Root nvarchar(max),
							Content nvarchar(max),
							UpvoteCount int,
							ViewCount int,
							Category int,
							Type int,
							Classification int,
							CategoryId nvarchar(450),
							CategoryName nvarchar(max),
							CreatorName nvarchar(450),
							UserId nvarchar(450),
							UserName nvarchar(450),
							Path nvarchar(max),
							Extension nvarchar(max),
							FileId nvarchar(450),
							ProfilePictureUrl nvarchar(max)
							);


declare @tempTable2 as table (Id  nvarchar(450),
[CreatedById]  nvarchar(450),
Created datetime2(7),
Modified datetime2(7),
Parent nvarchar(max),
Root nvarchar(max),
Content nvarchar(max),
UpvoteCount int,
ViewCount int,
Category int,
Type int,
Classification int,
CategoryId nvarchar(450),
CategoryName nvarchar(max),
CreatorName nvarchar(450)
);

insert into @tempTable
SELECT *
FROM [vwComments]
where Id in (
select Id from (select distinct id,Created
				from vwComments c
				where (c.CategoryId in 
						(select GroupId 
							from [GroupMember]
							where MemberId=@UserId) or c.CategoryId=@UserId)
				) com
order by com.Created desc
OFFSET (@PageIndex-1)*@PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY )

insert into @tempTable2
select distinct  Id ,
[CreatedById] ,
Created ,
Modified,
Parent ,
Root ,
Content ,
UpvoteCount ,
ViewCount ,
Category ,
Type ,
Classification ,
CategoryId ,
CategoryName,
CreatorName
from @tempTable

BEGIN TRANSACTION;
SAVE TRANSACTION MySavePoint;
BEGIN TRY



declare @profileType int=0
declare @groupType int=10
declare @viewTransaction int=40

-------------------------------------------------------------
declare @unSeenProfileComment table(Id nvarchar(100));
insert into @unSeenProfileComment
select * from(select Id from @tempTable2 where Root is null and Category=@profileType --return result
except											
select CommentId from ProfileCommentViewer where ViewerProfileId=@UserId--Prev seen result
) unSeenProfileComments


update ProfileComment set ViewCount=ViewCount+1 where Id in (select id from @unSeenProfileComment)

update ProfileNotification 
set  IsSeen=1  
where SourceId in (select id from @unSeenProfileComment)

insert into ProfileCommentViewer([Id],[CommentId],[Date],[ProfileId],[ViewerProfileId])
select newid(),Id,getdate(),CategoryId,@UserId
from @tempTable2
where  Id in (select id from @unSeenProfileComment)

insert into ProfileCommentTransaction
([Id],[CommentId],[CreatedDate],CommentRoot,CommentParent,[Data],
[ProfileId],[CommentTransactionType],[UserId])
select newid(),Id,getdate(),[Root],[Parent],[Content],CategoryId,@viewTransaction,CreatedById
from @tempTable2
where Id in (select Id from @tempTable2 where Root is null and Category=@profileType) -- duplication is allow in order to see How many times a specific comment is seen
-------------------------------------------------------------
declare @unSeenGroupComment table(Id nvarchar(100));
insert into @unSeenGroupComment
select * from
(
select Id from @tempTable2 where Root is null and Category=@groupType --return result
except											
select CommentId from GroupCommentViewer where ViewerProfileId=@UserId--Prev seen result
) unSeenGroupComments


update GroupComment set ViewCount=ViewCount+1 where Id in (select id from @unSeenGroupComment)

update GroupNotification 
set  IsSeen=1  
where SourceId in (select id from @unSeenGroupComment)

insert into GroupCommentViewer([Id],[CommentId],[Date],[GroupId],[ViewerProfileId])
select newid(),Id,getdate(),CategoryId,@UserId
from @tempTable2
where Id in(select id from @unSeenGroupComment)

insert into GroupCommentTransaction
([Id],[CommentId],[CreatedDate],CommentRoot,CommentParent,[Data],
GroupId,[CommentTransactionType],[UserId])
select newid(),Id,getdate(),[Root],[Parent],[Content],CategoryId,@viewTransaction,CreatedById
from @tempTable2
where  Id in(select Id from @tempTable2 where Root is null and Category=@groupType ) -- duplication is allow in order to see How many times a specific comment is seen
-------------------------------------------------------------
COMMIT TRANSACTION 

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END

END CATCH

select [Id]
,[CreatedById]
,[Created]
,[Modified]
,[Parent]
,[Root]
,[Content]
,[UpvoteCount]
,[ViewCount]
,[Category]
,[Type]
,[Classification]
,[CategoryId]
,[CategoryName]
,[CreatorName]
,[ProfilePictureUrl]
,[UserId]
,[UserName]
,[FileId]
,[Path]
,[Extension]
from (
select * from [vwComments]  
where Root in (select Id from @tempTable)
union
select * from @tempTable
) result
END
                                  ");

            migrationBuilder.Sql($@"


CREATE PROCEDURE [dbo].[spViewGroupComments]
(
     @GroupId varchar(100)
	, @UserId varchar(100)
    , @PageSize int=15
	,@PageIndex int=1
)
AS
BEGIN
declare @tempTable as table (Id  nvarchar(450),
[CreatedById]  nvarchar(450),
Created datetime2(7),
Modified datetime2(7),
Parent nvarchar(max),
Root nvarchar(max),
Content nvarchar(max),
UpvoteCount int,
ViewCount int,
Category int,
Type int,
Classification int,
CategoryId nvarchar(450),
CategoryName nvarchar(max),
CreatorName nvarchar(450),
UserId nvarchar(450),
UserName nvarchar(450),
Path nvarchar(max),
Extension nvarchar(max),
FileId nvarchar(450),
ProfilePictureUrl nvarchar(max)
);
declare @tempTable2 as table (Id  nvarchar(450),
[CreatedById]  nvarchar(450),
Created datetime2(7),
Modified datetime2(7),
Parent nvarchar(max),
Root nvarchar(max),
Content nvarchar(max),
UpvoteCount int,
ViewCount int,
Category int,
Type int,
Classification int,
CategoryId nvarchar(450),
CategoryName nvarchar(max),
CreatorName nvarchar(450)
);

insert into @tempTable 
SELECT *
FROM [vwGroupComments]
where Id in (
select Id from 
(select distinct id,Created
from [vwGroupComments] c
where (c.CategoryId=@GroupId and Root IS NULL)) com
order by com.Created desc
OFFSET (@PageIndex-1)*@PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY )

insert into @tempTable2
select distinct  Id ,
[CreatedById] ,
Created ,
Modified,
Parent ,
Root ,
Content ,
UpvoteCount ,
ViewCount ,
Category ,
Type ,
Classification ,
CategoryId ,
CategoryName,
CreatorName
from @tempTable

BEGIN TRANSACTION;
SAVE TRANSACTION MySavePoint;

BEGIN TRY


declare @groupType int=10
declare @viewTransaction int=40

declare @unSeenGroupComment table(Id nvarchar(100));
insert into @unSeenGroupComment
select * from
(
select Id from @tempTable2 where Root is null and Category=@groupType --return result
except											
select CommentId from GroupCommentViewer where ViewerProfileId=@UserId--Prev seen result
) unSeenGroupComments


update GroupComment set ViewCount=ViewCount+1 where Id in (select id from @unSeenGroupComment)

update GroupNotification 
set  IsSeen=1  
where SourceId in (select id from @unSeenGroupComment)

insert into GroupCommentViewer([Id],[CommentId],[Date],[GroupId],[ViewerProfileId])
select newid(),Id,getdate(),CategoryId,@UserId
from @tempTable2
where Id in(select id from @unSeenGroupComment)

insert into GroupCommentTransaction
([Id],[CommentId],[CreatedDate],CommentRoot,CommentParent,[Data],
GroupId,[CommentTransactionType],[UserId])
select newid(),Id,getdate(),[Root],[Parent],[Content],CategoryId,@viewTransaction,CreatedById
from @tempTable2
where  Id in(select Id from @tempTable2 where Root is null and Category=@groupType ) -- duplication is allow in order to see How many times a specific comment is seen

COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH

	select [Id]
,[CreatedById]
,[Created]
,[Modified]
,[Parent]
,[Root]
,[Content]
,[UpvoteCount]
,[ViewCount]
,[Category]
,[Type]
,[Classification]
,[CategoryId]
,[CategoryName]
,[CreatorName]
,[ProfilePictureUrl]
,[UserId]
,[UserName]
,[FileId]
,[Path]
,[Extension]
from (
select * from [vwGroupComments]  where Root in (select Id from @tempTable)
union
select * from @tempTable
) result

END
                                  ");

            migrationBuilder.Sql($@"
CREATE PROCEDURE [dbo].[spViewProfileComments]
(
     @Profile varchar(100)
   , @UserId  varchar(100)
   , @PageSize int=15
	,@PageIndex int=1
)
AS
BEGIN
declare @tempTable as table (Id  nvarchar(450),
[CreatedById]  nvarchar(450),
Created datetime2(7),
Modified datetime2(7),
Parent nvarchar(max),
Root nvarchar(max),
Content nvarchar(max),
UpvoteCount int,
ViewCount int,
Category int,
Type int,
Classification int,
CategoryId nvarchar(450),
CategoryName nvarchar(max),
CreatorName nvarchar(450),
UserId nvarchar(450),
UserName nvarchar(450),
Path nvarchar(max),
Extension nvarchar(max),
FileId nvarchar(450),
ProfilePictureUrl nvarchar(max)
);


declare @tempTable2 as table (Id  nvarchar(450),
[CreatedById]  nvarchar(450),
Created datetime2(7),
Modified datetime2(7),
Parent nvarchar(max),
Root nvarchar(max),
Content nvarchar(max),
UpvoteCount int,
ViewCount int,
Category int,
Type int,
Classification int,
CategoryId nvarchar(450),
CategoryName nvarchar(max),
CreatorName nvarchar(450)
);

insert into @tempTable 
SELECT *
FROM [vwProfileComments]
where Id in (
select Id from 
(select distinct id,Created
from [vwProfileComments] c
where (c.CategoryId=@Profile and Root IS NULL)) com
order by com.Created desc
OFFSET (@PageIndex-1)*@PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY )


insert into @tempTable2
select distinct  Id ,
[CreatedById] ,
Created ,
Modified,
Parent ,
Root ,
Content ,
UpvoteCount ,
ViewCount ,
Category ,
Type ,
Classification ,
CategoryId ,
CategoryName,
CreatorName
from @tempTable

BEGIN TRANSACTION;
SAVE TRANSACTION MySavePoint;

BEGIN TRY
declare @profileType int=0
declare @viewTransaction int=40


-------------------------------------------------------------
declare @unSeenProfileComment table(Id nvarchar(100));
insert into @unSeenProfileComment
select * from(select Id from @tempTable2 where Root is null and Category=@profileType --return result
except											
select CommentId from ProfileCommentViewer where ViewerProfileId=@UserId--Prev seen result
) unSeenProfileComments


update ProfileComment set ViewCount=ViewCount+1 where Id in (select id from @unSeenProfileComment)

update ProfileNotification 
set  IsSeen=1  
where SourceId in (select id from @unSeenProfileComment)

insert into ProfileCommentViewer([Id],[CommentId],[Date],[ProfileId],[ViewerProfileId])
select newid(),Id,getdate(),CategoryId,@UserId
from @tempTable2
where  Id in (select id from @unSeenProfileComment)

insert into ProfileCommentTransaction
([Id],[CommentId],[CreatedDate],CommentRoot,CommentParent,[Data],
[ProfileId],[CommentTransactionType],[UserId])
select newid(),Id,getdate(),[Root],[Parent],[Content],CategoryId,@viewTransaction,CreatedById
from @tempTable2
where Id in (select Id from @tempTable2 where Root is null and Category=@profileType) -- duplication is allow in order to see How many times a specific comment is seen
-------------------------------------------------------------



COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH

	select [Id]
,[CreatedById]
,[Created]
,[Modified]
,[Parent]
,[Root]
,[Content]
,[UpvoteCount]
,[ViewCount]
,[Category]
,[Type]
,[Classification]
,[CategoryId]
,[CategoryName]
,[CreatorName]
,[ProfilePictureUrl]
,[UserId]
,[UserName]
,[FileId]
,[Path]
,[Extension]
from (
select * from [vwProfileComments]  where Root in (select Id from @tempTable)
union
select * from @tempTable
) result
END

                                  ");


            migrationBuilder.Sql($@"
CREATE PROCEDURE [dbo].[spViewSpesificCommentIncludeReplies]
(
     @UserId varchar(100)
	,@CommentId varchar(100)
)
AS
BEGIN

declare @tempTable as table (Id  nvarchar(450),
[CreatedById]  nvarchar(450),
Created datetime2(7),
Modified datetime2(7),
Parent nvarchar(max),
Root nvarchar(max),
Content nvarchar(max),
UpvoteCount int,
ViewCount int,
Category int,
Type int,
Classification int,
CategoryId nvarchar(450),
CategoryName nvarchar(max),
CreatorName nvarchar(450),
UserId nvarchar(450),
UserName nvarchar(450),
Path nvarchar(max),
Extension nvarchar(max),
FileId nvarchar(450),
ProfilePictureUrl nvarchar(max)
);


declare @tempTable2 as table (Id  nvarchar(450),
[CreatedById]  nvarchar(450),
Created datetime2(7),
Modified datetime2(7),
Parent nvarchar(max),
Root nvarchar(max),
Content nvarchar(max),
UpvoteCount int,
ViewCount int,
Category int,
Type int,
Classification int,
CategoryId nvarchar(450),
CategoryName nvarchar(max),
CreatorName nvarchar(450)
);
insert into @tempTable 
SELECT *
FROM [vwComments]
where Id=@CommentId 
and CategoryId in 
		(select GroupId from [GroupMember] where MemberId=@UserId --user groups
			union
			select Id from [Group] where GroupTypeId=1 -- others public groups
			union select @UserId -- user profile
		)
		
insert into @tempTable2
select distinct  Id ,
[CreatedById] ,
Created ,
Modified,
Parent ,
Root ,
Content ,
UpvoteCount ,
ViewCount ,
Category ,
Type ,
Classification ,
CategoryId ,
CategoryName,
CreatorName
from @tempTable
BEGIN TRANSACTION;
SAVE TRANSACTION MySavePoint;

BEGIN TRY

declare @profileType int=0
declare @groupType int=10
declare @viewTransaction int=40


-------------------------------------------------------------
declare @unSeenProfileComment table(Id nvarchar(100));
insert into @unSeenProfileComment
select * from(select Id from @tempTable2 where Root is null and Category=@profileType --return result
except											
select CommentId from ProfileCommentViewer where ViewerProfileId=@UserId--Prev seen result
) unSeenProfileComments


update ProfileComment set ViewCount=ViewCount+1 where Id in (select id from @unSeenProfileComment)

update ProfileNotification 
set  IsSeen=1  
where SourceId in (select id from @unSeenProfileComment)

insert into ProfileCommentViewer([Id],[CommentId],[Date],[ProfileId],[ViewerProfileId])
select newid(),Id,getdate(),CategoryId,@UserId
from @tempTable2
where  Id in (select id from @unSeenProfileComment)

insert into ProfileCommentTransaction
([Id],[CommentId],[CreatedDate],CommentRoot,CommentParent,[Data],
[ProfileId],[CommentTransactionType],[UserId])
select newid(),Id,getdate(),[Root],[Parent],[Content],CategoryId,@viewTransaction,CreatedById
from @tempTable2
where Id in (select Id from @tempTable2 where Root is null and Category=@profileType) -- duplication is allow in order to see How many times a specific comment is seen
-------------------------------------------------------------
declare @unSeenGroupComment table(Id nvarchar(100));
insert into @unSeenGroupComment
select * from
(
select Id from @tempTable2 where Root is null and Category=@groupType --return result
except											
select CommentId from GroupCommentViewer where ViewerProfileId=@UserId--Prev seen result
) unSeenGroupComments


update GroupComment set ViewCount=ViewCount+1 where Id in (select id from @unSeenGroupComment)

update GroupNotification 
set  IsSeen=1  
where SourceId in (select id from @unSeenGroupComment)

insert into GroupCommentViewer([Id],[CommentId],[Date],[GroupId],[ViewerProfileId])
select newid(),Id,getdate(),CategoryId,@UserId
from @tempTable2
where Id in(select id from @unSeenGroupComment)

insert into GroupCommentTransaction
([Id],[CommentId],[CreatedDate],CommentRoot,CommentParent,[Data],
GroupId,[CommentTransactionType],[UserId])
select newid(),Id,getdate(),[Root],[Parent],[Content],CategoryId,@viewTransaction,CreatedById
from @tempTable2
where  Id in(select Id from @tempTable2 where Root is null and Category=@groupType ) -- duplication is allow in order to see How many times a specific comment is seen
-------------------------------------------------------------


COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
        END
    END CATCH

	select [Id]
,[CreatedById]
,[Created]
,[Modified]
,[Parent]
,[Root]
,[Content]
,[UpvoteCount]
,[ViewCount]
,[Category]
,[Type]
,[Classification]
,[CategoryId]
,[CategoryName]
,[CreatorName]
,[ProfilePictureUrl]
,[UserId]
,[UserName]
,[FileId]
,[Path]
,[Extension]
from (
select * from [vwComments]  
where Root in (select Id from @tempTable)
union
select * from @tempTable
) result
END

                                  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
