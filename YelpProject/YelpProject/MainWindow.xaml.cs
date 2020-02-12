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
            public string bid { get; set; }
            public string name { get; set; }
            public string state { get; set; }
            public string city { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            addState();
            addColumns2grid();
        }

          private string buildConnectionString()
          {
               return "Host = localhost; Username = postgres; Database = milestone1db; password = postgres";
          }
         
        private void addState()
        {
              using (var connection = new NpgsqlConnection(buildConnectionString()))
              {
                   connection.Open();
                   using (var cmd = new NpgsqlCommand())
                   {
                       cmd.Connection = connection;
                       cmd.CommandText = "SELECT distinct state FROM business ORDER BY state";
                       try 
                       { 
                           var reader = cmd.ExecuteReader();
                           while(reader.Read())
                               Statelist.Items.Add(reader.GetString(0));
                       } catch (NpgsqlException ex)
                       {
                           Console.WriteLine(ex.Message.ToString());
                           System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString()); 
                       } finally
                       {
                           connection.Close();
                       }
                   }
              }
             
        }

        private void addColumns2grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            DataGridTextColumn col4 = new DataGridTextColumn(); //bid

            col1.Binding = new Binding("name");
            col1.Header = "Business Name";
            col1.Width = 270;
            Businessgrid.Columns.Add(col1);

            col2.Binding = new Binding("state");
            col2.Header = "State";
            col2.Width = 120;
            Businessgrid.Columns.Add(col2);

            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 190;
            Businessgrid.Columns.Add(col3);

            col4.Binding = new Binding("bid");
            col4.Header = "";
            col4.Width = 0;
            Businessgrid.Columns.Add(col4);
        }

        private void executeQuery(string sqlstr, Action<NpgsqlDataReader> myf)
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
                        {
                            myf(reader);
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

        private void addCity(NpgsqlDataReader R)
        {
            Citylist.Items.Add(R.GetString(0));
        }

        private void Statelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Citylist.Items.Clear();
            if (Statelist.SelectedIndex > -1)
            {
                string sqlstr = "SELECT distinct city FROM business WHERE state = '" + Statelist.SelectedItem.ToString() + "' ORDER BY city";
                executeQuery(sqlstr, addCity);
            }
        }

        private void addGridRow(NpgsqlDataReader R)
        {
            Businessgrid.Items.Add(new Business() { name = R.GetString(0), state = R.GetString(1), city = R.GetString(2), bid = R.GetString(3) });
        }

        private void Citylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Businessgrid.Items.Clear();
            if (Citylist.SelectedIndex > -1)
            {
                string sqlstr = "SELECT name, state, city, business_id FROM business WHERE state = '" + Statelist.SelectedItem.ToString() + "' AND city = '" + Citylist.SelectedItem.ToString() + "' ORDER BY city;";
                executeQuery(sqlstr, addGridRow);
            }         
        }

        private void businessGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Businessgrid.SelectedIndex > -1)
            {
                Business B = Businessgrid.Items[Businessgrid.SelectedIndex] as Business;
                if((B.bid != null) && (B.bid.ToString().CompareTo("") != 0))
                {
                    Window1 businessWindow = new Window1(B.bid.ToString());
                    businessWindow.Show();
                }
            }
        }

    }
}