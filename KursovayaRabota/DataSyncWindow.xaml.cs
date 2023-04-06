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
using System.Windows.Media.Animation;
using KursovayaRabota;
using System.Windows.Navigation;
using System.Data.SqlClient;

namespace KursovayaRabota
{
    /// <summary>
    /// Логика взаимодействия для DataSyncWindow.xaml
    /// </summary>
    public partial class DataSyncWindow : Window
    {
        public DataSyncWindow()
        {
            InitializeComponent();           
            var timer = new System.Timers.Timer(5000);
            timer.Start();
            timer.Elapsed += (s, args) => // таймер для выполнения метода
            {
                timer.Stop();
                this.Dispatcher.Invoke(new Action(delegate ()
                {
                    waitLabel.Content = "Готово!";
                }
            ));              
                SyncDataAndClose(); 
            };           
        }
        public void SyncDataAndClose() // метод который проверяет подключение к БД и актуальность данных
        {
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                continueButton.IsEnabled = true;
            }
            ));
            string connectionString = @"Server=.\SQLEXPRESS;Database=master;Integrated Security=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"SELECT COUNT(*) FROM sys.databases WHERE name = 'TOSystem'", connection);
                int count = (int)command.ExecuteScalar();

                if (count == 0)
                {
                    MessageBox.Show("Ошибка! БД не создана/не подключилась!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                if (count == 1)
                {
                    MessageBox.Show("Все успешно прошло! База данных синхронизирована.", "!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            this.dataSyncWindoww.Close();
        }
    }
}
