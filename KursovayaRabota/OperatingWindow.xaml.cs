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
using KursovayaRabota.Pages;
using System.Data.SqlClient;

namespace KursovayaRabota
{
    /// <summary>
    /// Логика взаимодействия для OperatingWindow.xaml
    /// </summary>
    public partial class OperatingWindow : Window
    {
        public OperatingWindow()
        {
            InitializeComponent();
            LoadWindow();

            CreateObjects();

            string connectionString = @"Server=.\SQLEXPRESS;Database=master;Integrated Security=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(@"SELECT COUNT(*) FROM sys.databases WHERE name = 'TOSystem'", connection);
                int count = (int)command.ExecuteScalar();

                if (count == 0)
                {
                    MessageBox.Show("Ошибка! БД не создана/не подключилась! После данного сообщения будет проведена попытка создать БД и нужные таблицы.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                if (count == 1)
                {
                    MessageBox.Show("Запуск прошёл успешно! Удачного пользования.", "!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void LoadWindow()
        {
            LoginPage LP = new LoginPage();
            operatingFrame.Content = LP;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
        }
        private void CreateObjects()
        {
            string connectionString = @"Server=.\SQLEXPRESS;Database=master;Integrated Security=true"; // подключается полностью к серверу SQLExpress
            

            // Создаётся само подключение, а также запросы для взаимодействия
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // открывается подключение
                connection.Open();

                // комманда/запрос
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM sys.databases WHERE name = 'TOSystem'", connection); // считает сколько есть баз данных с именем TOSystem

                // выполнение комманды
                int count = (int)command.ExecuteScalar();

                if (count == 0) // если БД не найдено
                {
                    // Создать БД
                    command = new SqlCommand("CREATE DATABASE TOSystem", connection);
                    command.ExecuteNonQuery();
                }
            }
            string con2 = @"Server=.\SQLEXPRESS;Database=TOSystem;Integrated Security=true"; // подключается к БД TOSystem
            using (SqlConnection connect2 = new SqlConnection(con2)) // подключается к Bankomat для выполнения запросов
            {
                connect2.Open();

                SqlCommand commandCheck = new SqlCommand("SELECT COUNT(*) FROM information_schema.tables WHERE table_name = 'Users'", connect2); // счиатет сколько есть таблиц с именем Users
                int count2 = (int)commandCheck.ExecuteScalar();
                if (count2 == 0) // если нету
                {
                    SqlCommand command2 = new SqlCommand("CREATE TABLE Users (Login varchar(50), Password varchar(50))", connect2); // создаёт таблицу
                    command2.ExecuteNonQuery();
                    SqlCommand command4 = new SqlCommand("CREATE TABLE CompletedTO (Number int, CarModel varchar(250), CarPlateNumber varchar(250), DateOfCompleting date)", connect2);
                    command4.ExecuteNonQuery();
                    SqlCommand command3 = new SqlCommand("INSERT INTO Information (Login, Password) VALUES ('123', '321')", connect2); // заполняет её первичными данными
                    command3.ExecuteNonQuery();
                }
                if (count2 >= 1)
                {
                    //
                }
            }
        }
    }
}
