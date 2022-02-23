using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mailbox
{
    /// <summary>
    /// Interaction logic for GetPK.xaml
    /// </summary>
    public partial class GetPK : Window
    {
        public GetPK()
        {
            InitializeComponent();
            string PK = JHI_SINGLTONE.sendAndRcive(JHI_SINGLTONE.GET_PUBLIC_KEY);
            this.PublicKeyBlock.Text = PK;
            
        }

        private void SavePKButton_Click(object sender, RoutedEventArgs e)
        {
            
            File.WriteAllBytes(@".\..\..\PK\publicKey.text", JHI_SINGLTONE.sendIntAndReciveBytes(JHI_SINGLTONE.GET_PUBLIC_KEY));
            new SendReadMails().SendMailWithXOAUTH2(this.To.Text, @".\..\..\PK\publicKey.text");
            this.Close();
        }
    }
}
