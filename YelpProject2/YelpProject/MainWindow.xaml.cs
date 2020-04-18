using Npgsql;
using System;
using System.Collections.Generic;
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
        private TipsWindow tipWindow;
        private CheckInWindow checkInWindow;
        private bool tipsWindowIsOpen = false;
        private bool checkinWindowIsOpen = false;

        public MainWindow()
        {
            InitializeComponent();



        }


        private void initializeAll()
        {
            addColumns2BusinessGrid();
            addColumns2UserGrid();
            addStates();
        }


        //private string buildConnectionString(string host = "localhost", string username = "postgres", string database = "yelpdb", string password = "postgres")
        //{
        //    return "Host = " + host + "; Username = " + username + "; Database = " + database + "; password = " + password + ";";
        //}

        //public bool executeQuery(string sqlstr, Action<NpgsqlDataReader> myfunc)
        //{
        //    using (var connection = new NpgsqlConnection(buildConnectionString()))
        //    {
        //        connection.Open();
        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = connection;
        //            cmd.CommandText = sqlstr;
        //            try
        //            {
        //                var reader = cmd.ExecuteReader();
        //                while (reader.Read())
        //                    myfunc(reader);
        //            }
        //            catch (NpgsqlException ex)
        //            {
        //                Console.WriteLine(ex.Message.ToString());
        //                return true;
        //                //System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }

        //            return false;
        //        }
        //    }
        //}

        private void queryStates(NpgsqlDataReader R)
        {
            statecomboBox.Items.Add(R.GetString(0));
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
            cityListBox.Items.Add(R.GetString(0));
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

        private void queryCity(NpgsqlDataReader R)
        {
            categoryListBox.Items.Add(R.GetString(0));
        }

        private void queryZipcode(NpgsqlDataReader R)
        {
            categoryListBox.Items.Add(R.GetString(0));
        }

        private void queryCategories(NpgsqlDataReader R)
        {
            categoryListBox.Items.Add(R.GetString(0));
        }

        private void addColumns2BusinessGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // name
            DataGridTextColumn col2 = new DataGridTextColumn(); // address
            DataGridTextColumn col3 = new DataGridTextColumn(); // city
            DataGridTextColumn col4 = new DataGridTextColumn(); // state
            DataGridTextColumn col5 = new DataGridTextColumn(); // distance
            DataGridTextColumn col6 = new DataGridTextColumn(); // stars
            DataGridTextColumn col7 = new DataGridTextColumn(); // #tips
            DataGridTextColumn col8 = new DataGridTextColumn(); // total Checkins

            col1.Binding = new Binding("name");
            col2.Binding = new Binding("address");
            col3.Binding = new Binding("city");
            col4.Binding = new Binding("state");
            col5.Binding = new Binding("distance");
            col6.Binding = new Binding("stars");
            col7.Binding = new Binding("numTips");
            col8.Binding = new Binding("numCheckins");

            col1.Header = "BusinessName";
            col2.Header = "Address";
            col3.Header = "City";
            col4.Header = "State";
            col5.Header = "Distance (Miles)";
            col6.Header = "Stars";
            col7.Header = "# Tips";
            col8.Header = "Total Checkins";

            col1.Width = 300;
            col2.Width = 150;
            col3.Width = 100;
            col4.Width = 50;
            col5.Width = 50;
            col6.Width = 50;
            col7.Width = 50;
            col8.Width = 50;

            businessDataGrid.Columns.Add(col1);
            businessDataGrid.Columns.Add(col2);
            businessDataGrid.Columns.Add(col3);
            businessDataGrid.Columns.Add(col4);
            businessDataGrid.Columns.Add(col5);
            businessDataGrid.Columns.Add(col6);
            businessDataGrid.Columns.Add(col7);
            businessDataGrid.Columns.Add(col8);

            businessDataGrid.CanUserResizeColumns = false;
            businessDataGrid.CanUserResizeRows = false;
            businessDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            businessDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            businessDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }

        private void addColumns2UserFriendsGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // name
            DataGridTextColumn col2 = new DataGridTextColumn(); // TotalLikes
            DataGridTextColumn col3 = new DataGridTextColumn(); // Avg Stars
            DataGridTextColumn col4 = new DataGridTextColumn(); // Yelping Since

            col1.Binding = new Binding("name");
            col2.Binding = new Binding("likecount");
            col3.Binding = new Binding("avg_stars");
            col4.Binding = new Binding("yelping_since");


            col1.Header = "Name";
            col2.Header = "TotalLikes";
            col3.Header = "Avg Stars";
            col4.Header = "Yelping Since";

            col1.Width = 200;
            col2.Width = 70;
            col3.Width = 50;
            col4.Width = 100;

            businessDataGrid.Columns.Add(col1);
            businessDataGrid.Columns.Add(col2);
            businessDataGrid.Columns.Add(col3);
            businessDataGrid.Columns.Add(col4);

            businessDataGrid.CanUserResizeColumns = false;
            businessDataGrid.CanUserResizeRows = false;
            businessDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            businessDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            businessDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }

        private void addColumns2SelectedBusinessAttributes()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // att_name

            col1.Binding = new Binding("att_name");

            col1.Header = "Attribute Name";

            col1.Width = 100;

            attributesDataGrid.Columns.Add(col1);

            attributesDataGrid.CanUserResizeColumns = false;
            attributesDataGrid.CanUserResizeRows = false;
            attributesDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            attributesDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            attributesDataGrid.IsEnabled = false;
            //businessDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }

        private void addColumns2SelectedBusinessCategories()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // cat_name

            col1.Binding = new Binding("cat_name");

            col1.Header = "Category Name";

            col1.Width = 100;

            categoryDataGrid.Columns.Add(col1);

            categoryDataGrid.CanUserResizeColumns = false;
            categoryDataGrid.CanUserResizeRows = false;
            categoryDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            categoryDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            categoryDataGrid.IsEnabled = false;
            //businessDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }

        private void addColumns2UserTipsGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // user_name
            DataGridTextColumn col2 = new DataGridTextColumn(); // bus_name
            DataGridTextColumn col3 = new DataGridTextColumn(); // City
            DataGridTextColumn col4 = new DataGridTextColumn(); // Text
            DataGridTextColumn col5 = new DataGridTextColumn(); // date

            col1.Binding = new Binding("user_name");
            col2.Binding = new Binding("bus_name");
            col3.Binding = new Binding("city");
            col4.Binding = new Binding("text");
            col5.Binding = new Binding("date");


            col1.Header = "User Name";
            col2.Header = "Business";
            col3.Header = "City";
            col4.Header = "Text";
            col5.Header = "Date";

            col1.Width = 100;
            col2.Width = 100;
            col3.Width = 50;
            col4.Width = 300;
            col5.Width = 100;

            businessDataGrid.Columns.Add(col1);
            businessDataGrid.Columns.Add(col2);
            businessDataGrid.Columns.Add(col3);
            businessDataGrid.Columns.Add(col4);
            businessDataGrid.Columns.Add(col5);

            businessDataGrid.CanUserResizeColumns = false;
            businessDataGrid.CanUserResizeRows = false;
            businessDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            businessDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            businessDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }


        private void addColumns2UserGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // user_id

            col1.Binding = new Binding("user_id");
            col1.Header = "User Id";

            col1.Width = 300;
            setUserDataGrid.Columns.Add(col1);


            setUserDataGrid.CanUserResizeColumns = false;
            setUserDataGrid.CanUserResizeRows = false;
            setUserDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            setUserDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }


        private void disableCurrentUserInfo()
        {
            //Disable all of the current user info buttons
            curnametextBox.IsEnabled = false;
            curstarstextBox.IsEnabled = false;
            fanstextBox.IsEnabled = false;
            yelpingsincetextBox.IsEnabled = false;
            funnytextBox.IsEnabled = false;
            cooltextBox.IsEnabled = false;
            usefultextBox.IsEnabled = false;
            tipCounttextBox.IsEnabled = false;
            tiptotaltextBox.IsEnabled = false;
            longtextBox.IsEnabled = false;
            lattextBox.IsEnabled = false;
        }

        private void queryUserFriends(NpgsqlDataReader R)
        {
            friendDataGrid.Items.Add(new Friend() { name = R.GetString(0), bid = R.GetString(1) });
        }

        private void queryUserTipFriends(NpgsqlDataReader R)
        {
            latestFriendTipsDataGrid.Items.Add(new Friend() { name = R.GetString(0), bid = R.GetString(1) });
        }

        //private void queryBusiness(NpgsqlDataReader R)
        //{
        //    businessGrid.Items.Add(new Business() { name = R.GetString(0), bid = R.GetString(1) });
        //}

        private void queryBusinessSearchResult(NpgsqlDataReader R)
        {
            businessDataGrid.Items.Add(new Business() { name = R.GetString(0), bid = R.GetString(1) });
        }


        private void queryCurUser(NpgsqlDataReader R)
        {
            setUserDataGrid.Items.Add(new User() { id = R.GetString(0), name = R.GetString(1) });
            
            //Set the other information for the current user

            //Set the current user (or we could just use the grid)
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
                    sqlstr1 + ") as bus, CategoryTable WHERE CategoryTable.businessID = bus.businessID ORDER BY cat_name";
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


        private void addSortResultsByItems()
        {
            List<string> items = new List<string> { "Business Name (Default)", "Highest Rating(Stars)", "Most Tips", "Most Check-ins", "Nearest" };
            foreach (string item in items)
            {
                sortResultsByComboBox.Items.Add(item);
            }

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

        //private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //Future works
        //}

        //Business Datagrid
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the data for the datagrid

            //Update the selected business

        }

        //Category Box changes
        private void busCategorylistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Edit current User information
        private void editbutton_Click(object sender, RoutedEventArgs e)
        {

        }

        //Update current User information
        private void updatebutton_Click(object sender, RoutedEventArgs e)
        {

        }

        //User entering in new current user (via name)
        private void setUsertextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //User selected for new current user datagrid
        private void setUserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Resort the datagrid using the chosen option
        private void sortResultsByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Show the next window (checkin)
        private void showCheckinsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //Show the next window (Tips)
        private void showTipsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //City is selected
        private void cityListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Zip code is selected
        private void zipcodeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //A state is selected
        private void statecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
