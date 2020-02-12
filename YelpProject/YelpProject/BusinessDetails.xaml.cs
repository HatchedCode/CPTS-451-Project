using Npgsql;
using System;
using System.Windows;

namespace YelpProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string bid = "";
        public Window1(string bid)
        {
            InitializeComponent();
            this.bid = String.Copy(bid);
            loadBusinessDetails();
            loadBusinessNumbers();
        }


        private string buildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = milestone1db; password = postgres";
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
                        reader.Read(); //Since business_ids are unique, the query always returns a single value.
                        myf(reader);
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

        private void setNumInState(NpgsqlDataReader reader)
        {
            numinCity.Content = reader.GetInt16(0).ToString();
        }

        private void setNumInCity(NpgsqlDataReader reader)
        {
            numinState.Content = reader.GetInt16(0).ToString();
        }

        private void setBusinessDetails(NpgsqlDataReader reader)
        {
            bname.Text = reader.GetString(0);
            state.Text = reader.GetString(1);
            city.Text = reader.GetString(2);
        }

        private void loadBusinessNumbers()
        {
            string sqlStr1 = "SELECT count(*) from business WHERE state = (SELECT state FROM business WHERE business_id = '" + this.bid + "');";
            executeQuery(sqlStr1, setNumInState);

            string sqlStr2 = "SELECT count(*) from business WHERE city = (SELECT city FROM business WHERE business_id = '" + this.bid + "');";
            executeQuery(sqlStr2, setNumInCity);
        }

        private void loadBusinessDetails()
        {
            string sqlStr = "SELECT name, state, city FROM business WHERE business_id = '" + this.bid + "';";
            executeQuery(sqlStr, setBusinessDetails);
        }
    }
}
