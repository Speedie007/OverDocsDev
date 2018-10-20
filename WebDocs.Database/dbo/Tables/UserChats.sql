CREATE TABLE [dbo].[UserChats] (
    [UserChatID] INT           IDENTITY (1, 1) NOT NULL,
    [ChatID]     INT           NOT NULL,
    [UserID]     INT           NOT NULL,
    [Message]    VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_UserChats] PRIMARY KEY CLUSTERED ([UserChatID] ASC),
    CONSTRAINT [FK_UserChats_AspNetUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserChats_Chats] FOREIGN KEY ([ChatID]) REFERENCES [dbo].[Chats] ([ChatID])
);



