CREATE TABLE [dbo].[EmailSettings] (
    [ID]                 INT           IDENTITY (1, 1) NOT NULL,
    [UserName]           VARCHAR (250) NOT NULL,
    [Password]           VARCHAR (250) NOT NULL,
    [SmtpHost]           VARCHAR (500) NOT NULL,
    [SmtpPort]           INT           CONSTRAINT [DF_EmailSettings_SmtpPort] DEFAULT ((25)) NOT NULL,
    [SslEnabled]         BIT           CONSTRAINT [DF_EmailSettings_SslEnabled] DEFAULT ((0)) NOT NULL,
    [SystemEmailAddress] VARCHAR (250) CONSTRAINT [DF_EmailSettings_SystemEmailAddress] DEFAULT ('System@OverDocs.com') NOT NULL,
    CONSTRAINT [PK_EmailSettings] PRIMARY KEY CLUSTERED ([ID] ASC)
);



