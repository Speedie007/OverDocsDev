namespace WebDocs.Common.Enum.DBLookupEnum
{
    public enum FileViewStatus
    {
        FileIsLocked = 1,
        FileIsAvailable = 2
    }

    public enum FileSharedStatus
    {

        Public = 1,
        Private = 2,
        Hidden = 6

    }

    public enum NotificationTypes
    {
        FileRequest = 1,
        FileUpload = 2,
        Download = 3,
        Revoked = 4,
        Unshare = 5
    }
}