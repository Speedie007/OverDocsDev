CREATE TABLE [dbo].[LookupTable_ShareStatues] (
    [FileShareStatusID] INT          IDENTITY (1, 1) NOT NULL,
    [FileShareStatus]   VARCHAR (50) NULL,
    CONSTRAINT [PK_LookupTable_ShareStatues] PRIMARY KEY CLUSTERED ([FileShareStatusID] ASC)
);

