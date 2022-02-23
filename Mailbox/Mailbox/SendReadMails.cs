using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EASendMail;
using EAGetMail;
using System.Globalization;
using System.IO;
using System.Net;


namespace Mailbox
{
    class SendReadMails
    {
        const string clientID = "your client id";
        const string clientSecret = "your client secret";
        const string scope = "openid%20profile%20email%20https://mail.google.com";
        const string authUri = "https://accounts.google.com/o/oauth2/v2/auth";
        const string tokenUri = "https://www.googleapis.com/oauth2/v4/token";
        public void SendMailWithXOAUTH2(string ToEmail, string file)
        {
            var userEmail = File.ReadAllText(@"userEmail");
            var accessToken = File.ReadAllText(@"acssesToken");
            SendMailWithXOAUTH2(userEmail, accessToken, ToEmail, file);
        }
        public string[] RetrieveMailListWithXOAUTH2()
        {
            var userEmail = File.ReadAllText(@"userEmail");
            var accessToken = File.ReadAllText(@"acssesToken");
            return RetrieveMailListWithXOAUTH2(userEmail, accessToken);
        }
        public string RetrieveMailWithXOAUTH2(int index)
        {
            var userEmail = File.ReadAllText(@"userEmail");
            var accessToken = File.ReadAllText(@"acssesToken");
            return RetrieveMailWithXOAUTH2(userEmail, accessToken,index);
        }
        private void SendMailWithXOAUTH2(string userEmail, string accessToken, string ToEmail, string file)
        {
            try
            {
                // Gmail API server address
                SmtpServer oServer = new SmtpServer("https://www.googleapis.com/upload/gmail/v1/users/me/messages/send?uploadType=media");

                // set Gmail RESTFul API protocol 
                oServer.Protocol = EASendMail.ServerProtocol.GmailApi;

                // enable SSL connection
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                // use Gmail OAUTH 2.0 authentication
                oServer.AuthType = SmtpAuthType.XOAUTH2;
                // set user authentication
                oServer.User = userEmail;
                // use access token as password
                oServer.Password = accessToken;

                SmtpMail oMail = new SmtpMail("TryIt");
                // Your gmail email address
                oMail.From = userEmail;
                oMail.To = ToEmail;


                oMail.AddAttachment(file);


                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);


            }
            catch (Exception ep)
            {
                Console.WriteLine("Exception: {0}", ep.Message);
            }
        }
        private string[] RetrieveMailListWithXOAUTH2(string user, string accessToken)
        {

            try
            {
                // Create a folder named "inbox" under current directory
                // to save the email retrieved.
                string localInbox = string.Format("{0}\\inbox", Directory.GetCurrentDirectory());
                // If the folder is not existed, create it.
                if (!Directory.Exists(localInbox))
                {
                    Directory.CreateDirectory(localInbox);
                }

                MailServer oServer = new MailServer("imap.gmail.com",
                        user,
                        accessToken, // use access token as password
                        EAGetMail.ServerProtocol.Imap4);

                // Set IMAP OAUTH 2.0
                oServer.AuthType = ServerAuthType.AuthXOAUTH2;
                // Enable SSL/TLS connection, most modern email server require SSL/TLS by default
                oServer.SSLConnection = true;
                // Set IMAP4 SSL Port
                oServer.Port = 993;

                MailClient oClient = new MailClient("TryIt");

                // Get new email only, if you want to get all emails, please remove this line
                oClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.All;


                oClient.Connect(oServer);
                List<string> mailList = new List<string>();
                foreach (var item in oClient.GetMailInfos())
                {
                    mailList.Add(oClient.GetMail(item).SentDate+"\n"+oClient.GetMail(item).From.Address);
                }

                // Quit and expunge emails marked as deleted from server.
                oClient.Quit();
                return mailList.ToArray();

            }
            catch (Exception ep)
            {
                throw ep;
            }
        }
        private string RetrieveMailWithXOAUTH2(string user, string accessToken, int index)
        {

            try
            {
                // Create a folder named "inbox" under current directory
                // to save the email retrieved.
                string localInbox = string.Format("{0}\\inbox", Directory.GetCurrentDirectory());
                // If the folder is not existed, create it.
                if (!Directory.Exists(localInbox))
                {
                    Directory.CreateDirectory(localInbox);
                }

                MailServer oServer = new MailServer("imap.gmail.com",
                        user,
                        accessToken, // use access token as password
                        EAGetMail.ServerProtocol.Imap4);

                // Set IMAP OAUTH 2.0
                oServer.AuthType = ServerAuthType.AuthXOAUTH2;
                // Enable SSL/TLS connection, most modern email server require SSL/TLS by default
                oServer.SSLConnection = true;
                // Set IMAP4 SSL Port
                oServer.Port = 993;

                MailClient oClient = new MailClient("TryIt");

                // Get new email only, if you want to get all emails, please remove this line
                oClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.All;


                oClient.Connect(oServer);
                MailInfo info = oClient.GetMailInfos()[index];
                // Receive email from email server
                Mail oMail = oClient.GetMail(info);
                
                oMail.Attachments.First().SaveAs(@".\..\..\email\" + oMail.Attachments.First().Name, true);
                var from = oMail.From.Address;
                

                // Mark email as read to prevent retrieving this email again.
                oClient.MarkAsRead(info, true);


                // Quit and expunge emails marked as deleted from server.
                oClient.Quit();
                return from;

            }
            catch (Exception ep)
            {
                throw ep;
            }
        }

    }
}
