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

namespace Nagelmackers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DestinationTime.SelectedDate = DateTime.Now;
        }
        /// <summary>
        /// Swaps The values of DestinationFrom and DestinationTo text fields
        /// </summary>
        /// <param name="DestinationFrom.Text"></param>
        /// <param name="DestinationTo.Text"></param>
        private void DestinationSwitcher_Click(object sender, RoutedEventArgs e)
        {
            String Buffor;
            Buffor = DestinationFrom.Text;
            DestinationFrom.Text = DestinationTo.Text;
            DestinationTo.Text = Buffor;
        }
        /// <summary>
        /// Shows the message box with the information about the searched route (temporary)
        /// </summary>
        private void DestinationSearch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wyszukujesz trasy z " + DestinationFrom.Text + " do " + DestinationTo.Text + " w dniu " + DestinationTime.ToString());
        }
    }
}
