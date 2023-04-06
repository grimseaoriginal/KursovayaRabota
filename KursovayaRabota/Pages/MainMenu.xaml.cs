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

namespace KursovayaRabota.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
        }

        private void openBD_button_Click(object sender, RoutedEventArgs e)
        {
            DBPage dB = new DBPage();
            this.NavigationService.Navigate(dB);
        }

        private void syncData_button_Click(object sender, RoutedEventArgs e)
        {
            DataSyncWindow dtSync = new DataSyncWindow();
            dtSync.Show();
        }
    }
}
