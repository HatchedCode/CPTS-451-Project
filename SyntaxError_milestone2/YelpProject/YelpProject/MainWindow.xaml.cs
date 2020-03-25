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
        }

        private void addState()
        {
            stateList.Items.Add("WA");
            stateList.Items.Add("CA");
            stateList.Items.Add("OR");
            stateList.Items.Add("ID");
        }

        /*private void addColumns2Grid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "BusinessName";
            col1.Width = 255;
            businessGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col1.Header = "State";
            col1.Width = 80;
            businessGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col1.Header = "City";
            col1.Width = 80;
            businessGrid.Columns.Add(col3);
        }*/
    }
}
