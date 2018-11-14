using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;

namespace WebDocs.Common.Helper.Email
{
    /// <summary>
    /// 
    /// </summary>
    public static class EmailHelper
    {

        public static async Task addEmailToCache(EmailCacheModel ECM)
        {
            using(WebDocs.DataAccessLayer.WebDocsEntities db = new DataAccessLayer.WebDocsEntities())
            {
                ECM.EntityState = DomainModels.EntityState.Added;
                db.EmailCacheModels.Add(ECM);
                await db.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ToAddress"></param>
        /// <param name="_FromAddress"></param>
        /// <param name="_FromName"></param>
        /// <param name="_ToName"></param>
        /// <param name="_Subject"></param>
        /// <param name="_Message"></param>
        /// <param name="_Credentials_UserName"></param>
        /// <param name="_Credentials_Password"></param>
        /// <param name="_SMTP_HOST"></param>
        /// <param name="_IsSsl"></param>
        /// <param name="_SMTP_PORT"></param>
        /// <remarks>
        /// 
        /// </remarks>
        public static async System.Threading.Tasks.Task sendMessageAsync(
            string _ToAddress,
            string _FromAddress,
            string _FromName,
            string _ToName,
            string _Subject,
            string _Message,
            string _Credentials_UserName,
            string _Credentials_Password,
            string _SMTP_HOST,
            Boolean _IsSsl,
            int _SMTP_PORT)
        {
            string body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(new MailAddress(_ToAddress));  // replace with valid value 
            message.From = new MailAddress(_FromAddress);  // replace with valid value
            message.Subject = _Subject;
            message.Body = string.Format(body, _FromName, _FromAddress, _Message);
            message.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    NetworkCredential credential = new NetworkCredential
                    {

                        UserName = _Credentials_UserName,  // replace with valid value
                        Password = _Credentials_Password   // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = _SMTP_HOST; //smtp.mweb.co.za
                    smtp.Port = Convert.ToInt16(_SMTP_PORT); // default port numnber is 25
                    smtp.EnableSsl = Convert.ToBoolean(_IsSsl);
                   await smtp.SendMailAsync(message);
                }
                catch (SmtpFailedRecipientException ex)
                {
                    SmtpStatusCode statusCode = ex.StatusCode;

                    if (statusCode == SmtpStatusCode.MailboxBusy ||
                    statusCode == SmtpStatusCode.MailboxUnavailable ||
                    statusCode == SmtpStatusCode.TransactionFailed)
                    {
                       await  smtp.SendMailAsync(message);
                    }
                    else { throw; }
                }
                finally { message.Dispose(); }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ToAddress"></param>
        /// <param name="_FromAddress"></param>
        /// <param name="_FromName"></param>
        /// <param name="_ToName"></param>
        /// <param name="_Subject"></param>
        /// <param name="_Message"></param>
        /// <param name="_Credentials_UserName"></param>
        /// <param name="_Credentials_Password"></param>
        /// <param name="_SMTP_HOST"></param>
        /// <param name="_IsSsl"></param>
        /// <param name="_SMTP_PORT"></param>
        public static void  sendMessage(
           string _ToAddress,
           string _FromAddress,
           string _FromName,
           string _ToName,
           string _Subject,
           string _Message,
           string _Credentials_UserName,
           string _Credentials_Password,
           string _SMTP_HOST,
           Boolean _IsSsl,
           int _SMTP_PORT)
        {
            string body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(new MailAddress(_ToAddress));  // replace with valid value 
            message.From = new MailAddress(_FromAddress);  // replace with valid value
            message.Subject = _Subject;
            message.Body = string.Format(body, _FromName, _FromAddress, _Message);
            message.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    NetworkCredential credential = new NetworkCredential
                    {

                        UserName = _Credentials_UserName,  // replace with valid value
                        Password = _Credentials_Password   // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = _SMTP_HOST; //smtp.mweb.co.za
                    smtp.Port = Convert.ToInt16(_SMTP_PORT); // default port numnber is 25
                    smtp.EnableSsl = Convert.ToBoolean(_IsSsl);
                    smtp.Send(message);
                }
                catch (SmtpFailedRecipientException ex)
                {
                    SmtpStatusCode statusCode = ex.StatusCode;

                    if (statusCode == SmtpStatusCode.MailboxBusy ||
                    statusCode == SmtpStatusCode.MailboxUnavailable ||
                    statusCode == SmtpStatusCode.TransactionFailed)
                    {
                        smtp.Send(message);
                    }
                    else { throw; }
                }
                finally { message.Dispose(); }
            }
        }
    }
}