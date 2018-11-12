CREATE TABLE [dbo].[FileArchiveBlobs] (
    [FileArchiveID] INT   NOT NULL,
    [FileImage]     IMAGE NOT NULL,
    CONSTRAINT [PK_FileArchiveBlobs] PRIMARY KEY CLUSTERED ([FileArchiveID] ASC),
    CONSTRAINT [FK_FileArchiveBlobs_FileArchives] FOREIGN KEY ([FileArchiveID]) REFERENCES [dbo].[FileArchives] ([FileArchiveID])
);





