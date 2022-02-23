using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Mailbox
{
    class ViewModel
    {
        Window win;

        public ViewModel(string type, Window win)
        {
           
            if (type == "encrypt")
            {
                this.SendEmailCommand = new DelegateCommand(OnSendEmail);
            }
            if (type == "decrypt")
            {
                this.SendEmailCommand = new DelegateCommand(OnReciveEmail);
            }
            this.win = win;
        }

        public ICommand SendEmailCommand { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Password { get; set; }

        private void OnSendEmail(object obj)
        {   
            try
            {
                File.WriteAllBytes(@".\message.text", JHI_SINGLTONE.ecrypt(this.Content, ToEmail));
                
                new SendReadMails().SendMailWithXOAUTH2( this.ToEmail, @".\message.text");

              //  Gmail.Send(this.ToEmail,this.FromEmail, Subject, "", Password, @".\message.text");
                MessageBox.Show("ההודעה נשלחה בהצלחה");
                win.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());

            }
            
         
            

        }
        private void OnReciveEmail(object obj)
        {
            try
            {
               var from= new SendReadMails().RetrieveMailWithXOAUTH2((int)obj);
                new DecryptedContent(JHI_SINGLTONE.decrypt(@".\..\..\email\message.text", from)).Show();
                
              //  new DecryptedContent(JHI_SINGLTONE.decrypt(Content, ToEmail)).Show(); 
                 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }

        }
    }
}
