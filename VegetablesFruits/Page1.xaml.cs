using Microsoft.Data.SqlClient;
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

namespace VegetablesFruits
{
    class MyTable
    {
        public MyTable(int Id, string Name, string Type, string Color ,int Calories)
        {
            this.Id = Id;
            this.Name = Name;
            this.Type = Type;
            this.Color = Color;
            this.Calories=Calories;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int Calories { get; set; }
    }
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        public async Task fill(object sender, RoutedEventArgs e)
        {
            string sqlQuery = $"SELECT * FROM Customers";
            string conStr = @"Data Source = STHQ012E-09; Database=VegetablesFruits; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                List<MyTable> result = new List<MyTable>(3);
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    for (int i = 0; i < reader.FieldCount; i+= reader.FieldCount)
                    {
                        result.Add(new MyTable((int)reader.GetValue(i), (string)reader.GetValue(i+1), (string)reader.GetValue(i+2), (string)reader.GetValue(i+3), (int)reader.GetValue(i+4)));
                      
                    }
                    
                }
                TableGrid.ItemsSource = result;
            }
            
        }
        public async Task ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlQuery = $"SELECT * FROM Products";
            string conStr = @"Data Source = STHQ012E-09; Database=VegetablesFruits; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    
                }
            }
        }
    }
}
