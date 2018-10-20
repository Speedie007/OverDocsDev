CREATE TABLE [dbo].[Chats] (
    [ChatID]      INT          IDENTITY (1, 1) NOT NULL,
    [ChatHeading] VARCHAR (50) CONSTRAINT [DF_Chats_ChatHeading] DEFAULT ('General') NOT NULL,
    CONSTRAINT [PK_Chats] PRIMARY KEY CLUSTERED ([ChatID] ASC)
);



