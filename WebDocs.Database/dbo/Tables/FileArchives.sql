CREATE TABLE [dbo].[FileArchives] (
    [FileArchiveID]        INT           IDENTITY (1, 1) NOT NULL,
    [FileID]               INT           NOT NULL,
    [UserIDOfLastUploaded] INT           NOT NULL,
    [FileLookupStatusID]   INT           NOT NULL,
    [FileShareStatusID]    INT           NOT NULL,
    [ContentType]          VARCHAR (500) NOT NULL,
    [FileName]             VARCHAR (500) NOT NULL,
    [FileExtension]        VARCHAR (50)  NOT NULL,
    [FileSize]             INT           NOT NULL,
    [FileImage]            IMAGE         NOT NULL,
    [CurrentVersionNumber] INT           NOT NULL,
    [DateCreated]          DATETIME      NOT NULL,
    [DateArchived]         DATETIME      CONSTRAINT [DF_FileArchives_DateArchived] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_FileArchives] PRIMARY KEY CLUSTERED ([FileArchiveID] ASC),
    CONSTRAINT [FK_FileArchives_Files] FOREIGN KEY ([FileID]) REFERENCES [dbo].[Files] ([FileID])
);

