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
            DestinationDate.SelectedDate = DateTime.Now;
            DestinationTime.SelectedIndex = 0;
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
            StationList kek = new StationList();
            kek.ListOfStations_Reader();
            //MessageBox.Show("Wyszukujesz trasy z " + DestinationFrom.Text + " do " + DestinationTo.Text + " w dniu " + DestinationTime.ToString());
        }
        /// <summary>
        /// Swaps the visibility of DestinationDate and DestinationTime based on which is curenty visible
        /// </summary>
        private void DateTimeSwicher_Click(object sender, RoutedEventArgs e)
        {
            if (DestinationTime.Visibility == Visibility.Visible)
            {
                DestinationTime.Visibility = Visibility.Hidden;
                DestinationDate.Visibility = Visibility.Visible;
                DateTimeSwicher.Content = "🕓";
            }
            else
            {
                DestinationDate.Visibility = Visibility.Hidden;
                DestinationTime.Visibility = Visibility.Visible;
                DateTimeSwicher.Content = "📆";
            }
        }
        /// <summary>
        /// Fills the DestinationTime combobox with the values from 0:00 to 23:00
        /// </summary>
        /// <param name="Time"></param>
        public static void DestinationTimeFiller(ComboBox Time)
        {
            for (int i = 0; i < 24; i++)
            {
                Time.Items.Add(i.ToString() + ":00");
            }
        }
    }
}
//▼
//#FF013765 pkp