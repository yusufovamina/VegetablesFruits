using System;
using System.CodeDom;
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
using Microsoft.Data.SqlClient;

#region DbCreation
//string conStr = @"Data Source = STHQ012E-09; Database=master; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";

//using (SqlConnection connection = new SqlConnection(conStr))
//{
//    await connection.OpenAsync();
//    SqlCommand command = new SqlCommand();
//    command.CommandText = "CREATE DATABASE VegetablesFruits";
//    command.Connection = connection;
//    await command.ExecuteNonQueryAsync();
//}


//string conStr = @"Data Source = STHQ012E-09; Database=VegetablesFruits; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";


//using (SqlConnection connection = new SqlConnection(conStr))
//{
//    await connection.OpenAsync();
//    SqlCommand command = new SqlCommand();
//    command.CommandText = "CREATE TABLE Products(Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(30) NOT NULL, Types NVARCHAR(15) NOT NULL, Color NVARCHAR(30) NOT NULL, Calories INT NOT NULL)";
//    command.Connection = connection;
//    await command.ExecuteNonQueryAsync();
//}

//string conStr = @"Data Source = STHQ012E-09; Database=VegetablesFruits; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";

//using (SqlConnection connection = new SqlConnection(conStr))
//{
//    await connection.OpenAsync();
//    SqlCommand command = new SqlCommand();
//    command.CommandText = "INSERT INTO Products(Name, Types, Color, Calories) VALUES ('Tomato','Vegetable','Red',35)," +
//                                                                         " ('Cucumber','Vegetable','Green',27)," +
//                                                                        " ('Rastberry', 'Berry','Red',63),"+
//                                                                        " ('Orange', 'Fruit','Orange',58)," +
//                                                                        " ('Carrot', 'Vegetable','Orange',34)," +
//                                                                        " ('Apple','Fruit' ,'Green',51)," +
//                                                                        " ('Pumkin', 'Vegetable','Orange',63)," +
//                                                                        " ('Grapes', 'Fruit','Violet',89)," +
//                                                                        " ('Corn', 'Vegetable','Yellow',63)," +
//                                                                        " ('Lemon', 'Fruit','Yellow',25)," +
//                                                                        " ('Brocolli', 'Vegetable','Green',44)," +
//                                                                        " ('Banana', 'Fruit','Yellow',110)," +
//                                                                        " ('Strawberry', 'Berry','Red',78)," +
//                                                                        " ('Blueberry', 'Berry','Violet',83)," +
//                                                                        " ('Pineapple', 'Fruit','Yellow',133)," +
//                                                                        " ('Peach', 'Fruit','Red',101)";
//    command.Connection = connection;

//}
#endregion

namespace VegetablesFruits
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Visibility = Visibility.Hidden;
            MainFrame.Content = new Page1();
        }

        public void connection_OpenClose(object sender, System.Data.StateChangeEventArgs e)
        {
            SqlConnection connection = sender as SqlConnection;


            ServerNameTextBlock.Text = connection.DataSource;
            DBNameTextBlock.Text = connection.Database;

        }
        public void ConnectButton_Click(object sender, RoutedEventArgs e)
        {

            if (Username.Text == "admin" && Password.Password == "admin")
            {
                string conStr = @"Data Source = STHQ012E-09; Database=VegetablesFruits; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";
              
                ellipse.Fill = Brushes.Green;
                StateLabel.Content = "Connected";
                SqlConnection connection = new SqlConnection(conStr);


                connection.StateChange += connection_OpenClose;

                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }


            else
            {

                MessageBox.Show("Incorrect username or password!");
            }

        }
        public void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            string conStr = @"Data Source = STHQ012E-09; Database=VegetablesFruits; TrustServerCertificate=true; Integrated Security = false; User Id = admin; Password = admin;";

            SqlConnection connection = new SqlConnection(conStr);
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ellipse.Fill = Brushes.Red;
            StateLabel.Content = "Disconnected";
            ServerNameTextBlock.Text = "";
            DBNameTextBlock.Text = "";
            Username.Text = "";
            Password.Password = "";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text == "admin" && Password.Password == "admin")
            {
                MainFrame.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Incorrect username or password!");
            }
        }
    }
}
