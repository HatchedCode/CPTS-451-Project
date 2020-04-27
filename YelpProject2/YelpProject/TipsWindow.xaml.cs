using Npgsql;
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
using System.Windows.Shapes;
using YelpEngine;

namespace YelpProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TipsWindow : Window
    {
        private string userID;
        private string businessID;

        public TipsWindow(string businessID, string userID)
        {
            InitializeComponent();
            this.userID = userID;
            this.businessID = businessID;
            InitializeAll();
            this.addColumns2FriendsTipGrid();
            this.populateBusinessTipsDataGrid();
            this.populateFriendsTipsDataGrid();
        }

        private void InitializeAll()
        {
            addColumns2TipGrid();
        }

        private void addNewTipbutton_Click(object sender, RoutedEventArgs e)
        {
            if (newTipTextBox.Text.Trim().Length > 0)
            {
                string sqlstr = "INSERT INTO TipTable VALUES('" + this.businessID + "', '" + this.userID + "', '" + 0.ToString() + "', '" + newTipTextBox.Text + "', '" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "');";
                this.executeNonQuery(sqlstr);
                this.populateBusinessTipsDataGrid();
            }
            else
            {
                System.Windows.MessageBox.Show("No Text entered in the new tip box");
            }
        }

        private void addColumns2TipGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // date
            DataGridTextColumn col2 = new DataGridTextColumn(); // user name
            DataGridTextColumn col3 = new DataGridTextColumn(); // likes
            DataGridTextColumn col4 = new DataGridTextColumn(); // text
    
            col1.Binding = new Binding("date");
            col2.Binding = new Binding("user_name");
            col3.Binding = new Binding("likes");
            col4.Binding = new Binding("text");

            col1.Header = "Date";
            col2.Header = "User Name";
            col3.Header = "Likes";
            col4.Header = "Text";

            col1.Width = 200;
            col2.Width = 150;
            col3.Width = 50;
            col4.Width = 600;

            busTipsDataGrid.Columns.Add(col1);
            busTipsDataGrid.Columns.Add(col2);
            busTipsDataGrid.Columns.Add(col3);
            busTipsDataGrid.Columns.Add(col4);

            busTipsDataGrid.CanUserResizeColumns = false;
            busTipsDataGrid.CanUserResizeRows = false;
            busTipsDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            busTipsDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            busTipsDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }

        private void addColumns2FriendsTipGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn(); // date
            DataGridTextColumn col2 = new DataGridTextColumn(); // user name
            DataGridTextColumn col3 = new DataGridTextColumn(); // likes
            DataGridTextColumn col4 = new DataGridTextColumn(); // text

            col1.Binding = new Binding("user_name");
            col2.Binding = new Binding("date");
            col3.Binding = new Binding("text");

            col1.Header = "User Name";
            col2.Header = "Date";
            col3.Header = "Text";

            col1.Width = 200;
            col2.Width = 150;
            col3.Width = 600;

            friendTipsDataGrid.Columns.Add(col1);
            friendTipsDataGrid.Columns.Add(col2);
            friendTipsDataGrid.Columns.Add(col3);

            friendTipsDataGrid.CanUserResizeColumns = false;
            friendTipsDataGrid.CanUserResizeRows = false;
            friendTipsDataGrid.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            friendTipsDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            friendTipsDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }

        private void populateBusinessTipsDataGrid()
        {
            //Clear the ResultsBox
            busTipsDataGrid.Items.Clear();
            string sqlstr = "SELECT T.date, U.name, T.likes, T.text, U.user_id, T.businessID FROM TipTable as T, UserTable as U WHERE U.user_id = T.user_id and T.businessid = '" + this.businessID + "'  order by T.date desc"; //5KheTjYPu1HcQzQFtm4_vw for testing
            executeQuery(sqlstr, queryBusinessTipResult);
        }

        //SELECT T.date, U.name, T.likes, T.text FROM TipTable as T, UserTable as U WHERE U.user_id = T.user_id and T.businessid = '5KheTjYPu1HcQzQFtm4_vw'; //5KheTjYPu1HcQzQFtm4_vw for testing

        private void queryBusinessTipResult(NpgsqlDataReader R)
        {
            //businessDataGrid.Items.Add(new Business() { name = R.GetString(0), bid = R.GetString(1) }); GetValue(0).ToString()
            busTipsDataGrid.Items.Add(new Tip() { date = R.GetValue(0).ToString(), user_name = R.GetValue(1).ToString(), likes = R.GetValue(2).ToString(), text = R.GetValue(3).ToString(), user_id = R.GetValue(4).ToString(), business_id = R.GetValue(5).ToString()});
        }

        private void queryFriendsTipResult(NpgsqlDataReader R)
        {
            friendTipsDataGrid.Items.Add(new Tip() { user_name = R.GetValue(0).ToString(), date = R.GetValue(1).ToString(), text = R.GetValue(2).ToString() });
        }

        private void populateFriendsTipsDataGrid()
        {
            //Clear the ResultsBox
            friendTipsDataGrid.Items.Clear(); //Should already be cleared since the Window is just getting instantiated
            string sqlstr = "SELECT F.name, T.date, T.text FROM TipTable as T, (select  friend_user_id as friendID, current_user_id as userID, name from usertable, friendtable where usertable.user_id = friendtable.friend_user_id and friendtable.current_user_id = '"+ this.userID + "') as F" + " WHERE F.friendID = T.user_id  and T.businessid = '" + this.businessID + "' order by T.date desc"; //5KheTjYPu1HcQzQFtm4_vw for testing
            executeQuery(sqlstr, queryFriendsTipResult);
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

        private void executeNonQuery(string sqlstr)
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
                        int insertion = cmd.ExecuteNonQuery();
                        if (insertion < 0) //Error
                        {
                            System.Windows.MessageBox.Show("SQL ExecuteNonQuery Error - ");
                        }
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

        private void likeTipButton_Click(object sender, RoutedEventArgs e)
        {
            if(busTipsDataGrid.SelectedIndex > -1)
            {
                Tip userTip = busTipsDataGrid.SelectedItem as Tip;
                string sqlstr = "UPDATE TipTable SET likes = likes + 1 WHERE businessID = '" + userTip.business_id + "' and user_id = '" + userTip.user_id + "' and date = '" + userTip.date + "' ";
                this.executeNonQuery(sqlstr);
                this.populateBusinessTipsDataGrid();
            }
        }
    }
}
