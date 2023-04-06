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
using System.Data.SqlClient;
using System.Data;

namespace KursovayaRabota.Pages
{
    /// <summary>
    /// Логика взаимодействия для DBPage.xaml
    /// </summary>
    public partial class DBPage : Page
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=TOSystem;Integrated Security = true";
        

        public DBPage()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
            LoadGrid();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        public void LoadGrid() // загрузить данные в Grid элемент
        {
            SqlConnection con = new SqlConnection(connectionString);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * FROM CompletedTO", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dbDataGrid.ItemsSource = dt.DefaultView;
        }

        private void UpdateData() // обновить данные и сохранить в БД
        {
            if (carModel_box.Text == "" || plateNumberBox.Text == "" || chosenDate_box.Text == "")
            {
                MessageBox.Show("Ошибка! Отсутствуют данные!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd1 = new SqlCommand("INSERT INTO CompletedTO (CarModel, CarPlateNumber, DateOfCompleting) VALUES (@V1, @V2, @V3)", con);
                con.Open();
                cmd1.Parameters.AddWithValue("@V1", carModel_box.Text);
                cmd1.Parameters.AddWithValue("@V2", plateNumberBox.Text);
                cmd1.Parameters.AddWithValue("@V3", chosenDate_box.Text);
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            
        }
        private void calen_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDateRN = (DateTime)calen.SelectedDate;
            chosenDate_box.Text = selectedDateRN.ToString();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteRow();
            LoadGrid();
        }

        private void DeleteRow() // удалить какой-то ряд из БД
        {
            
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd1 = new SqlCommand("DELETE FROM CompletedTO WHERE Number = @id", con);
            con.Open();
            int idD = int.Parse(selectedCell.Text);
            cmd1.Parameters.AddWithValue("@id", idD);
            cmd1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Ячейка успешно удаленна!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
