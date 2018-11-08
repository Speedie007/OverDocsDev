using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Interfaces.Responses;

namespace WebDocs.DomainModels.TransactionResponse
{
    public class FileUploadResponses : ITransactionResponses, IDisposable
    {
        public string FileName { get; set; }
        public string Message { get; set; }
        public bool WasSuccessfull { get; set; }

        // Flag: Has Dispose already been called?
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
    }
}
