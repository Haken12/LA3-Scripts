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

namespace La3ScriptConverter
{
    /// <summary>
    /// Interaction logic for LogManager.xaml
    /// </summary>
    public partial class LogManager : Window
    {
        public LogManager()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtLogging.Text = null;
        }
    }
}
