CREATE TABLE [dbo].[Notifications] (
    [NotificationID]                INT      IDENTITY (1, 1) NOT NULL,
    [FileID]                        INT      NOT NULL,
    [NotificationTypeID]            INT      NOT NULL,
    [DateCreated]                   DATETIME NOT NULL,
    [UserIDOfNotificationSender]    INT      NOT NULL,
    [UserIDOfNotificationRecipient] INT      NOT NULL,
    [UserHasAcknowledgement]        BIT      CONSTRAINT [DF_Notifications_UserHasAcknowledgement_1] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED ([NotificationID] ASC),
    CONSTRAINT [FK_Notifications_AspNetUsers] FOREIGN KEY ([UserIDOfNotificationSender]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Notifications_AspNetUsers1] FOREIGN KEY ([UserIDOfNotificationRecipient]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Notifications_Files] FOREIGN KEY ([FileID]) REFERENCES [dbo].[Files] ([FileID]),
    CONSTRAINT [FK_Notifications_LookupTableNotificationTypes] FOREIGN KEY ([NotificationTypeID]) REFERENCES [dbo].[LookupTable_NotificationTypes] ([NotificationTypeID])
);







