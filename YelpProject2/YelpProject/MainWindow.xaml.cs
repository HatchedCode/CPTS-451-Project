using Npgsql;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YelpProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Business
        {
            public string name { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            addStates();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone2db; password = postgres";
        }
        private void executeQuery(string sqlstr, Action<NpgsqlDataReader> myfunc)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlstr;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            myfunc(reader);
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void queryStates(NpgsqlDataReader R)
        {
            stateList.Items.Add(R.GetString(0));
        }

        private void addStates()
        {
            string sqlstr = "SELECT distinct state FROM business ORDER BY state";
            executeQuery(sqlstr, queryStates);
        }

        private void queryCity(NpgsqlDataReader R)
        {
            cityList.Items.Add(R.GetString(0));
        }

        private void stateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityList.Items.Clear();
            if (stateList.SelectedIndex > -1)
            {
                string sqlstr = "SELECT distinct city FROM business '" + stateList.SelectedItem.ToString() + "' ORDER BY city";
                executeQuery(sqlstr, queryCity);
            }
        }

        private void queryZip(NpgsqlDataReader R)
        {
            zipList.Items.Add(R.GetString(0));
        }

        private void cityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zipList.Items.Clear();
            if (zipList.SelectedIndex > -1)
            {
                string sqlstr = "SELECT distinct postal FROM business WHERE state = '" + stateList.SelectedItem.ToString() + "' AND city = '" + cityList.SelectedItem.ToString() + "' ORDER BY postal";
                executeQuery(sqlstr, queryZip);
            }
        }

        private void queryCategories(NpgsqlDataReader R)
        {
            categoryListBox.Items.Add(R.GetString(0));
        }

        private void zipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryListBox.Items.Clear();
            if (categoryListBox.SelectedIndex > -1)
            {
                // Need to make the sql statement to extract category name
                string sqlstr = "SELECT distinct cat_name FROM category WHERE ";
                executeQuery(sqlstr, queryCategories);
            }
        }

        private void addColumns2BusinessGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // name
            DataGridTextColumn col2 = new DataGridTextColumn(); // id

            col1.Binding = new Binding("name");
            col1.Header = "Business Name";
            col1.Width = 230;
            businessGrid.Columns.Add(col1);

            col2.Binding = new Binding("bid");
            col2.Header = "ID";
            col2.Width = 100;
            businessGrid.Columns.Add(col2);
        }

        private void queryBusiness(NpgsqlDataReader R)
        {
            businessGrid.Items.Clear();
            //if (businessGrid.sele)
        }
    }
}
