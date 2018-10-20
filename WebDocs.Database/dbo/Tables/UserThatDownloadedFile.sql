CREATE TABLE [dbo].[UserThatDownloadedFile] (
    [FileID]                   INT      NOT NULL,
    [UserIDThatDownloadedFIle] INT      NOT NULL,
    [DateDownloaded]           DATETIME CONSTRAINT [DF_UserThatDownloadedFile_DateDownloaded] DEFAULT (getdate()) NOT NULL,
    [HasFileBeenReturned]      BIT      CONSTRAINT [DF_UserThatDownloadedFile_HasFileBeenReturned] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserThatDownloadedFile] PRIMARY KEY CLUSTERED ([FileID] ASC),
    CONSTRAINT [FK_UserThatDownloadedFile_AspNetUsers] FOREIGN KEY ([UserIDThatDownloadedFIle]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserThatDownloadedFile_Files] FOREIGN KEY ([FileID]) REFERENCES [dbo].[Files] ([FileID])
);



