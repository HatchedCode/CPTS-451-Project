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
using Npgsql;

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
        }
        public MainWindow()
        {
            InitializeComponent();
            addState();
            addColumns2grid();
        }

        /*
         * private string buildConnectionString()
         * {
         *      return "Host = localhost; Username = postgres; Database = milestone1db; password = pumba";
         * }
         */
        private void addState()
        {
            Statelist.Items.Add("WA");
            Statelist.Items.Add("CA");
            Statelist.Items.Add("ID");
            Statelist.Items.Add("OR");

            /* Making the connection to SQL 19:57
             * using (var connection = new NpgsqlConnection(buildConnectionString()))
             * {
             *      connection.Open();
             *      using (var cmd = new NpgsqlCommand())
             *      {
             *          cmd.Connection = connection
             *          cmd.CommandText = "SELECT distinct state FROM business ORDER BY state";
             *          try 
             *          { 
             *              var reader = cmd.ExecuteReader();
             *              while(reader.Read())
             *                  Statelist.Items.Add(reader.GetString(0));
             *          } catch (NgsqlException ex)
             *          {
             *              Console.WriteLine(ex.Message.ToString());
             *              System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString()); 
             *          } finally
             *          {
             *              connection.Close();
             *          }
             *      }
             * }
             */
        }

        private void addColumns2grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();

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

            Businessgrid.Items.Add(new Business() { name = "Chick-Fil-A", state = "WA", city = "Tacoma" });
            Businessgrid.Items.Add(new Business() { name = "Popeyes", state = "OR", city = "Portland" });
            Businessgrid.Items.Add(new Business() { name = "Dicks", state = "WA", city = "Seattle" });
            Businessgrid.Items.Add(new Business() { name = "KFC", state = "CA", city = "Los Angeles" });
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
            Businessgrid.Items.Add(new Business() { name = R.GetString(0), state = R.GetString(1), city = R.GetString(2) });
        }

        private void Citylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Businessgrid.Items.Clear();
            if (Citylist.SelectedIndex > -1)
            {
                string sqlstr = "SELECT name, state, city FROM business WHERE state = '" + Statelist.SelectedItem.ToString() + "' AND city = '" + Citylist.SelectedItem.ToString() + "' ORDER BY city;";
                executeQuery(sqlstr, addGridRow);
            }         
        }

    }
}

/* Stopped watching the video at 50:00*/
