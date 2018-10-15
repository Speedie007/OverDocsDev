using System.Linq;
using System.Net.Mail;

namespace NewSecurityDemo.Models.Email
{
    public class MailDetailModel
    {
        /// <summary>
        /// 
        /// </summary>
        public MailDetailModel()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ToAddress"></param>
        /// <param name="_FromAddress"></param>
        /// <param name="_FromName"></param>
        /// <param name="_ToName"></param>
        /// <param name="_Credentials_UserName"></param>
        /// <param name="_Credentials_Password"></param>
        /// <param name="_SMTP_HOST"></param>
        /// <param name="_SMTP_PORT"></param>
        public MailDetailModel(
            string _ToAddress,
            string _FromAddress,
            string _FromName,
            string _ToName,
            string _Credentials_UserName = "",
            string _Credentials_Password = "",
            string _SMTP_HOST = "smtp.mweb.co.za",
            int _SMTP_PORT = 25)
        {
            this.toAddress = new MailAddress(_ToAddress);
            this.fromAddress = new MailAddress(_FromAddress);
            this.fromName = _FromName;
            this.toName = _ToName;

        }
        public string fromName { get; set; }
        public string toName { get; set; }
        public MailAddress toAddress { get; set; }
        public MailAddress fromAddress { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

        public string Credentials_UserName { get; set; }
        public string Credentials_Password { get; set; }
        public string SMTP_HOST { get; set; }
        public string SMTP_PORT { get; set; }
    }
}