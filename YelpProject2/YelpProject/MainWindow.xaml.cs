﻿using Npgsql;
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

        private void zipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryListBox.Items.Clear();
            if (zipList.SelectedIndex > -1)
            {
                // Need to make the sql statement to extract category name
                string sqlstr1 = "SELECT businessID FROM BusinessTable WHERE busPostal = '" + zipList.SelectedItem.ToString() + "' ORDER BY busPostal";
                string sqlstr = "SELECT distinct cat_name FROM (" +
                    sqlstr1 + ") as bus FULL OUTER JOIN CategoryTable ON CategoryTable.businessID = bus.businessID ORDER BY cat_name";
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

        private void categoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}