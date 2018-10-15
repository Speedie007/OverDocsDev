namespace WebDocs.Common.Files
{
    public static class FileExtensionHelper
    {
        public static string GetFileType(string sFileExtinsion)
        {
            string rtnValue = "UnKnown Ext";
            switch (sFileExtinsion)
            {
                case "doc":
                    rtnValue = "Word Document";
                    break;
                case "xls":
                    rtnValue = "Excel Document";
                    break;
                case "xlsx":
                    rtnValue = "Excel Document";
                    break;
                case "docx":
                    rtnValue = "Word Document";
                    break;
                case "pdf":
                    rtnValue = "PDF Document";
                    break;

            }

            return rtnValue;
        }
    }
}