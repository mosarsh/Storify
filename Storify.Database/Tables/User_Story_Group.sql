CREATE TABLE [dbo].[User_Story_Group]
(
	[UserStoryGroupId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [StoryId] INT NULL, 
    [GroupId] INT NOT NULL, 
    CONSTRAINT [FK_User_Story_Group_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_User_Story_Group_Story] FOREIGN KEY ([StoryId]) REFERENCES [Story]([StoryId]), 
    CONSTRAINT [FK_User_Story_Group_Group] FOREIGN KEY ([GroupId]) REFERENCES [Group]([GroupId]), 
    CONSTRAINT [AK_User_Story] UNIQUE ([UserId],[StoryId])
)
