using System;
using System.Collections.Generic;
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
using System.IO;



namespace Mailbox
{
    /// <summary>
    /// Interaction logic for Sing_in.xaml
    /// </summary>
    public partial class Sing_in : Window
    {
        public Sing_in()
        {
            InitializeComponent();
        }



        private void SingInButton_Click(object sender, RoutedEventArgs e)
        {
            var result = Task.Run(async () => await new Sing().DoOauthAndRetrieveEmail()).Result;
            
            if (!result)
            {
                MessageBox.Show("athoriction failed");
                this.Close();
            }





            if (this.passwordBox.Password.CompareTo(this.verifyPasswordBox.Password) != 0)
            {
                MessageBox.Show("verfy password and password not adintical");
                return;
            }
            if (SingIn(this.passwordBox.Password))
            {
                MessageBox.Show("sing in seceeded");
                this.Close();
            }
            else
            {
                MessageBox.Show("sing in failed");
                this.Close();
            }

        }

        

        private bool SingIn(string Password)
        {

            return JHI_SINGLTONE.sendAndRecive(Password, JHI_SINGLTONE.SET_PASSWORD);

        }

        private void passwordBox_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((e.Command == ApplicationCommands.Cut) ||
                 (e.Command == ApplicationCommands.Copy) ||
                  (e.Command == ApplicationCommands.Paste))
            {
                e.Handled = true;
                e.CanExecute = false;

            }
        }
    }
}