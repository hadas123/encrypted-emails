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

namespace Mailbox
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }


        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.passwordBox.Password.CompareTo(this.verifyPasswordBox.Password) != 0)
            {
                MessageBox.Show("verfy password and password not adintical");
                return;
            }
            if (Change(this.passwordBox.Password))
            {
                MessageBox.Show("changing seceeded");
                this.Close();
            }
            else
            {
                MessageBox.Show("changing in failed");
                this.Close();
            }
        }

        private bool Change(string password)
        {
           return JHI_SINGLTONE.sendAndRecive(password, JHI_SINGLTONE.RESET_PASSWORD);
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
