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
        public TipsWindow()
        {
            InitializeComponent();
            InitializeAll();
        }

        private void InitializeAll()
        {
            addColumns2TipGrid();
        }

        private void addNewTipbutton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newTipTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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

        private void busTipsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Clear the ResultsBox
            busTipsDataGrid.Items.Clear();

            string sqlstr = "SELECT T.date, U.name, T.likes, T.text FROM TipTable as T, UserTable as U WHERE U.user_id = T.user_id and T.businessid = '5KheTjYPu1HcQzQFtm4_vw'"; //5KheTjYPu1HcQzQFtm4_vw
            executeQuery(sqlstr, queryBusinessTipResult);
        }

        private void queryBusinessTipResult(NpgsqlDataReader R)
        {
            //businessDataGrid.Items.Add(new Business() { name = R.GetString(0), bid = R.GetString(1) }); GetValue(0).ToString()
            busTipsDataGrid.Items.Add(new Tip() { date = R.GetValue(0).ToString(), user_name = R.GetValue(1).ToString(), likes = R.GetValue(2).ToString(), text = R.GetValue(3).ToString() });
        }

        private string buildConnectionString(string host = "localhost", string username = "postgres", string database = "yelpdb", string password = "0622")
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
    }
}
