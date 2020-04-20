using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Text.RegularExpressions;
using YelpEngine;
using System.Text;

namespace YelpProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TipsWindow tipWindow = new TipsWindow();
        private CheckInWindow checkInWindow = new CheckInWindow();
        private bool tipsWindowIsOpen = false;
        private bool checkinWindowIsOpen = false;

        public MainWindow()
        {
            InitializeComponent();
            initializeAll();
        }


        private void initializeAll()
        {
            addColumns2BusinessGrid();
            addColumns2UserGrid();
            addColumns2UserFriendsGrid();
            addColumns2SelectedBusinessAttributes();
            addColumns2SelectedBusinessCategories();
            addColumns2UserTipsGrid();
            addColumns2UserFriendsTipsGrid();
            addStates();
            addUsers();
        }

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
            string sqlstr = "SELECT * FROM UserTable ORDER BY user_id";
            executeQuery(sqlstr, queryUser);
        }

        private void queryCity(NpgsqlDataReader R)
        {
            cityListBox.Items.Add(R.GetString(0));
        }

        //private void stateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    cityList.Items.Clear();
        //    if (stateList.SelectedIndex > -1)
        //    {
        //        string sqlstr = "SELECT distinct busCity FROM BusinessTable WHERE busState = '" + stateList.SelectedItem.ToString() + "' ORDER BY busCity";
        //        executeQuery(sqlstr, queryCity);
        //    }
        //}

        //private void queryZip(NpgsqlDataReader R)
        //{
        //    zipList.Items.Add(R.GetString(0));
        //}

        //private void cityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    zipList.Items.Clear();
        //    if (cityList.SelectedIndex > -1)
        //    {
        //        string sqlstr = "SELECT distinct busPostal FROM BusinessTable WHERE busState = '" + stateList.SelectedItem.ToString() + "' AND busCity = '" + cityList.SelectedItem.ToString() + "' ORDER BY busPostal";
        //        executeQuery(sqlstr, queryZip);
        //    }
        //}

        //private void queryCity(NpgsqlDataReader R)
        //{
        //    categoryListBox.Items.Add(R.GetString(0));
        //}

        //private void queryZipcode(NpgsqlDataReader R)
        //{
        //    categoryListBox.Items.Add(R.GetString(0));
        //}

        //private void queryCategories(NpgsqlDataReader R)
        //{
        //    categoryListBox.Items.Add(R.GetString(0));
        //}

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

            friendDataGrid.Columns.Add(col1);
            friendDataGrid.Columns.Add(col2);
            friendDataGrid.Columns.Add(col3);
            friendDataGrid.Columns.Add(col4);

            friendDataGrid.CanUserResizeColumns = false;
            friendDataGrid.CanUserResizeRows = false;
            friendDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            friendDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            friendDataGrid.IsEnabled= false;
        }

        private void addColumns2UserFriendsTipsGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // User Name
            DataGridTextColumn col2 = new DataGridTextColumn(); // Business Name
            DataGridTextColumn col3 = new DataGridTextColumn(); // City
            DataGridTextColumn col4 = new DataGridTextColumn(); // Text
            DataGridTextColumn col5 = new DataGridTextColumn(); // Date

            col1.Binding = new Binding("user_name");
            col2.Binding = new Binding("bus_name");
            col3.Binding = new Binding("bus_city");
            col4.Binding = new Binding("text");
            col5.Binding = new Binding("date");


            col1.Header = "User Name";
            col2.Header = "Business Name";
            col3.Header = "City";
            col4.Header = "Text";
            col4.Header = "Date";

            col1.Width = 200;
            col2.Width = 70;
            col3.Width = 70;
            col4.Width = 100;
            col5.Width = 70;

            latestFriendTipsDataGrid.Columns.Add(col1);
            latestFriendTipsDataGrid.Columns.Add(col2);
            latestFriendTipsDataGrid.Columns.Add(col3);
            latestFriendTipsDataGrid.Columns.Add(col4);
            latestFriendTipsDataGrid.Columns.Add(col5);

            latestFriendTipsDataGrid.CanUserResizeColumns = false;
            latestFriendTipsDataGrid.CanUserResizeRows = false;
            latestFriendTipsDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            latestFriendTipsDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            latestFriendTipsDataGrid.IsEnabled = false;
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

            col1.Binding = new Binding("id");
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

        private void enableCurrentUserInfo()
        {
            //Disable all of the current user info buttons
            curnametextBox.IsEnabled = true;
            curstarstextBox.IsEnabled = true;
            fanstextBox.IsEnabled = true;
            yelpingsincetextBox.IsEnabled = true;
            funnytextBox.IsEnabled = true;
            cooltextBox.IsEnabled = true;
            usefultextBox.IsEnabled = true;
            tipCounttextBox.IsEnabled = true;
            tiptotaltextBox.IsEnabled = true;
            longtextBox.IsEnabled = true;
            lattextBox.IsEnabled = true;
        }

        private void queryUserFriends(NpgsqlDataReader R)
        {
            User newFriend = new User()
            {
                id = R.GetString(0),
                name = R.GetString(1),
                funny = R.GetString(2),
                yelping_since = R.GetString(3),
                useful = R.GetString(4),
                fans = R.GetString(5),
                cool = R.GetString(6),
                avg_stars = R.GetString(7),
                tipcount = R.GetString(8),
                postcount = R.GetString(9),
                likecount = R.GetString(10),
                longitude = R.GetString(11),
                latitude = R.GetString(12)
            };

            friendDataGrid.Items.Add(newFriend);
            //friendDataGrid.Items.Add(new User() { current_user_id = R.GetString(0), friend_user_id = R.GetString(1) });
        }

        private void queryUserTipFriends(NpgsqlDataReader R)
        {
            //0-User Name   1-Business  2-City  3-Text  4-Date
            Tip newTipFriend = new Tip()
            {
                user_name = R.GetString(0),
                bus_name = R.GetString(1),
                bus_city = R.GetString(2),
                text=R.GetString(3),
                date=R.GetString(4)
            };

            latestFriendTipsDataGrid.Items.Add(newTipFriend);
            //latestFriendTipsDataGrid.Items.Add(new Tip() { name = R.GetString(0), bid = R.GetString(1) });
        }

        //private void queryBusiness(NpgsqlDataReader R)
        //{
        //    businessGrid.Items.Add(new Business() { name = R.GetString(0), bid = R.GetString(1) });
        //}

        private void queryBusinessSearchResult(NpgsqlDataReader R)
        {
            businessDataGrid.Items.Add(new Business() { name = R.GetString(0), bid = R.GetString(1) });
        }


        //private void queryCurUser(NpgsqlDataReader R)
        //{
        //    setUserDataGrid.Items.Add(new User() { id = R.GetString(0), name = R.GetString(1) });
            
        //    //Set the other information for the current user

        //    //Set the current user (or we could just use the grid)
        //}


        //private void zipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    categoryListBox.Items.Clear();
        //    businessGrid.Items.Clear();
        //    if (zipList.SelectedIndex > -1)
        //    {
        //        // Need to make the sql statement to extract category name
        //        string sqlstr1 = "SELECT businessID FROM BusinessTable WHERE busPostal = '" + zipList.SelectedItem.ToString() + "' ORDER BY busPostal";
        //        string sqlstr = "SELECT distinct cat_name FROM (" +
        //            sqlstr1 + ") as bus, CategoryTable WHERE CategoryTable.businessID = bus.businessID ORDER BY cat_name";
        //        executeQuery(sqlstr, queryCategories);

        //        string sqlstr2 = "SELECT busName, businessID FROM BusinessTable WHERE busPostal = '" + zipList.SelectedItem.ToString() + "' ORDER BY busName, businessID";
        //        executeQuery(sqlstr2, queryBusiness);
        //    }
        //}

        //private void categoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    businessGrid.Items.Clear();
        //    string sqlstr = "SELECT distinct BusinessTable.busName, BusinessTable.businessID FROM BusinessTable, CategoryTable WHERE BusinessTable.busPostal = '" + zipList.SelectedItem.ToString() + "'";
        //    for (int i = 0; i < categoryListBox.SelectedItems.Count; i++)
        //    {
        //        sqlstr += " AND BusinessTable.businessID IN (SELECT businessID FROM CategoryTable WHERE cat_name = '" + categoryListBox.SelectedItems[i].ToString() + "')";
        //    }
        //    executeQuery(sqlstr, queryBusiness);
        //}


        private void addSortResultsByItems()
        {
            List<string> items = new List<string> { "Business Name (Default)", "Highest Rating(Stars)", "Most Tips", "Most Check-ins", "Nearest" };
            foreach (string item in items)
            {
                sortResultsByComboBox.Items.Add(item);
            }

        }

        //private void queryTips(NpgsqlDataReader R)
        //{
        //    tipListBox.Items.Add(R.GetString(0));
        //}

        //private void businessGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    tipListBox.Items.Clear();
        //    if (businessGrid.Items.IndexOf(businessGrid.SelectedItem) > -1)
        //    {
        //        string sqlstr1 = "SELECT businessID FROM BusinessTable WHERE busName = '" + businessGrid.SelectedItem.ToString() + "' ORDER BY busName";
        //        string sqlstr = "SELECT distinct text, date FROM (" +
        //            sqlstr1 + ") as bus FULL OUTER JOIN TipTable ON TipTable.businessID = bus.businessID ORDER BY date";
        //        executeQuery(sqlstr, queryTips);
        //        addUsers();
        //    }
        //}

        //private void submitButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (businessGrid.Items.IndexOf(businessGrid.SelectedItem) > -1 && leaveTipTextBox.Text.Length > 0 && usersGrid.Items.IndexOf(usersGrid.SelectedItem) > -1)
        //    {
        //        string sqlstr = "INSERT INTO TipTable(businessID, user_id, likes, text, date) VALUES('" + (businessGrid.SelectedItem as Business).bid + "', '" +
        //         (usersGrid.SelectedItem as User).id + "', " + "0" + ", '" + leaveTipTextBox.Text + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //        executeQuery(sqlstr, queryTips);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Something has gone wrong, FIX IT", "Error adding a tip.", MessageBoxButton.OK);
        //    }
        //}

        //private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //Future works
        //}

        //Business Datagrid
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Clear information on the selected business
            clearSelectedBusinessInformation();

            if(businessDataGrid.SelectedIndex > -1)
            {
                //Get the data form the datagrid
                Business selectedBusiness = (businessDataGrid.SelectedItem as Business);

                //Update the selected business -->NEED TO WRITE THE QUERIES FOR THESE
                setSelectedBusinessInformation(selectedBusiness.name, selectedBusiness.address);
                setSelectedBusinessCategories(selectedBusiness.bid);
                setSelectedBusinessAttributes(selectedBusiness.bid);
            }

        }

        private void clearSelectedBusinessInformation()
        {
            //Bus name
            busNamelabel.Content = "Business Name: ";
            //adress
            busaddlabel.Content = "Address: ";

            //Date
            operationslabel.Content = "Today(day):  Opens:    Closes:";

            //Categories
            categoryDataGrid.Items.Clear();

            //Attributes
            attributesDataGrid.Items.Clear();
        }

        private void setSelectedBusinessInformation(string name, string address)
        {
            //This information we can get from the businessDataGrid

            //Bus name
            busNamelabel.Content = "Business Name: ";
            //adress
            busaddlabel.Content = "Address: ";
        }

        private void setHours(NpgsqlDataReader R)
        {
            //Notice that in the original query we are passing in the real date

            //Date
            operationslabel.Content = "Today(day):  Opens:    Closes:";
        }

        private void setSelectedBusinessCategories(string business_id)
        {
            string sqlstr = "";
            executeQuery(sqlstr, setSelectedBusinessCategories);
        }

        private void setSelectedBusinessCategories(NpgsqlDataReader R)
        {
            //Categories
            categoryDataGrid.Items.Add(new Category() { name=R.GetString(0), businessID=R.GetString(1)});
        }

        private void setSelectedBusinessAttributes(string business_id)
        {
            string sqlstr = "";
            executeQuery(sqlstr, setSelectedBusinessAttributes);
        }


        private void setSelectedBusinessAttributes(NpgsqlDataReader R)
        {
            //Attributes
            attributesDataGrid.Items.Add(new Attributes() { name=R.GetString(0), att_value=R.GetString(1), business_id=R.GetString(2)});
        }


        //Category Box changes
        private void busCategorylistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string sqlstr = "SELECT distinct BusinessTable.busName, BusinessTable.businessID FROM BusinessTable, CategoryTable WHERE BusinessTable.busPostal = '" + zipList.SelectedItem.ToString() + "'";
            //for (int i = 0; i < categoryListBox.SelectedItems.Count; i++)
            //{
            //    sqlstr += " AND BusinessTable.businessID IN (SELECT businessID FROM CategoryTable WHERE cat_name = '" + categoryListBox.SelectedItems[i].ToString() + "')";
            //}
            //executeQuery(sqlstr, queryBusiness);
        }

        //Edit current User information
        private void editbutton_Click(object sender, RoutedEventArgs e)
        {
            //Save the state of the objects -- Or we can just look at the selected index in the datagrid
            //Enable the Buttons
            enableCurrentUserInfo();

            //Now we wait

        }

        //Update current User information
        private void updatebutton_Click(object sender, RoutedEventArgs e)
        {
            //Disbale Buttons
            disableCurrentUserInfo();

            //Check the old information with the new information, if same{do nothing} else{update}

            //Update the information on the SQL database
            string user_id = (setUserDataGrid.SelectedItem as User).id;

            //Now we use the id to update the user information
        }

        //User entering in new current user (via name)
        private void setUsertextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //get the text that the user entered
            string userName = setUsertextBox.Text;
            clearUserInformation();

            setUserDataGrid.Items.Clear();
            friendDataGrid.Items.Clear();
            latestFriendTipsDataGrid.Items.Clear();

            switch (checkString(userName))
            {
                case 0:
                    //There is nothing so we won't do anything
                    //clearUserInformation();

                    //Now clear the userResultDataGrid
                    addUsers();
                    break;
                case 1:
                    //We are querying the User Table looking for all Users whose ID start with the given value
                    //setUserDataGrid.Items.Clear();
                    //friendDataGrid.Items.Clear();
                    //latestFriendTipsDataGrid.Items.Clear();
                    this.findUsersByID(userName);

                    break;
                case 2:
                    //We are querying the User Table looking for all Users whose Name contains with the given value
                    //setUserDataGrid.Items.Clear();
                    //friendDataGrid.Items.Clear();
                    //latestFriendTipsDataGrid.Items.Clear();
                    this.findUsersByName(userName);
                    break;
            }
        }

        private void clearUserInformation()
        {
            //Name
            curnametextBox.Text = "";

            //Stars
            curstarstextBox.Text = "";
            fanstextBox.Text = "";

            //Yelping Since
            yelpingsincetextBox.Text = "";

            //Votes
            funnytextBox.Text = "";
            cooltextBox.Text = "";
            usefultextBox.Text = "";

            //Tip Count
            tipCounttextBox.Text = "";

            //Likes
            tiptotaltextBox.Text = "";

            //Location
            longtextBox.Text = "";
            lattextBox.Text = "";
        }

        private void queryUser(NpgsqlDataReader R)
        {
            User newUser = new User()
            {
                id = R.GetValue(0).ToString(),
                name = R.GetValue(1).ToString(),
                funny = R.GetValue(2).ToString(),
                yelping_since = R.GetValue(3).ToString(),
                useful = R.GetValue(4).ToString(),
                fans = R.GetValue(5).ToString(),
                cool = R.GetValue(6).ToString(),
                avg_stars = R.GetValue(7).ToString(),
                tipcount = R.GetValue(8).ToString(),
                postcount = R.GetValue(9).ToString(),
                likecount = R.GetValue(10).ToString(),
                longitude = R.GetValue(11).ToString(),
                latitude = R.GetValue(12).ToString()
            };

            setUserDataGrid.Items.Add(newUser);
        }

        //This method gets the the users whose names contain the value
        private void findUsersByName(string name)
        {
            string sqlstr = "SELECT * FROM UserTable WHERE user_id ILIKE '%" + name + "%' ORDER BY user_id";
            executeQuery(sqlstr, queryUser);
        }

        private void findUsersByID(string user_id)
        {
            string sqlstr = "SELECT * FROM UserTable WHERE user_id LIKE '" + user_id + "%' ORDER BY user_id";
            executeQuery(sqlstr, queryUser);
        }

        //Gets user by the id
        private void findUserByID(string user_id)
        {
            string sqlstr = "SELECT * FROM UserTable WHERE user_id == '" + user_id+ "'  ORDER BY user_id";
            executeQuery(sqlstr, queryUser);
        }

        private int checkString(string str)
        {
            if(str == null || str.Length == 0)
            {
                return 0;
            }
            
            if(Regex.IsMatch(str, "^[0-9]+$",RegexOptions.Compiled))
            {
                return 1;
            }

            return 2;
        }

        private void setBasicUserInfo(string name, string yelpSince)
        {
            //Name
            curnametextBox.Text = name;

            //Yelping Since
            yelpingsincetextBox.Text = yelpSince;
        }

        private void setUserStars(string stars, string fans)
        {
            //Stars
            curstarstextBox.Text = stars;

            fanstextBox.Text = fans;
        }

        private void setUserVotes(string funny, string cool, string useful)
        {
            //Votes
            funnytextBox.Text = funny;
            
            cooltextBox.Text = cool;

            usefultextBox.Text = useful;
        }

        private void setUserLocation(string longitude, string latitude)
        {
            //Location
            longtextBox.Text = longitude;

            lattextBox.Text = latitude;
        }

        private void setUserTips(string count, string likes)
        {
            //Tip Count
            tipCounttextBox.Text = count;

            //Likes
            tiptotaltextBox.Text = likes;
        }

        //User selected for new current user datagrid
        private void setUserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearUserInformation();
            friendDataGrid.Items.Clear();
            latestFriendTipsDataGrid.Items.Clear();

            //Get Selection and 
            if (setUserDataGrid.Items.IndexOf(setUserDataGrid.SelectedItem) > -1)
            {
                //Query User Information
                User curUser = (setUserDataGrid.SelectedItem as User);


                //Query Friends


                // Tips of Friends

            }
        }

        List<string> values = new List<string>();

        string getAllUsersQuery()
        {
            return "SELECT * FROM UserTable ORDER BY user_id";
        }
        string getAllUserInformationByIDQuery(string user_id)
        {
            return "SELECT * FROM UserTable WHERE user_id == '"+ user_id +"' ORDER BY user_id";
        }

        string getUserInformationByIDQuery(string user_id)
        {
            return "SELECT name,funny, yelping_since, useful, fans, cool, average_stars, tipcount, likecount, longitude, latitude FROM UserTable WHERE user_id == '" + user_id + "' ORDER BY user_id";
        }

        //private void getAllUserInformationByID(NpgsqlDataReader R)
        //{
        //    for (int index=0; index < R.FieldCount; index++)
        //    {
        //        this.values.Append(R.GetString(index));
        //    } 
        //}

        //Resort the datagrid using the chosen option
        private void sortResultsByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Show the next window (checkin)
        private void showCheckinsButton_Click(object sender, RoutedEventArgs e)
        {
            if (tipsWindowIsOpen || checkinWindowIsOpen)
            {
                MessageBox.Show("Sorry the Checkin or Tip Window is still Open. CLOSE IT!", "Error Opening New Window.", MessageBoxButton.OK);
            }
            else
            {
                checkinWindowIsOpen = true;
                checkInWindow.ShowDialog();
                checkinWindowIsOpen = false;
            }
        }

        //Show the next window (Tips)
        private void showTipsButton_Click(object sender, RoutedEventArgs e)
        {
            if (tipsWindowIsOpen || checkinWindowIsOpen)
            {
                MessageBox.Show("Sorry the Checkin or Tip Window is still Open. CLOSE IT!", "Error Opening New Window.", MessageBoxButton.OK);
            }
            else
            {
                tipsWindowIsOpen = true;
                tipWindow.ShowDialog();
                tipsWindowIsOpen = false;
            }
        }

        //City is selected
        private void cityListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Keep in mind that this has multi-selection
            //And we do not want to clear the results box, but we will need to reload it

        }

        //Zip code is selected --> FINISHED
        private void zipcodeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StringBuilder str = new StringBuilder(" ");
            busCategorylistBox.Items.Clear();
            if(zipcodeListBox.SelectedItems.Count > 0)
            {
                string str2 = " ORDER BY busPostal";
                string sqlstr1 = "SELECT businessID FROM BusinessTable WHERE busPostal = '" + zipcodeListBox.SelectedItems[0].ToString() + "' ";
                for (int i = 1; i < zipcodeListBox.SelectedItems.Count; i++)
                {
                    str.Append("OR busPostal = '" + zipcodeListBox.SelectedItems[i].ToString() + "'  ");
                }
                sqlstr1 = sqlstr1 + str.ToString() + str2;

                // Need to make the sql statement to extract category name
                string sqlstr = "SELECT distinct cat_name FROM (" +
                    sqlstr1 + ") as bus, CategoryTable WHERE CategoryTable.businessID = bus.businessID ORDER BY cat_name";
                executeQuery(sqlstr, querySearchCategories);
            }
        }

        private void querySearchCategories(NpgsqlDataReader R)
        {
            busCategorylistBox.Items.Add(R.GetString(0));
        }

        //private void queryCity(NpgsqlDataReader R)
        //{
        //    cityList.Items.Add(R.GetString(0));
        //}

        //A state is selected
        private void statecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityListBox.Items.Clear();
            if (statecomboBox.SelectedIndex > -1)
            {
                string sqlstr = "SELECT distinct busCity FROM BusinessTable WHERE busState = '" + statecomboBox.SelectedItem.ToString() + "' ORDER BY busCity";
                executeQuery(sqlstr, queryCity);
            }
        }


        private string buildConnectionString(string host = "localhost", string username = "postgres", string database = "yelpdb", string password = "postgres")
        {
            return "Host = " + host + "; Username = " + username + "; Database = " + database + "; password = " + password + ";";
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

        private void searchBusinessButton_Click(object sender, RoutedEventArgs e)
        {
            //Clear the ResultsBox
            businessDataGrid.Items.Clear();

            //Now gather all of the information that we need
            //State,city,postalCode,businessCategory,price, Attributes
            //Groupby: 

            //Call execute Query with BusinessQuery --> THIS IS ONE HEAVY QUERY WE ARE ABOUT TO MAKE ;)
        }

        //Add Categories to the added categories list box
        private void addCatButton_Click(object sender, RoutedEventArgs e)
        {
            if (busCategorylistBox.SelectedIndex > -1)
            {
                addedCategoriesListBox.Items.Add(busCategorylistBox.Items[busCategorylistBox.SelectedIndex].ToString());
            }
        }

        private void removeCatButton_Click(object sender, RoutedEventArgs e)
        {
            if(addedCategoriesListBox.SelectedIndex > -1)
            {
                addedCategoriesListBox.Items.RemoveAt(addedCategoriesListBox.SelectedIndex);
            }
        }
    }
}
