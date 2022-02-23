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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Mailbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int numTry = 5;
        public MainWindow()
        {
            InitializeComponent();
             JHI_SINGLTONE.inizilze();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LogIn(this.passwordBox.Password))
            {
                this.passwordBox.Password = "";
                this.passwordBox.Visibility = Visibility.Hidden;
                this.passwordButton.Visibility = Visibility.Hidden;
                this.sing_inButton.Visibility = Visibility.Hidden;
                
                this.passwordBox.IsEnabled = false;
                this.passwordButton.IsEnabled = false;
                this.sing_inButton.IsEnabled = false;

                this.LogOut.IsEnabled = true;
                this.changePassword.IsEnabled = true;
                this.AddTo.IsEnabled = true;
                this.encryptButton.IsEnabled = true;
                this.decryptButton.IsEnabled = true;
                this.getPKButton.IsEnabled = true;
                this.getPKButton.Visibility = Visibility.Visible;
                this.AddTo.Visibility = Visibility.Visible;
                this.encryptButton.Visibility = Visibility.Visible;
                this.decryptButton.Visibility = Visibility.Visible;
                this.LogOut.Visibility = Visibility.Visible;
                this.changePassword.Visibility = Visibility.Visible;
              
            }
            else {
                numTry--;
                this.passwordBox.Foreground = Brushes.Red;
                this.passwordButton.Foreground = Brushes.Red;
              

                if (numTry < 0) {
                    
                    this.passwordBox.Visibility = Visibility.Hidden;
                    this.passwordButton.Visibility = Visibility.Hidden;
                    this.sing_inButton.Visibility = Visibility.Hidden;
                   
                }

            }




        }

        private bool LogIn(string passwordGiven)
        {
            return JHI_SINGLTONE.sendAndRecive(passwordGiven, JHI_SINGLTONE.LOGIN);
        }


        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
           new EncrypyEmail().Show() ;
          
        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            new DecryptEmail().Show();
         
        }

        private void sing_inButton_Click(object sender, RoutedEventArgs e)
        {
            new Sing_in().Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           JHI_SINGLTONE.close();
         
           
        }

        private void AddTo_Click(object sender, RoutedEventArgs e)
        {
            new AddAddress().Show();
        }

        private void getPKButton_Click(object sender, RoutedEventArgs e)
        {
            new GetPK().Show();
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            new ChangePassword().Show();
           
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            if (JHI_SINGLTONE.sendAndRecive( JHI_SINGLTONE.LOGOUT))
            { MessageBox.Show("Log out seccfly");

                this.passwordBox.IsEnabled = true;
                this.passwordButton.IsEnabled = true;
                this.sing_inButton.IsEnabled = true;
                this.passwordBox.Visibility = Visibility.Visible;
                this.passwordButton.Visibility = Visibility.Visible;
                this.sing_inButton.Visibility = Visibility.Visible;
                

                this.LogOut.IsEnabled = false;
                this.changePassword.IsEnabled = false;
                this.AddTo.IsEnabled = false;
                this.encryptButton.IsEnabled = false;
                this.decryptButton.IsEnabled = false;
                this.getPKButton.IsEnabled = false;
                this.getPKButton.Visibility = Visibility.Hidden;
                this.AddTo.Visibility = Visibility.Hidden;
                this.encryptButton.Visibility = Visibility.Hidden;
                this.decryptButton.Visibility = Visibility.Hidden;
                this.LogOut.Visibility = Visibility.Hidden;
                this.changePassword.Visibility = Visibility.Hidden;


            }

        }
    }
}
