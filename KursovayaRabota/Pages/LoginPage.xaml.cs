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
using KursovayaRabota.Pages;
using System.Data.SqlClient;

namespace KursovayaRabota.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string _login;
        private string _password;
        public LoginPage()
        {
            InitializeComponent();
        }
        public void ChangeFrameContent(Frame frame, Page page)
        {
            frame.Content = page;
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAccount();
        }

        private void closeApp_button_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
        }

        private bool CheckAccount() // метод для проверки авторизации
        {
            _login = loginBox.Text;
            _password = passBox.Text;
            SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=TOSystem;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Login = @login AND Password = @password", conn);
            cmd.Parameters.AddWithValue("@login", _login);
            cmd.Parameters.AddWithValue("@password", _password);
            conn.Open();
            int count = (int)cmd.ExecuteScalar(); // добавляет число если имеется пользователь
            if (count > 0) // если пользователь есть, то пропускает в систему
            {
                MessageBox.Show("Добро пожаловать!", "Здравствуйте!", MessageBoxButton.OK, MessageBoxImage.Information);
                MainMenu mM = new MainMenu();
                this.NavigationService.Navigate(mM);
                return count > 0;
            }
            else // в противном случае нет
            {
                MessageBox.Show("Данного пользователя не существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Question);
                return count == 0;
            }
        }
    }
}
