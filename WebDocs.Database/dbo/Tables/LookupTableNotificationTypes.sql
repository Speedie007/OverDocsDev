CREATE TABLE [dbo].[LookupTableNotificationTypes] (
    [NotificationTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [NotificationType]   VARCHAR (50) CONSTRAINT [DF_LookupTableNotificationTypes_NotificationType] DEFAULT ('Unknown') NOT NULL,
    CONSTRAINT [PK_LookupTableNotificationTypes] PRIMARY KEY CLUSTERED ([NotificationTypeID] ASC)
);

