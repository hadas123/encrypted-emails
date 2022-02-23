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
    /// Interaction logic for EncrypyEmail.xaml
    /// </summary>
    public partial class EncrypyEmail : Window
    {
        ViewModel v;
        public EncrypyEmail()
        {
            InitializeComponent();
            v= new ViewModel("encrypt",this);
            this.DataContext = v;


            var fullFiles= Directory.GetFiles(@".\..\..\address\").ToList();
            var files = new List<string>();
            foreach (var item in fullFiles)
            {
                var file = System.IO.Path.GetFileName(item);
                files.Add(file.Remove(file.Length - 5, 5));
                

            }
           
            this.address.ItemsSource = files;
            this.address.SelectedIndex = 0;
            
        }


        


        private void address_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (this.DataContext as ViewModel).ToEmail = this.address.SelectedItem.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
