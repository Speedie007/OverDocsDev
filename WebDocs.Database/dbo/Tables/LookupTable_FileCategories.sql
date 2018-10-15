CREATE TABLE [dbo].[LookupTable_FileCategories] (
    [LookupTableFileCategoryID] INT           IDENTITY (1, 1) NOT NULL,
    [FileCategory]              VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_LookupTable_FileCategories] PRIMARY KEY CLUSTERED ([LookupTableFileCategoryID] ASC)
);

