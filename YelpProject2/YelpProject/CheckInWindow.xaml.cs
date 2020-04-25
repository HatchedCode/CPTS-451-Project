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

namespace YelpProject
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        public CheckInWindow()
        {
            InitializeComponent();
        }

        private void columnChart()
        {
            List<KeyValuePair<string, int>> myChartData = new List<KeyValuePair<string, int>>();
            myChartData.Add(new KeyValuePair<string, int>("Sun", 70));
            myChartData.Add(new KeyValuePair<string, int>("Mon", 50));
            myChartData.Add(new KeyValuePair<string, int>("Tue", 40));
            myChartData.Add(new KeyValuePair<string, int>("Wed", 10));
            myChartData.Add(new KeyValuePair<string, int>("Thu", 30));
            myChartData.Add(new KeyValuePair<string, int>("Fri", 40));
            myChartData.Add(new KeyValuePair<string, int>("Sat", 60));

            checkChart.DataContext = myChartData;
        }
    }
}
