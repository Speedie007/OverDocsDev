using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;

namespace WebDocs.EmailService
{
    public partial class EmailSending : ServiceBase
    {
        /*See Articale for implementation
        https://www.c-sharpcorner.com/UploadFile/naresh.avari/develop-and-install-a-windows-service-in-C-Sharp/
        */

        private Timer EmailTimer = null;

        private readonly IEmailCacheRepository _EmailCacheRepsoitory = new EmailCacheRepository();

        private EmailSetting ES;
        private List<EmailCacheModel> EmailToSend = new List<EmailCacheModel>();

        public EmailSending()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EmailTimer = new Timer();
            this.EmailTimer.Interval = 2000; //every 60 seconds
            this.EmailTimer.Elapsed += new System.Timers.ElapsedEventHandler(EmailTimer_Tick);
            this.EmailTimer.Enabled = true;
            ///Common.WindowsServices.ServiceLogFiles.WrtieToLog("WebDocs Service Strarted");

        }

        private async void EmailTimer_Tick(object sender, ElapsedEventArgs e)
        {
            //Stop timer untill the function completes.
            EmailTimer.Enabled = false;
            //Common.WindowsServices.ServiceLogFiles.WrtieToLog("Timer tick event entered and stoped - 1");
            //get relivant emails and email settings.

            
            try
            {
                using (WebDocs.DataAccessLayer.WebDocsEntities db = new DataAccessLayer.WebDocsEntities())
                {
                    ES = db.EmailSettings.FirstOrDefault<EmailSetting>();

                    EmailToSend = await db.EmailCacheModels.Where(a => a.HasBeenSent == false)
                        .Include(a => a.EmailSender)
                        .Include(a => a.EmailRecipient)
                        .ToListAsync<EmailCacheModel>();

                }
                // Common.WindowsServices.ServiceLogFiles.WrtieToLog("Email Settings and List of unsent emails are populated - 2");
            }
            catch
            {
                // Common.WindowsServices.ServiceLogFiles.WrtieToLog("Failed at - 2 Error Message: " + ex.Message + " Inner Message : " + ex.InnerException.Message);
            }

           
            //if any emails are avaiable send emails.
            //Common.WindowsServices.ServiceLogFiles.WrtieToLog("Sending unsent emails START - 3");
            foreach (EmailCacheModel EC in EmailToSend)
            {
                SendEmail(EC, ES);
                //Common.WindowsServices.ServiceLogFiles.WrtieToLog("Email sent To: " + EC.EmailRecipient.UserFullName + " - " + EC.EmailRecipient.Email +"  - 3");
            }
           
            //restart Timer
            //Common.WindowsServices.ServiceLogFiles.WrtieToLog("Email Timer Resstarted - 4");
            this.EmailTimer.Enabled = true;

        }

        private void SendEmail(EmailCacheModel EC, EmailSetting ES)
        {
            Boolean SentSuccsessfully;
            try
            {
                Common.Helper.Email.EmailHelper.sendMessage(
                    _ToAddress: EC.EmailRecipient.Email,
                    _FromAddress: EC.EmailSender.Email,
                    _ToName: EC.EmailRecipient.UserFullName,
                    _FromName: EC.EmailSender.UserFullName,
                    _Subject: EC.EmailSubject,
                    _Message: EC.EmailMessage,
                    _SMTP_HOST: ES.SmtpHost,
                    _SMTP_PORT: ES.SmtpPort,
                    _IsSsl: ES.SslEnabled,
                    _Credentials_Password: ES.Password,
                    _Credentials_UserName: ES.UserName
                 );
                SentSuccsessfully = true;
                //Common.WindowsServices.ServiceLogFiles.WrtieToLog("11111111111111111111Successfully Sent Email for: " + EC.EmailRecipient.UserFullName + " From: " + EC.EmailSender.UserFullName + " - email Cahce Item: " + EC.EmailCacheID);
            }
            catch
            {
                SentSuccsessfully = false;
                // Common.WindowsServices.ServiceLogFiles.WrtieToLog("11111111111111111111111111111Failed Sent Email for: " + EC.EmailRecipient.UserFullName + " From: " + EC.EmailSender.UserFullName + " - email Cahce Item: " + EC.EmailCacheID);
            }
            using (WebDocs.DataAccessLayer.WebDocsEntities db = new DataAccessLayer.WebDocsEntities())
            {
                if (SentSuccsessfully)
                {
                    EC.HasBeenSent = true;
                    db.EmailCacheModels.Attach(EC);
                    db.Entry(EC).State = EntityState.Modified;
                    db.SaveChanges();
                    //Common.WindowsServices.ServiceLogFiles.WrtieToLog("Successfully Updated Email for: " + EC.EmailRecipient.UserFullName + " From: " + EC.EmailSender.UserFullName + " - email Cahce Item: " + EC.EmailCacheID);
                }
                else
                {
                    if (EC.RetryAttempt < 5)
                    {
                        EC.RetryAttempt = EC.RetryAttempt + 1;
                        db.EmailCacheModels.Attach(EC);
                        db.Entry(EC).State = EntityState.Modified;
                        db.SaveChanges();
                        //Common.WindowsServices.ServiceLogFiles.WrtieToLog("222222222222222222222222Failed to Update Email for: " + EC.EmailRecipient.UserFullName + " From: " + EC.EmailSender.UserFullName + " - email Cahce Item: " + EC.EmailCacheID);
                    }
                }
            }

        }

        protected override void OnStop()
        {
            // Common.WindowsServices.ServiceLogFiles.WrtieToLog("Web Docs Service Stopped - 5");
            EmailTimer.Enabled = false;

        }
    }
}
