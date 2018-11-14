using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.Common.WindowsServices
{
    public class ServiceLogFiles
    {
        public static void WrtieToLog(string Message)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter("C:\\Logfile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }
    }
}
