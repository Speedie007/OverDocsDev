CREATE TABLE [dbo].[FileBlobs] (
    [FileID]    INT   NOT NULL,
    [FileImage] IMAGE NOT NULL,
    CONSTRAINT [PK_FileBlobs] PRIMARY KEY CLUSTERED ([FileID] ASC),
    CONSTRAINT [FK_FileBlobs_Files] FOREIGN KEY ([FileID]) REFERENCES [dbo].[Files] ([FileID])
);

