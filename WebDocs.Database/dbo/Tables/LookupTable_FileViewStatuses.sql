CREATE TABLE [dbo].[LookupTable_FileViewStatuses] (
    [FileLookupStatusID] INT           IDENTITY (1, 1) NOT NULL,
    [FileStatus]         VARCHAR (100) NULL,
    CONSTRAINT [PK_LookupTable_FileStatuses] PRIMARY KEY CLUSTERED ([FileLookupStatusID] ASC)
);

