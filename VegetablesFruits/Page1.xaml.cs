using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace VegetablesFruits
{

    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            Page1Load();

        }

        public async Task Page1Load()
        {
            string conStr = @"Data Source = STHQ012E-09; Database=VegetablesFruits; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";
            string sqlQuery = "SELECT DISTINCT Color FROM Products";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        ColorSelection.Items.Add(reader.GetString(0));
                    }
                }
            }

        }
        private async void FillDataGrid(string sqlQuery)
        {

            string conStr = @"Data Source = STHQ012E-09; Database=VegetablesFruits; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";
            DataTable dataTable = await ExecuteSqlQueryAsync(sqlQuery, conStr);


            TableGrid.ItemsSource = dataTable.DefaultView;
        }

        public async Task<DataTable> ExecuteSqlQueryAsync(string sqlQuery, string conStr)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        private void SeeAll_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT * FROM Products";

            FillDataGrid(sqlQuery);
        }
        private void SeeNames_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT Name FROM Products";

            FillDataGrid(sqlQuery);
        }
        private void SeeColors_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT DISTINCT Color FROM Products ";

            FillDataGrid(sqlQuery);
        }
        private void SeeMaxCal_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT MAX(Calories) FROM Products";

            FillDataGrid(sqlQuery);
        }
        private void SeeMinCal_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT MIN(Calories) FROM Products";

            FillDataGrid(sqlQuery);
        }
        private void SeeAvgCal_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT AVG(Calories) FROM Products";

            FillDataGrid(sqlQuery);
        }

        private void SeeVegCount_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT COUNT(*) FROM Products WHERE Types = 'Vegetable'";

            FillDataGrid(sqlQuery);
        }
        private void SeeFruitCount_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT COUNT(*) FROM Products WHERE Types = 'Fruit'";

            FillDataGrid(sqlQuery);
        }
        private void SeeEachColor_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = "SELECT Color, COUNT(*) AS Count FROM Products GROUP BY Color";

            FillDataGrid(sqlQuery);
        }
        private void SeeByColor_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = $"SELECT COUNT(*) FROM Products WHERE Color = '{ColorSelection.SelectedValue.ToString()}'";

            FillDataGrid(sqlQuery);
        }
        private void SeeByCalories_Click(object sender, RoutedEventArgs e)
        {
            if(MinCal.Text=="Min" || MinCal.Text == "") {
                MinCal.Text = "0";
            }
            else if (MaxCal.Text=="Max" || MinCal.Text == "")
            {
                MaxCal.Text = "2000";
            }
            string sqlQuery = $"SELECT Name, Calories FROM Products WHERE (Calories > {MinCal.Text} AND Calories < {MaxCal.Text})";

            FillDataGrid(sqlQuery);
        }

        private void SeeRedOrYellow_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = $"SELECT * FROM Products WHERE Color = 'Yellow' OR Color='Red'";

            FillDataGrid(sqlQuery);
        }

    }
}