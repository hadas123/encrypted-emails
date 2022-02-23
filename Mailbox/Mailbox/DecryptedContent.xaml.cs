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
    /// Interaction logic for DecryptedContent.xaml
    /// </summary>
    public partial class DecryptedContent : Window
    {
        public DecryptedContent(string content)
        {
            InitializeComponent();
            this.output.Text = content;
        }
    }
}
