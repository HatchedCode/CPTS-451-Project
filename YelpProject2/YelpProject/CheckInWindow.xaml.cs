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
using Npgsql;
using YelpEngine;


namespace YelpProject
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        private string businessId;

        public CheckInWindow(string businessId)
        {
            InitializeComponent();
            this.businessId = businessId;
            this.initializeChart();
            this.queryBusinessCheckins();
        }

        private Dictionary<string, string> months = new Dictionary<string, string>();
        private List<KeyValuePair<string, int>> myChartData = new List<KeyValuePair<string, int>>();

        private void initializeChart()
        {
            this.months.Add("01", "Jan");
            this.months.Add("1", "Jan");
            this.months.Add("02", "Feb");
            this.months.Add("2", "Feb");
            this.months.Add("03", "March");
            this.months.Add("3", "March");
            this.months.Add("04", "April");
            this.months.Add("4", "April");
            this.months.Add("05", "May");
            this.months.Add("5", "May");
            this.months.Add("06", "June");
            this.months.Add("6", "June");
            this.months.Add("07", "July");
            this.months.Add("7", "July");
            this.months.Add("08", "Aug");
            this.months.Add("8", "Aug");
            this.months.Add("09", "Sept");
            this.months.Add("9", "Sept");
            this.months.Add("10", "Oct");
            this.months.Add("11", "Nov");
            this.months.Add("12", "Dec");
        }

        private void queryCheckins(NpgsqlDataReader R)
        {
            string month = R.GetValue(0).ToString();
            int count = R.GetInt32(1); //GetValue(1).ToString();
            if (this.months.TryGetValue(month, out string val))
            {
                month = val;
                this.myChartData.Add(new KeyValuePair<string, int>(month, count));
            }
            else
            {
                MessageBox.Show(month, "before", MessageBoxButton.OK);
                MessageBox.Show("Error retrieving month", "Error adding month to chart.", MessageBoxButton.OK);
            }
        }

        private void queryBusinessCheckins()
        {
            myChartData = new List<KeyValuePair<string, int>>();
            string sqlstr1 = "SELECT month, count(*) FROM CheckInTable WHERE businessID = '" + this.businessId+ "' group by month  order by month";
            executeQuery(sqlstr1, queryCheckins);
            checkChart.DataContext = myChartData;
            //SELECT month, count(*) FROM CheckInTable WHERE businessID = '-79cl_yASWXiv7RmzirNxA'  group by month  order by month;
        }

        private void checkInButton_Click(object sender, RoutedEventArgs e)
        {
            myChartData = new List<KeyValuePair<string, int>>();
            string sqlstr = "INSERT INTO CheckInTable VALUES('" + DateTime.UtcNow.Day.ToString("00") + "', '"+ DateTime.UtcNow.ToString("HH:mm:ss") +"', '" + DateTime.UtcNow.Year.ToString() + "', '" + DateTime.UtcNow.Month.ToString("00") + "', '" + this.businessId + "');";
            executeNonQuery(sqlstr);

            //Now re-populate the chart
            queryBusinessCheckins();
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
                        if(insertion < 0) //Error
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


    }
}
