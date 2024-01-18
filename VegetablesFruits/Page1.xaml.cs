using Microsoft.Data.SqlClient;
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


        }

        private async void FillDataGrid(string sqlQuery)
        {

            string conStr = @"Data Source = DESKTOP-0LP9EBH; Initial Catalog = VegetablesFruits; TrustServerCertificate=true; Integrated Security = true;";

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
        //private void SeeVegCount_Click(object sender, RoutedEventArgs e)
        //{
        //    string sqlQuery = "SELECT COUNT(*) FROM Products WHERE Types = 'Vegetable'";

        //    FillDataGrid(sqlQuery);
        //}
        //private void SeeVegCount_Click(object sender, RoutedEventArgs e)
        //{
        //    string sqlQuery = "SELECT COUNT(*) FROM Products WHERE Types = 'Vegetable'";

        //    FillDataGrid(sqlQuery);
        //}

    }
}