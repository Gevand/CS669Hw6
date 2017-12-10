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

namespace CS669GevandBalayanHomework7
{
    /// <summary>
    /// Interaction logic for Output.xaml
    /// </summary>
    public partial class Output : Window
    {
        public Output()
        {
            InitializeComponent(); 
        }
        public void WriteToOuput(string Input)
        {
            txtOutput.Text += Input + Environment.NewLine;
            txtOutput.ScrollToEnd();
        }
    }
}
