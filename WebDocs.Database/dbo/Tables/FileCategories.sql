CREATE TABLE [dbo].[FileCategories] (
    [FileCategoryID]            INT IDENTITY (1, 1) NOT NULL,
    [FileID]                    INT NOT NULL,
    [LookupTableFileCategoryID] INT NOT NULL,
    CONSTRAINT [PK_FileCategories] PRIMARY KEY CLUSTERED ([FileCategoryID] ASC),
    CONSTRAINT [FK_FileCategories_Files] FOREIGN KEY ([FileID]) REFERENCES [dbo].[Files] ([FileID]),
    CONSTRAINT [FK_FileCategories_LookupTable_FileCategories] FOREIGN KEY ([LookupTableFileCategoryID]) REFERENCES [dbo].[LookupTable_FileCategories] ([LookupTableFileCategoryID])
);

