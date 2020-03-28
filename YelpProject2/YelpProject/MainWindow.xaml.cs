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
            public string bid { get; set; }
        }

        public class User
        {
            public string name { get; set; }
            public string id { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            addColumns2BusinessGrid();
            addColumns2UserGrid();
            addStates();
        }

        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = yelpdb; password = postgres";
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
            string sqlstr = "SELECT distinct busState FROM BusinessTable ORDER BY busState";
            executeQuery(sqlstr, queryStates);
        }

        private void addUsers()
        {
            string sqlstr = "SELECT distinct user_id, name FROM UserTable ORDER BY user_id";
            executeQuery(sqlstr, queryUser);
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
                string sqlstr = "SELECT distinct busCity FROM BusinessTable WHERE busState = '" + stateList.SelectedItem.ToString() + "' ORDER BY busCity";
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
            if (cityList.SelectedIndex > -1)
            {
                string sqlstr = "SELECT distinct busPostal FROM BusinessTable WHERE busState = '" + stateList.SelectedItem.ToString() + "' AND busCity = '" + cityList.SelectedItem.ToString() + "' ORDER BY busPostal";
                executeQuery(sqlstr, queryZip);
            }
        }

        private void queryCategories(NpgsqlDataReader R)
        {
            categoryListBox.Items.Add(R.GetString(0));
        }

        private void addColumns2BusinessGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // name

            col1.Binding = new Binding("name");
            col1.Header = "Business Name";
            col1.Width = 330;
            businessGrid.Columns.Add(col1);
            businessGrid.CanUserResizeColumns = false;
            businessGrid.CanUserResizeRows = false;
        }

        private void addColumns2UserGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // name
            DataGridTextColumn col2 = new DataGridTextColumn(); // id

            col1.Binding = new Binding("name");
            col1.Header = "User Name";
            col1.Width = 330/2;
            usersGrid.Columns.Add(col1);

            col2.Binding = new Binding("id");
            col2.Header = "User Id";
            col2.Width = 330/2;
            usersGrid.Columns.Add(col2);


            usersGrid.CanUserResizeColumns = false;
            usersGrid.CanUserResizeRows = false;

            usersGrid.SelectionMode = DataGridSelectionMode.Single;
        }


        private void queryBusiness(NpgsqlDataReader R)
        {
            businessGrid.Items.Add(new Business() { name = R.GetString(0), bid = R.GetString(1) });
        }

        private void queryUser(NpgsqlDataReader R)
        {
            usersGrid.Items.Add(new User() { id = R.GetString(0), name = R.GetString(1) });
        }

        private void zipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryListBox.Items.Clear();
            businessGrid.Items.Clear();
            if (zipList.SelectedIndex > -1)
            {
                // Need to make the sql statement to extract category name
                string sqlstr1 = "SELECT businessID FROM BusinessTable WHERE busPostal = '" + zipList.SelectedItem.ToString() + "' ORDER BY busPostal";
                string sqlstr = "SELECT distinct cat_name FROM (" +
                    sqlstr1 + ") as bus FULL OUTER JOIN CategoryTable ON CategoryTable.businessID = bus.businessID ORDER BY cat_name";
                executeQuery(sqlstr, queryCategories);

                string sqlstr2 = "SELECT busName, businessID FROM BusinessTable WHERE busPostal = '" + zipList.SelectedItem.ToString() + "' ORDER BY busName, businessID";
                executeQuery(sqlstr2, queryBusiness);
            }
        }

        private void categoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessGrid.Items.Clear();
            string sqlstr = "SELECT distinct BusinessTable.busName, BusinessTable.businessID FROM BusinessTable, CategoryTable WHERE BusinessTable.busPostal = '" + zipList.SelectedItem.ToString() + "'";
            for (int i = 0; i < categoryListBox.SelectedItems.Count; i++)
            {
                sqlstr += " AND BusinessTable.businessID IN (SELECT businessID FROM CategoryTable WHERE cat_name = '" + categoryListBox.SelectedItems[i].ToString() + "')";
            }
            executeQuery(sqlstr, queryBusiness);
        }

        private void queryTips(NpgsqlDataReader R)
        {
            tipListBox.Items.Add(R.GetString(0));
        }

        private void businessGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tipListBox.Items.Clear();
            if (businessGrid.Items.IndexOf(businessGrid.SelectedItem) > -1)
            {
                string sqlstr1 = "SELECT businessID FROM BusinessTable WHERE busName = '" + businessGrid.SelectedItem.ToString() + "' ORDER BY busName";
                string sqlstr = "SELECT distinct text, date FROM (" +
                    sqlstr1 + ") as bus FULL OUTER JOIN TipTable ON TipTable.businessID = bus.businessID ORDER BY date";
                executeQuery(sqlstr, queryTips);
                addUsers();
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (businessGrid.Items.IndexOf(businessGrid.SelectedItem) > -1 && leaveTipTextBox.Text.Length > 0 && usersGrid.Items.IndexOf(usersGrid.SelectedItem) > -1)
            {
                string sqlstr = "INSERT INTO TipTable(businessID, user_id, likes, text, date) VALUES('" + (businessGrid.SelectedItem as Business).bid + "', '" +
                 (usersGrid.SelectedItem as User).id + "', " + "0" + ", '" + leaveTipTextBox.Text + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                executeQuery(sqlstr, queryTips);
            }
            else
            {
                MessageBox.Show("Something has gone wrong, FIX IT", "Error adding a tip.", MessageBoxButton.OK);
            }
        }

        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Future works
        }
    }
}
