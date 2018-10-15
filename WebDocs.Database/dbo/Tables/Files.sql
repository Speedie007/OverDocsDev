CREATE TABLE [dbo].[Files] (
    [FileID]               INT            IDENTITY (1, 1) NOT NULL,
    [ParentFileID]         INT            NOT NULL,
    [UserIDOfFileOwner]    NVARCHAR (128) NOT NULL,
    [UserIDOfLastUploaded] NVARCHAR (128) NOT NULL,
    [FileLookupStatusID]   INT            NOT NULL,
    [FileShareStatusID]    INT            NOT NULL,
    [ContentType]          VARCHAR (500)  NOT NULL,
    [FileName]             VARCHAR (500)  NOT NULL,
    [FileExtension]        VARCHAR (50)   NOT NULL,
    [FileImage]            IMAGE          NOT NULL,
    [FileSize]             INT            NOT NULL,
    [CurrentVersionNumber] INT            NOT NULL,
    [DateCreated]          DATETIME       NOT NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED ([FileID] ASC),
    CONSTRAINT [FK_Files_AspNetUsers] FOREIGN KEY ([UserIDOfFileOwner]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Files_LookupTable_FileStatuses] FOREIGN KEY ([FileLookupStatusID]) REFERENCES [dbo].[LookupTable_FileStatuses] ([FileLookupStatusID]),
    CONSTRAINT [FK_Files_LookupTable_ShareStatues] FOREIGN KEY ([FileShareStatusID]) REFERENCES [dbo].[LookupTable_ShareStatues] ([FileShareStatusID])
);

