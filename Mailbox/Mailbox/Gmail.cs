using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ActiveUp.Net.Mail;
using System.Net.Mime;

namespace Mailbox
{
    class Gmail
    {
        public static void Send(string toEmail, string fromEmail, string subject, string Content, string password ,string file)
        {
            MailAddress to = new MailAddress(toEmail);
            MailAddress from = new MailAddress(fromEmail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = Content;
            System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(file, MediaTypeNames.Application.Octet);
            message.Attachments.Add(data);
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient()
            {   
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, password)
            };
            client.Send(message);
           
        }
        // private Imap4Client client;

        //public void ReadMail(string mailServer, int port, bool ssl, string login, string password)
        //{
        //    if (ssl)
        //        Client.ConnectSsl(mailServer, port);
        //    else
        //        Client.Connect(mailServer, port);
        //    Client.Login(login, password);
        //}

        //public IEnumerable<Message> GetAllMails(string mailBox)
        //{
        //    return GetMails(mailBox, "ALL").Cast<Message>();
        //}

        //public IEnumerable<Message> GetUnreadMails(string mailBox)
        //{
        //    return GetMails(mailBox, "UNSEEN").Cast<Message>();
        //}

        //protected Imap4Client Client
        //{
        //    get { return client ?? (client = new Imap4Client()); }
        //}

        //private MessageCollection GetMails(string mailBox, string searchPhrase)
        //{
           
        //    Mailbox mails = Client.SelectMailbox(mailBox);
        //    MessageCollection messages = mails.SearchParse(searchPhrase);
        //    return messages;
        //}













    }
}
