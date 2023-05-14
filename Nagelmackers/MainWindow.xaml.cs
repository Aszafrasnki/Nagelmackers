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
        private List<Carrier> CarrierList = new List<Carrier>();
        StationList StationsList = new StationList();
        public MainWindow()
        {
            InitializeComponent();
            InicializeSearchBarElements(DestinationDate, DestinationTime, DestinationFrom, DestinationTo, StationsList);
            InicializeCarrierList(CarrierList);
            ResultsWindow.Visibility = Visibility.Hidden;
            SetResultListValues();
        }
        /// <summary>
        /// Swaps The values of DestinationFrom and DestinationTo text fields, Will not do anything if either of the fileds are set to "Z" or "Do" (default values)
        /// </summary>
        /// <param name="DestinationFrom.Text"></param>
        /// <param name="DestinationTo.Text"></param>
        private void DestinationSwitcher_Click(object sender, RoutedEventArgs e)
        {
            String Buffor;
            if (DestinationFrom.Text != "Z" && DestinationTo.Text != "Do")
            {
                Buffor = DestinationFrom.Text;
                DestinationFrom.Text = DestinationTo.Text;
                DestinationTo.Text = Buffor;
            }
        }
        /// <summary>
        /// Shows the message box with the information about the searched route (temporary)
        /// </summary>
        private void DestinationSearch_Click(object sender, RoutedEventArgs e)
        {
            StationsList.ListOfStations_Reader();
            ResultsWindow.Visibility = Visibility.Visible;
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
        /// Removes the first item from DestinationFrom ComboBox once clicked (Expected to remove "Do")
        /// </summary>
        private void DestinationFrom_DropDownOpened(object sender, EventArgs e)
        {
            DestinationFrom.Items.RemoveAt(0);
        }
        /// <summary>
        /// Removes the first item from DestinationTo ComboBox once clicked (Expected to remove "Z")
        /// </summary>
        private void DestinationTo_DropDownOpened(object sender, EventArgs e)
        {
            DestinationTo.Items.RemoveAt(0);
        }
        /// <summary>
        /// If nothing was selected upon closing the DestinationFrom ComboBox adds the Default Value and selects it
        /// </summary>
        private void DestinationFrom_DropDownClosed(object sender, EventArgs e)
        {
            if (DestinationFrom.SelectedItem == null)
            {
                DestinationFrom.Items.Insert(0, "Z");
                DestinationFrom.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// If nothing was selected upon closing the DestinationTo ComboBox adds the Default Value and selects it
        /// </summary>
        private void DestinationTo_DropDownClosed(object sender, EventArgs e)
        {
            if (DestinationTo.SelectedItem == null)
            {
                DestinationTo.Items.Insert(0, "Do");
                DestinationTo.SelectedIndex = 0;
            }
        }
        //Automatic Filler Methods
        /// <summary>
        /// Fills the DestinationTime combobox with the values from 0:00 to 23:00
        /// </summary>
        /// <param name="Time"></param>
        private static void DestinationTimeFiller(ComboBox Time)
        {
            Time.Items.Add("00:00");
            for (int i = 1; i < 24; i++)
            {
                Time.Items.Add(i.ToString() + ":00");
            }
        }
        /// <summary>
        /// Fills out the ComboBox with Station names from the StationList
        /// </summary>
        /// <param name="DestinationCombBox">The combobox to be filled</param>
        /// <param name="List"></param>
        private static void DestinationComboBoxFiller(ComboBox DestinationCombBox, StationList List)
        {
            for (int i = 0; i < List.getSize(); i++)
            {
                DestinationCombBox.Items.Add(List.GetStationNameAtIndex(i));
            }
        }
        //Inicialization Method
        private static void InicializeSearchBarElements(DatePicker Date, ComboBox Time, ComboBox DestinationFrom, ComboBox DestinationTo, StationList List)
        {
            Date.SelectedDate = DateTime.Now;
            DestinationTimeFiller(Time);
            DestinationFrom.Items.Add("Z");
            DestinationComboBoxFiller(DestinationFrom, List);
            DestinationFrom.SelectedIndex = 0;
            DestinationTo.Items.Add("Do");
            DestinationComboBoxFiller(DestinationTo, List);
            DestinationTo.SelectedIndex = 0;
            Time.SelectedIndex = 0;
        }
        private static void InicializeCarrierList(List<Carrier> CarrierList)
        {
            /// <summary>
            /// #FF013765 pkp     | +1H    | 2.25zł | 3.18min | 
            /// #FFCC0000 pr      | +0.5H  | 2.15zł | 6min    |
            /// #FFBFB700 ic      | +6H    | 4.11zł | 1.63min |
            /// #FF14001B kd      | +1.5H  | 2zł    | 5.25min | 
            /// </summary>
            CarrierList.Add(new Carrier(1, 55, 101, "PKP", 60, 2.25f, 3.18));
            CarrierList.Add(new Carrier(204, 0, 0, "PR", 30, 2.15f, 6));
            CarrierList.Add(new Carrier(191, 183, 0, "IC", 360, 4.11f, 1.63));
            CarrierList.Add(new Carrier(20, 0, 27, "KD", 90, 2f, 5.25));
        }
        public void SetResultListValues()
        {
            //DestinationFromName.Content = 
            int i = 0;
            //1
            ResultBarDepartureTimeLabel_1.Content = "10:26";
            ResultBarArrivalTimeLabel_1.Content = "11:42";
            ResultBarCarrierNameLabel_1.Background = new SolidColorBrush(Color.FromRgb(1, 55, 101));
            ResultBarCarrierNameLabel_1.Content = "PKP";
            ResultBarFastestTag_1.Visibility = Visibility.Hidden;
            ResultBarTravelTimeLabel_1.Content = "1H16";
            ResultBarCheapestTag_1.Visibility = Visibility.Hidden;
            ResultBarTicketPriceLabel_1.Content = "26.45zł";
            i++;
            //2
            ResultBarDepartureTimeLabel_2.Content = "10:26";
            ResultBarArrivalTimeLabel_2.Content = "11:42";
            ResultBarCarrierNameLabel_2.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
            ResultBarCarrierNameLabel_2.Content = "PR";
            ResultBarFastestTag_2.Visibility = Visibility.Hidden;
            ResultBarTravelTimeLabel_2.Content = "1H16";
            ResultBarCheapestTag_2.Visibility = Visibility.Hidden;
            ResultBarTicketPriceLabel_2.Content = "26.45zł";
            i++;
            //3
            ResultBarDepartureTimeLabel_3.Content = "10:26";
            ResultBarArrivalTimeLabel_3.Content = "11:42";
            ResultBarCarrierNameLabel_3.Background = new SolidColorBrush(Color.FromRgb(191, 183, 0));
            ResultBarCarrierNameLabel_3.Content = "IC";
            ResultBarFastestTag_3.Visibility = Visibility.Hidden;
            ResultBarTravelTimeLabel_3.Content = "1H16";
            ResultBarCheapestTag_3.Visibility = Visibility.Hidden;
            ResultBarTicketPriceLabel_3.Content = "26.45zł";
            i++;
            //4
            ResultBarDepartureTimeLabel_4.Content = "10:26";
            ResultBarArrivalTimeLabel_4.Content = "11:42";
            ResultBarCarrierNameLabel_4.Background = new SolidColorBrush(Color.FromRgb(20, 0, 27));
            ResultBarCarrierNameLabel_4.Content = "KD";
            ResultBarFastestTag_4.Visibility = Visibility.Hidden;
            ResultBarTravelTimeLabel_4.Content = "1H16";
            ResultBarCheapestTag_4.Visibility = Visibility.Hidden;
            ResultBarTicketPriceLabel_4.Content = "26.45zł";
            i++;
            //5
            ResultBarDepartureTimeLabel_5.Content = "10:26";
            ResultBarArrivalTimeLabel_5.Content = "11:42";
            ResultBarCarrierNameLabel_5.Background = new SolidColorBrush(Color.FromRgb(1, 55, 101));
            ResultBarCarrierNameLabel_5.Content = "PKP";
            ResultBarFastestTag_5.Visibility = Visibility.Hidden;
            ResultBarTravelTimeLabel_5.Content = "1H16";
            ResultBarCheapestTag_5.Visibility = Visibility.Visible;
            ResultBarTicketPriceLabel_5.Content = "24.45zł";
            i++;
            //6
            ResultBarDepartureTimeLabel_6.Content = "10:26";
            ResultBarArrivalTimeLabel_6.Content = "11:42";
            ResultBarCarrierNameLabel_6.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
            ResultBarCarrierNameLabel_6.Content = "PR";
            ResultBarFastestTag_6.Visibility = Visibility.Hidden;
            ResultBarTravelTimeLabel_6.Content = "1H16";
            ResultBarCheapestTag_6.Visibility = Visibility.Hidden;
            ResultBarTicketPriceLabel_6.Content = "26.45zł";
            i++;
            //7
            ResultBarDepartureTimeLabel_7.Content = "10:26";
            ResultBarArrivalTimeLabel_7.Content = "11:42";
            ResultBarCarrierNameLabel_7.Background = new SolidColorBrush(Color.FromRgb(191, 183, 0));
            ResultBarCarrierNameLabel_7.Content = "IC";
            ResultBarFastestTag_7.Visibility = Visibility.Hidden;
            ResultBarTravelTimeLabel_7.Content = "1H16";
            ResultBarCheapestTag_7.Visibility = Visibility.Hidden;
            ResultBarTicketPriceLabel_7.Content = "26.45zł";
            i++;
            //8
            ResultBarDepartureTimeLabel_8.Content = "10:26";
            ResultBarArrivalTimeLabel_8.Content = "11:42";
            ResultBarCarrierNameLabel_8.Background = new SolidColorBrush(Color.FromRgb(20, 0, 27));
            ResultBarCarrierNameLabel_8.Content = "KD";
            ResultBarFastestTag_8.Visibility = Visibility.Hidden;
            ResultBarTravelTimeLabel_8.Content = "1H16";
            ResultBarCheapestTag_8.Visibility = Visibility.Hidden;
            ResultBarTicketPriceLabel_8.Content = "26.45zł";
            i++;
            //9
            ResultBarDepartureTimeLabel_9.Content = "10:26";
            ResultBarArrivalTimeLabel_9.Content = "11:42";
            ResultBarCarrierNameLabel_9.Background = new SolidColorBrush(Color.FromRgb(1, 55, 101));
            ResultBarCarrierNameLabel_9.Content = "PKP";
            ResultBarFastestTag_9.Visibility = Visibility.Visible;
            ResultBarTravelTimeLabel_9.Content = "1H12";
            ResultBarCheapestTag_9.Visibility = Visibility.Hidden;
            ResultBarTicketPriceLabel_9.Content = "26.45zł";
        }

        private void DestinationFromName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DestinationToName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DestinationCarrierName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DestinationTimeTravel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DestinationTicketPrice_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
//▼
//ResultBarCarrierNameLabel_5.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
/* for later use
ResultBarDepartureTimeLabel_1.Content = "";
ResultBarArrivalTimeLabel_1.Content = "";
ResultBarCarrierNameLabel_1.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
ResultBarCarrierNameLabel_1.Content = "";
ResultBarFastestTag_1.Visibility = Visibility.Hidden;
ResultBarTravelTimeLabel_1.Content = "";
ResultBarCheapestTag_1.Visibility = Visibility.Hidden;
ResultBarTicketPriceLabel_1.Content = "";
i++;
*/