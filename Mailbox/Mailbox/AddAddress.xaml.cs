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
    /// Interaction logic for AddAddress.xaml
    /// </summary>
    public partial class AddAddress : Window
    {
        public AddAddress()
        {
            InitializeComponent();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           
           File.Copy(this.KeyLocationBox.Text, 
                    @".\..\..\address\" + this.UserNameBox.Text + ".text",
                    true);
          
            this.UserNameBox.Text = "Email";
            this.KeyLocationBox.Text = "Key Loction";
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
